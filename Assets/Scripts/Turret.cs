using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float range = 15f;
    
    public string targetTag = "Enemy";
    
    public Transform PartToRotate;
    
    public float turnSpeed = 10;
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
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
