using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{

    public GameObject enemy;
    public GameObject spider_enemy;
    public GameObject player; 

    private int nb_enemies = 2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnEnemies", 1f, 15f);
        InvokeRepeating("spawnSpiderEnemies", 1f, 10f);
    }
    void spawnEnemies(){
        for(int i = 0; i<nb_enemies; i++){
            // get a random position near the player 
            Vector3 offset_position = new Vector3(Random.Range(-15f, 15), 0, Random.Range(-15f, 15));
            GameObject spawnedEnemy = Instantiate(
                enemy,
                player.transform.position + offset_position,
                player.transform.rotation
            );
        }

    }

    void spawnSpiderEnemies(){
        Vector3 offset_position_spider = new Vector3(Random.Range(5f, 15), 0, Random.Range(5f, 15));
        GameObject spawnedSpiderEnemy = Instantiate(
            spider_enemy,
            player.transform.position + offset_position_spider,
            player.transform.rotation
        );
        spawnedSpiderEnemy.GetComponent<Rigidbody>().velocity = new Vector3(offset_position_spider.x * 500f, 100f, offset_position_spider.z * 500f);
        Debug.Log("set velocity");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
