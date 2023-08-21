using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    public float damage;

    void Update(){
        if (transform.position.y < -10){
            Destroy(gameObject);
        }
    }
    // ok this is a diff way. I usually check the game object tag. In this case, only the enemies will have the script Target. 
    void OnCollisionEnter(Collision other) {
        enemy_controller target = other.gameObject.GetComponent<enemy_controller>();
        // Only attempts to inflict damage if the other game object has
        // the 'Target' component
        if(target != null) {
            target.Hit(damage);
            Destroy(gameObject); // Deletes the bullet
        }
    }


}