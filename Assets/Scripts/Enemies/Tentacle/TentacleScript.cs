using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TentacleScript : MonoBehaviour
{
    public float TentacleHurtPower=3f;
    public Transform Player;
    public Transform CurrentTarget;
    public float VisionRadius=4f; 
    public float FollowVelocity=0.125f;
    public LegIk leg;
    public Vector3 Dir;
    private Vector3 StartP;
    private Transform LastPat;
    private bool TouchingPlayer;
    public float TouchingDistance=0.4f;
    public LayerMask PlayerMask;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag ("Player").transform;
        StartP = CurrentTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player==null)return;
        if (Vector3.Distance(Player.transform.position,transform.position)<VisionRadius)
        {
            Vector3 desiredposition = Player.position;
            Vector3 smoothposition = Vector3.Lerp(CurrentTarget.position, desiredposition, FollowVelocity);
            CurrentTarget.position = smoothposition;
            DetectPlayer();
        }
        else
        {
            MoveAlone();
        }
    }
    void DetectPlayer()
    {
        LastPat = leg.segments[leg.segmentCount-1].transform;
        TouchingPlayer = Physics.CheckSphere(LastPat.position,TouchingDistance,PlayerMask);
        if (TouchingPlayer)
        {
           Art_UIHealthSystem.Instance.HurtHealthPunch(TentacleHurtPower);
        }
    }

    void MoveAlone()
    {
        CurrentTarget.position = Vector3.Lerp(CurrentTarget.position, StartP + Dir * Mathf.Sin(Time.timeSinceLevelLoad), FollowVelocity); 
    }
    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, VisionRadius);
	}
}
