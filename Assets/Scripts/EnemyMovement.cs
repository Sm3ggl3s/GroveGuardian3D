using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public Transform PartToRotate; // what part of the model to rotate
    public float speed = 5f; // movement speed
    public float turnSpeed = 5f; // rotation speed
    
    private Transform target; // where to move to
    private int wp_idx = 0; // first waypoint index
    private GardenDamage gardenDamage; // reference to damage script
    public float slowed = 1f; //number to track slowing effect
    private float speedTracker; //stores the original speed

    void Start() {
        speedTracker = speed;
        target = Waypoints.points[0]; // set the first waypoint for the enemy
        gardenDamage = GameObject.Find("Greenhouse").GetComponent<GardenDamage>(); // find the component with the damage script
    }

    void Update() {
        // Get direction to move to and move to it at calculated speed
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // dir.normalized * speed: normalize the vector to keep it going at the same specified speed
        // Time.deltaTime: accounts for delays/different framerates on different machines
        // Space.world: move relative to the game world

        //Debug.Log(speed);

        // From Turret.cs
        Vector3 aim = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(aim);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // If we've reached waypoint (within small distance to wp), change to next target
        if(Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }

    // Function to help retrieve next waypoint and set it as the new target
    void GetNextWaypoint() {
        // If last waypoint reached, remove gameobject and deal damage to Garden
        if(wp_idx >= Waypoints.points.Length - 1) {
            gardenDamage.takeDamage(1f);
            Destroy(gameObject);
            return;
        }

        wp_idx ++;
        target = Waypoints.points[wp_idx];
    }

    public IEnumerator ReduceSpeed(float duration){       
        // Slow down the enemy
        if (speedTracker == speed){
            speed *= slowed;
        
            // Wait for the specified duration
            yield return new WaitForSeconds(duration);
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            // Restore the original speed
            speed = speedTracker;
            slowed = 1f;
        }
    }

}
