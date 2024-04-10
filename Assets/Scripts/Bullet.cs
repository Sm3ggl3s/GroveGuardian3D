using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;

    public float speed = 70f;
    public int bulletDamage;
    public GameObject impactEffect;

    public void Seek(Transform _target, int _damage){
        target = _target;
        bulletDamage = _damage;
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

    }

    void HitTarget(){
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
        DamageTarget(bulletDamage);
    }

    void DamageTarget(int damage){
        EnemyBasic e = target.GetComponent<EnemyBasic>();
        e.TakeDamage(damage);
    }
}