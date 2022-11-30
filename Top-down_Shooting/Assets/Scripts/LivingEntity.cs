using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    public float health { get; protected set; }
    public bool dead;

    private const float minTimeBetDamaged = 0.1f;
    private float lastDamagedTime;

    public event System.Action OnDeath;

    protected virtual void Start()
    {
        dead = false;
        health = startingHealth;
    }

    protected bool IsInvulnerable
    {
        get
        {
            if (Time.time >= lastDamagedTime + minTimeBetDamaged)
                return false;


            return true;
        }
    }
    public virtual bool ApplyDamage(DamageMessage damageMessage)
    {
        if (IsInvulnerable || damageMessage.damager == gameObject || dead)
            return false;
        lastDamagedTime = Time.time;
        health -= damageMessage.amount;

        if (health <= 0)
            Die();

        return true;
    }

    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // Do some stuff here with hit var

        TakeDamage(damage);
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead || (health == startingHealth)) 
            return;
        health += newHealth;
    }

    [ContextMenu("Self Destruct")]
    protected virtual void Die()
    {
        dead = true;
        if(OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject,3);
    }

}
