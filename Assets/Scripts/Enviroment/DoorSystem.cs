using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public bool Open;
    public float OpenVelocity=0.125f;
    public float DesiredAngle=90;
    private float CurrentAngle=0;
    private float InicialAngleY;
    public float AngleX;
    public float AngleZ;
    public AudioSource sound;
    void Start()
    {
        InicialAngleY = transform.rotation.y;
    }
    void LateUpdate()
    {
        if (Open)
        {
            CurrentAngle = Mathf.Lerp(CurrentAngle, DesiredAngle, OpenVelocity);
        }
        else
        {
            CurrentAngle = Mathf.Lerp(CurrentAngle, InicialAngleY, OpenVelocity);
        }
        transform.localRotation = Quaternion.Euler(new Vector3(AngleX,CurrentAngle,AngleZ));
        
    }
    public void PlayDoorSound()
    {
        sound.Play();
    }

}
