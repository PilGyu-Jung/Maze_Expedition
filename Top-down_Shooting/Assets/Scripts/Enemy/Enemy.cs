using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
	public enum State { Idle, Chasing, Attacking, Wandering };
	
	public enum EnemyType { TypeA, TypeB, TypeC };

	[SerializeField]
	EnemyType enemyType;

	State currentState;

	public ParticleSystem deathEffect;
	public static event System.Action OnDeathStatic;

	NavMeshAgent pathfinder;
	Transform target;
	LivingEntity targetEntity;
	Material skinMaterial;
	FieldOfView fov;
	AIWandering aiwander;

	public EnemyAnimator enemyAnimator;
	public GameObject bullet;
	public float bulletspeed;
	public float attackRate;

	Color originalColour;

	float attackDistanceThreshold = .5f;
	float timeBetweenAttacks = 1;
	float damage = 1;

	float nextAttackTime;
	float lastAttackTime = 0;
	float myCollisionRadius;
	float targetCollisionRadius;

	bool hasTarget;
	bool isAttack;
	bool isChase;


	void Awake()
	{
		pathfinder = GetComponent<NavMeshAgent>();
		fov = GetComponent<FieldOfView>();
		aiwander = GetComponent<AIWandering>();

		//EnemyType = {bool A,bool };
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			hasTarget = true;

			target = GameObject.FindGameObjectWithTag("Player").transform;
			targetEntity = target.GetComponent<LivingEntity>();

			myCollisionRadius = GetComponent<CapsuleCollider>().radius;
			targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
		}
	}

	protected override void Start()
	{
		base.Start();
		if (hasTarget)
        {
            switch (enemyType)
            {
				case EnemyType.TypeA:
					currentState = State.Chasing;
					targetEntity.OnDeath += OnTargetDeath;
					StartCoroutine(UpdatePath());

					break;
				case EnemyType.TypeB:
					currentState = State.Idle;

					break;
				case EnemyType.TypeC:

					break;

			}

		}
	}

	//public void Wa

	public void SetCharacteristics(float moveSpeed, int hitsToKillPlayer, float enemyHealth, Color skinColour)
	{
		var main = deathEffect.main;
		pathfinder.speed = moveSpeed;

		if (hasTarget)
		{
			damage = Mathf.Ceil(targetEntity.startingHealth / hitsToKillPlayer);
		}
		startingHealth = enemyHealth;

		main.startColor = new Color(skinColour.r, skinColour.g, skinColour.b, 1);
		skinMaterial = GetComponent<Renderer>().material;
		skinMaterial.color = skinColour;
		originalColour = skinMaterial.color;
	}

	private void LookRotationToTarget()
    {
		// 목표 위치
		Vector3 to = new Vector3(target.position.x, 0, target.position.z);
		// 내 위치
		Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);

		// 바로돌기
		transform.rotation = Quaternion.LookRotation(to - from);
	}

	private void CalculateDistanceToTargetAndSelectState()
	{
		if (target == null) return;

		// 플레이어(Target)와 적의 거리 계산 후 거리에 따라 행동 선택
		float distance = Vector3.Distance(target.position, transform.position);

		if (distance <= aiwander.attackRadius || fov.canSeePlayer)
		{
			StopAllCoroutines();
			currentState = State.Attacking;
			isChase = false;
			aiwander.wandering = false;
			isAttack = true;

			StartCoroutine(EnemyLongAttack());
		}
		else if (distance > aiwander.attackRadius && distance <= aiwander.chaseRadius)
		{
			StopAllCoroutines();
			currentState = State.Chasing;
			isAttack = false;
			aiwander.wandering = false;
			isChase = true;
			StartCoroutine(Chasing());

		}
		else if (distance > aiwander.chaseRadius)
        {
			StopAllCoroutines();
			currentState = State.Wandering;
			isAttack = false;
			isChase = false;
			aiwander.wandering = true;
			enemyAnimator.OnWander(true);
        }
        else
        {
			enemyAnimator.OnWander(false);
			enemyAnimator.OnChase(false);
        }

	}
	public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
	{
		var main = deathEffect.main;

		AudioManager.instance.PlaySound("Impact", transform.position);
		if (damage >= health)
		{
			if(OnDeathStatic != null)
            {
				OnDeathStatic();
            }
			AudioManager.instance.PlaySound("Enemy Death", transform.position);

			Destroy(Instantiate(deathEffect.gameObject, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, main.startLifetime.constant);
		}
		base.TakeHit(damage, hitPoint, hitDirection);
	}

	void OnTargetDeath()
	{
		hasTarget = false;
		currentState = State.Idle;
	}

	void Update()
	{
		if (dead == true)
			return;
		
		if (hasTarget)
		{
            switch (enemyType)
            {
				case EnemyType.TypeA:
				if (Time.time > nextAttackTime)
				{
					float sqrDstToTarget
						= (target.position - transform.position).sqrMagnitude;
					if (sqrDstToTarget < Mathf.Pow
						(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
					{
						nextAttackTime = Time.time + timeBetweenAttacks;
						AudioManager.instance.PlaySound("Enemy Attack", transform.position);
						enemyAnimator.OnAttackAnim(enemyType);

						StartCoroutine(Attack());
					}
				}
					break;
				case EnemyType.TypeB:
					CalculateDistanceToTargetAndSelectState();

					break;
			}
		}
	}
	IEnumerator Attack()
	{
		pathfinder.isStopped = true;

		Vector3 originalPosition = transform.position;
		Vector3 dirToTarget = (target.position - transform.position).normalized;
		Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);

		float attackSpeed = 3;
		float percent = 0;

		skinMaterial.color = Color.red;
		bool hasAppliedDamage = false;

		while (percent <= 1)
		{

			if (percent >= .5f && !hasAppliedDamage)
			{
				hasAppliedDamage = true;
				targetEntity.TakeDamage(damage);
			}

			percent += Time.deltaTime * attackSpeed;
			float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
			transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);


			yield return null;
		}

		skinMaterial.color = originalColour;
		currentState = State.Chasing;
		pathfinder.isStopped = false;
	}

	IEnumerator EnemyLongAttack()
    {
		pathfinder.isStopped = true;

		LookRotationToTarget();

		while(hasTarget && isAttack)
        {
			if (!dead)
			{
				if (Time.time - lastAttackTime > attackRate)
				{
					lastAttackTime = Time.time;

					GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
					Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
					rigidBullet.velocity = transform.forward * bulletspeed;
					enemyAnimator.OnAttackAnim(enemyType);
				}
			}
			yield return null;
		}

	}
	IEnumerator UpdatePath()
	{
		float refreshRate = .25f;
		enemyAnimator.OnMovement();
		while (hasTarget)
		{
			if (currentState == State.Chasing)
			{
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
				if (!dead)
				{
					pathfinder.SetDestination(targetPosition);
				}
			}
			yield return new WaitForSeconds(refreshRate);
		}
	}

	IEnumerator Chasing()
    {
		if (dead)
			StopCoroutine(Chasing());

		pathfinder.isStopped = false;
		float refreshRate = .25f;
		enemyAnimator.OnChase(true);


		while (hasTarget && isChase)
        {
			if ( currentState == State.Chasing)
			{
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
				
				pathfinder.SetDestination(targetPosition);
			}

			yield return new WaitForSeconds(refreshRate);
        }
    }
    protected override void Die()
    {
		enemyAnimator.OnDeadAnim();
		pathfinder.isStopped = true;
        base.Die();
    }
}
