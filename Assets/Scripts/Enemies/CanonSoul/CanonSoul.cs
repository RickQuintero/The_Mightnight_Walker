using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonSoul : MonoBehaviour
{
    private Transform Canon;
    public GameObject Bullet;

    public float visionRadius;
    public float Shootforce=6f;
    public int ShootTime=2;
    private Transform player;
    private float Timer=0f;
    void Update()
    {
        Timer+=Time.deltaTime;
        Canon = transform.GetChild(0).GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 dir =new Vector3 (player.position.x,transform.position.y,player.position.z);
        transform.LookAt (dir);

        if (Timer>ShootTime)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < visionRadius)
            {
                Shoot();
            }
            Timer=0f;
        }
    }
    void Shoot()
    {
        GameObject firebullet = Instantiate(Bullet, Canon.position, Canon.rotation);
        firebullet.GetComponent<Rigidbody>().velocity = Canon.forward * Shootforce;
    }
    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, visionRadius);
	}
}