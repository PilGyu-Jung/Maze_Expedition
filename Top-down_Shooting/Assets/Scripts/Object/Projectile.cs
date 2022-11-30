using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    //public Color trailColor;
    float speed = 10;
    float damage = 1;
    float lifeTime = 2;
    float skinWidth = .1f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);

        Collider[] initialCollisions 
            = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if(initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }

        //GetComponent<TrailRenderer>().material.SetColor("_TintColor", trailColor);
    }
    public void SetSpeed( float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);

    }

    void CheckCollisions(float _moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, _moveDistance + skinWidth, collisionMask
            , QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }

    //void OnHitObject(RaycastHit hit)
    //{
    //    Debug.Log(hit.collider.gameObject.name);
    //    IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
    //    if(damageableObject != null)
    //    {
    //        damageableObject.TakeHit(damage, hit);
    //    }
    //    GameObject.Destroy(gameObject);
    //}

    void OnHitObject(Collider c, Vector3 hitpoint)
    {
        Debug.Log(c.gameObject.name);
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            //damageableObject.TakeDamage(damage);
            damageableObject.TakeHit(damage, hitpoint, transform.forward);
        }
        GameObject.Destroy(gameObject);
    }

}
