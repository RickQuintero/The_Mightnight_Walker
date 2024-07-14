using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TankMovement : MonoBehaviour
{
    //public CharacterController controller;
    public Transform cam;
    public Vector3 direction;
    Vector3 lastCamAngle;
 
   
    float camEulerAngX;
    float camEulerAngY;
    float camEulerAngZ;
 
    public bool canMovePlayer;
    public float speed = 1.0f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Rigidbody rb;
    private Animator anim;
    void Start()
    {
        //controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
    }
    void FixedUpdate()
    {
        
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
        camEulerAngX = cam.localEulerAngles.x;
        camEulerAngY = cam.localEulerAngles.y;
        camEulerAngZ = cam.localEulerAngles.z;
     
            if (canMovePlayer)
            {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                if ((horizontal==0) && (vertical==0)) // insert here your own check for player movement
                {
                    lastCamAngle = new Vector3(camEulerAngX, camEulerAngY, camEulerAngZ);
                }
 
                bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
                bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
                bool isWalking = hasHorizontalInput || hasVerticalInput;
                direction = new Vector3(horizontal, 0f, vertical).normalized;
                anim.SetFloat("Move_Speed", direction.magnitude);
                //m_Animator.SetBool("IsWalking", isWalking);
                if (isWalking)
                {
                    if (direction.magnitude >= 0.1f)
                    {
                        
                        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + lastCamAngle.y;
                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                        transform.rotation = Quaternion.Euler(0f, angle, 0f);
                        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                        rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
                        //controller.Move(moveDir * speed * Time.deltaTime);
                    }
                    
                }

            }
    }
}
