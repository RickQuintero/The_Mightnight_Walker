using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandSoul : MonoBehaviour
{
    //General

    public float Enemylife=100f;
    public float EnemyLightResistance=4f;

    //Dead////

    private bool isDead=false;
    public GameObject ParticleDead;
    float Size=1f;
    float sizetime=0.1f;

    /// Light System/////

    public Transform LightChecker;
    public float LightCheckerDistance=0.4f;
    public LayerMask LightCheckerMask;
    private bool OnLight=false;
    public ParticleSystem Blood;

    void LateUpdate()
    {
        LightCheckerSystem();
        if (Enemylife < 0f) 
		{
            Dead();
        }
        
    }
    void LightCheckerSystem()
    {
        OnLight = Physics.CheckSphere(LightChecker.position,LightCheckerDistance,LightCheckerMask); 
        if (OnLight)
        {
            Enemylife = Enemylife-EnemyLightResistance;
            Blood.Play();
        }
        else
        {
            Blood.Stop();
        }
    }
    void Dead()
    {
        if (!isDead)
        {
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
    void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (LightChecker.position,LightCheckerDistance);
	}
}
