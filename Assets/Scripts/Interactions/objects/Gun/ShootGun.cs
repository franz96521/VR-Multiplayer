using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BulletPrefab;
    public Transform launcher;    
    public float VelocidadDisparo=6;
    public float bulletLife = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {

        GameObject BulletPrefablnstanc;
        BulletPrefablnstanc = (GameObject)Instantiate(BulletPrefab, launcher.position , launcher.rotation);
        BulletPrefablnstanc.GetComponent<Rigidbody>().velocity = launcher.transform.forward * VelocidadDisparo;
        KillBullet(BulletPrefablnstanc);


    }
    void KillBullet(GameObject bullet)
    {
        Destroy(bullet, bulletLife);
    }
}
