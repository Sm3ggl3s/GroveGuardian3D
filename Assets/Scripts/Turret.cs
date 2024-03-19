using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("Setup Fields")]
    private Transform target;
    public string targetTag = "Enemy";
    public Transform PartToRotate;
    public float turnSpeed = 10;

    [Header("Attributes")]
    public int turretDamage = 20;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget (){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);
        float closest = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach(GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < closest){
                closest = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && closest <= range){
            target = nearestEnemy.transform;
        } else{
            target = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        
        Vector3 aim = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(aim);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if(fireCountdown <= 0){
            shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void shoot() {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
            bullet.Seek(target, turretDamage);
    }
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
