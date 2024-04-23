using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float explosionRadius= 0f;
    public float speed = 70f;
    private int bulletDamage;
    public GameObject impactEffect;
    private float slowEffect;

    public void Seek(Transform _target, int _damage, float _slow){
        target = _target;
        bulletDamage = _damage;
        slowEffect = _slow;
    }

    void Update()
    {
        if (target == null){
            Debug.Log("Null");
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget(){
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
        if(explosionRadius > 0f){
            Explode();
        } else {
            DamageTarget(bulletDamage, target);
        }
 
    }

    void DamageTarget(int damage, Transform enemy){
        EnemyBasic e = enemy.GetComponent<EnemyBasic>();
        e.TakeDamage(damage, slowEffect);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders){
            if(collider.tag == "Enemy"){
                DamageTarget(bulletDamage, collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
