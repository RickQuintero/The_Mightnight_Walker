using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollower : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    Vector3 offset;
    void Start()
    {
        offset = new Vector3(0,1000,0);
        target = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target==null)return;
            transform.position = target.position + offset;
    }
}
