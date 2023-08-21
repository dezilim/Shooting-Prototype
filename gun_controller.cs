using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_controller : MonoBehaviour
{
    // make gun follow camera.
    float rotationX = 0;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    // define a var that can take 3 enum values. 
    public enum ShootState {
        Ready, 
        Shooting, 
        Reloading
    }

    private float muzzleOffset; 

    [Header("Magazine")]
    public GameObject bullet;
    public int ammunition;

    [Range(0.5f, 10)] public float reloadTime;

    private int remainingAmmunition;

    [Header("Shooting")] 
    // how many shots per second
    [Range(0.25f, 25)] public float fireRate;
    [Range(0.5f, 100)] public float roundSpeed;
    [Range(0,45)] public float maxRoundVariation; 

    private ShootState shootState = ShootState.Ready;
    private float nextShootTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        muzzleOffset = GetComponent<Renderer>().bounds.extents.z;
        remainingAmmunition = ammunition;
    }

    // Update is called once per frame
    void Update()
    {
        // make gun rotation follow camera. 
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);



        switch(shootState) {
            case ShootState.Shooting:
                if (Time.time > nextShootTime){
                    shootState = ShootState.Ready;
                }
                break;
            case ShootState.Reloading:
                if(Time.time > nextShootTime){
                    remainingAmmunition = ammunition;
                    shootState = ShootState.Ready;
                }
                break;

        }
    }

    public void Shoot(){
        // instantiate bullet with velocity, if ready. 
        if(shootState == ShootState.Ready){
            GameObject spawnedBullet = Instantiate(
                bullet,
                transform.position + transform.forward * muzzleOffset,
                transform.rotation
            );
            if (spawnedBullet != null){
                spawnedBullet.transform.Rotate(new Vector3(
                    Random.Range(-1f, 1f) * maxRoundVariation,
                    Random.Range(-1f, 1f) * maxRoundVariation,
                    0
                ));

                Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
                rb.velocity = spawnedBullet.transform.forward * roundSpeed;
            }
            
            remainingAmmunition--;
            if(remainingAmmunition > 0){
                nextShootTime = Time.time + (1/fireRate);
                shootState = ShootState.Shooting;
            }else{
                Reload();
            }
        }

    }

    public void Reload() {
        if(shootState == ShootState.Ready){
            nextShootTime = Time.time + reloadTime;
            shootState = ShootState.Reloading;
        }
    }
}
