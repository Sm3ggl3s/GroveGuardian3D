using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    
    public float speed = 5f; //movement speed

    private Transform target; // where to move to
    private int wp_idx = 0; // first waypoint index

    void Start() {
        target = Waypoints.points[0]; //set the first waypoint for the enemy
    }

    void Update() {
        // Get direction to move to and move to it at calculated speed
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //dir.normalized * speed: normalize the vector to keep it going at the same specified speed
        //Time.deltaTime: accounts for delays/different framerates on different machines
        //Space.world: move relative to the game world

        //If we've reached waypoint (within small distance to wp), change to next target
        if(Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
    }

    //Function to help retrieve next waypoint and set it as the new target
    void GetNextWaypoint() {
        if(wp_idx >= Waypoints.points.Length - 1) {
            Destroy(gameObject);
            return;
        }

        wp_idx ++;
        target = Waypoints.points[wp_idx];
    }

}
