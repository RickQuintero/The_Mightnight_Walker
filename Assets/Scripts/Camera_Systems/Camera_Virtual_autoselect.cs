using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class Camera_Virtual_autoselect : MonoBehaviour
{
    public Transform lookAtTarget;
    public Transform followTarget;
    public bool activateFollow = true;
    private CinemachineVirtualCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        lookAtTarget = Art_GameController.Instance.Player.transform.GetChild(0).transform; 
        followTarget = Art_GameController.Instance.Player.transform.GetChild(0).transform;
    }
    void LateUpdate()
    {
        if (vcam != null)
        {
            if (lookAtTarget != null)
            {
                vcam.LookAt = lookAtTarget;
            }
            if ((followTarget != null) && (activateFollow))
            {
                vcam.Follow = followTarget; 
            }
        }
    }

}
