using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMovement(float horizontal, float vertical)
    {
        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);
    }

    public void OnShootAnim(int typeOfShoot)
    {
        animator.SetInteger("shotMode", typeOfShoot);
        animator.SetTrigger("gunShot");    
    }
    public void OnReloadAnim()
    {
        animator.SetTrigger("reload");
    }

    public void OnDeadAnim()
    {
        animator.SetTrigger("dead");
    }
}
