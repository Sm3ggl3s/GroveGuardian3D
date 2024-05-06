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
    public int turretBurst = 5;
    public float slowEffect = 0f;
    private float buffValue;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurretBurst());
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

            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
            foreach (Collider collider in colliders)
            {
                FervorFlower fervorFlower = collider.GetComponent<FervorFlower>();
                if (fervorFlower != null)
                {
                    buffValue = fervorFlower.buff;
                    Debug.Log("Detected FervorFlower with buff value: " + buffValue);
                }
            }
        } else{
            target = null;
        }
    }

    IEnumerator TurretBurst()
    {
        while (true)
        {
            if (fireCountdown <= 0 && target != null)
            {
                if (turretBurst > 0)
                {
                    shoot();
                    for (int i = 1; i < turretBurst; i++)
                    {
                        yield return new WaitForSeconds(0.1f);
                        shoot();
                    }
                }
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
            if(buffValue != 0.0f)
                fireCountdown = fireCountdown/buffValue;
            // Yield to the next frame before checking again
            yield return null;
        }
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 aim = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(aim);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void shoot() {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
            bullet.Seek(target, turretDamage, slowEffect);
    }
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Fertilize(){
        // Grab turrets tag
        string myTag = gameObject.tag;

        // Buff depending on the tag
        if (myTag == "BananaBurst")
        {
            // Increase burst
            turretBurst += 1;
        }
        else if (myTag == "BlastBloom")
        {
            // Increase damage
            turretDamage += 3;
        }
        else if (myTag == "Cocannut")
        {
            // Increase range
            range += 5;
        }
        else if (myTag == "ShackleVine")
        {
            //Increase slow or range if slow is maxxed
            if(slowEffect > .1f)
                slowEffect -= .1f;
            else
                range += 5;
        }
    }
}
