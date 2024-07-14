using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DitzelGames.FastIK;

public class WhispererController : MonoBehaviour
{

    //General
    public float Enemylife=20f;
    public float EnemyLightResistance=4f;

    public float EnemyHurtPower=10f;
    public float EnemyHurtVelocity=2f;
    private float timerForHurt=3f;
    public float EnemySpeed=2f;
    public float visionRadius=10f;
    public float visionRadiusDestroy=30f;

    //Random
    private float timerDirection=0f;
    private Vector3 direccion;


    //Others
    private NavMeshAgent nav;
    private Transform target;
    public FastIKLook LookPart;

    //LightChecker
    
    public Transform LightChecker;
    public float LightCheckerDistance=0.4f;
    public LayerMask LightCheckerMask;
    private bool OnLight=false;

    private bool isDead=false;
    public GameObject ParticleDead;
    float Size=1f;
    float sizetime=0.1f;


	void Start () {

		nav = GetComponent<NavMeshAgent> ();
        nav.SetDestination (transform.position);
		nav.speed = EnemySpeed;
        target = GameObject.FindGameObjectWithTag ("Player").transform;
        LookPart.Target = target;
	}

    void LateUpdate()
    {
        LightCheckerSystem();
        if (Enemylife > 0f) 
		{
            MovementFollow();
        }
        else
        {
            Dead();
        }
        
    }
    void LightCheckerSystem()
    {
        OnLight = Physics.CheckSphere(LightChecker.position,LightCheckerDistance,LightCheckerMask); 
        if (OnLight)
        {
            Enemylife= Enemylife-EnemyLightResistance;
        }
    }
    void Dead()
    {
        if (!isDead)
        {
            nav.SetDestination(transform.position);
            isDead=true;
            Instantiate(ParticleDead,transform.position,Quaternion.identity);   
        }
        if (isDead)
        {
            Size = Mathf.Lerp(Size,0f,sizetime);
            transform.localScale = new Vector3(Size,Size,Size);
            if (transform.localScale.x<0.1f)
            {
                Destroy(gameObject);
            } 
        }
    }
    void MovementFollow()
    {
        if (target!=null)
        {
            if (Vector3.Distance (target.transform.position, transform.position) < visionRadius) 
            {
                LookPart.enabled =true;
				nav.SetDestination (target.position);
			}
            else
            {
                LookPart.enabled =false;
                RandomizarDireccion ();
                nav.SetDestination (transform.position + direccion);
            }
            //if (Vector3.Distance (target.transform.position, transform.position) > visionRadiusDestroy) 
            //{
                
            //}
        }
        else
        {
            LookPart.enabled =false;
            RandomizarDireccion ();
            nav.SetDestination (transform.position + direccion);
        }
        
        
    }
    void RandomizarDireccion()
	{
        timerDirection += Time.deltaTime;
		if (timerDirection>2f) 
		{
			timerDirection = 0;
            float ramdonx = Random.Range (-1, 2);
			float ramdonz = Random.Range (-1, 2);
            direccion = new Vector3 (ramdonx, 0, ramdonz);
             
			
		}
	}
    void HurtPlayer()
	{
		timerForHurt = timerForHurt + Time.deltaTime;

		if (timerForHurt > EnemyHurtVelocity) 
		{
            Art_UIHealthSystem.Instance.HurtHealthPunch(EnemyHurtPower);
            timerForHurt=0f;
		}
	}
    void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			HurtPlayer();
		}
    }
    void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			timerForHurt=EnemyHurtVelocity+1;
		}
    }
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, visionRadius);

        Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (LightChecker.position,LightCheckerDistance);
	}
}
