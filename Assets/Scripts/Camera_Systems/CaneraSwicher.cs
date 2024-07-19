using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CaneraSwicher : MonoBehaviour
{
    public CinemachineVirtualCamera activeCam;
    public bool FixedCamera = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            activeCam.Priority=1;
            other.SendMessage("ToggleCameraMode", FixedCamera);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            activeCam.Priority=0;
        }
    }
}
