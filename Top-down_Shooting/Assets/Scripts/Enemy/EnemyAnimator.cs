using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator enemy_animator;
    //private Enemy 
    // Start is called before the first frame update
    void Awake()
    {
        enemy_animator = GetComponent<Animator>();
    }

    public void OnAttackAnim(Enemy.EnemyType type)
    {
        if (type == Enemy.EnemyType.TypeA)
        {
            enemy_animator.SetTrigger("attackShort");
        }
        else if (type == Enemy.EnemyType.TypeB)
        {
            enemy_animator.SetTrigger("attack");
        }
        else
        {
            return;
        }
    }

    public void OnMovement()
    {
        enemy_animator.SetBool("isChase",true);
    }

    public void OnDeadAnim()
    {
        enemy_animator.SetTrigger("dead");
    }

    public void OnChase(bool isChase)
    {
        enemy_animator.SetBool("isChase", isChase);
    }

    public void OnWander(bool isWander)
    {
        enemy_animator.SetBool("isWander", isWander);
    }

}
