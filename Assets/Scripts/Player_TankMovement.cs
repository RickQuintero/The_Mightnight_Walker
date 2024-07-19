using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TankMovement : MonoBehaviour
{
   public Transform cam;
    private Vector3 direction;
    private Vector3 lastCamAngle;

    private float camEulerAngX;
    private float camEulerAngY;
    private float camEulerAngZ;

    public bool canMovePlayer;
    public bool useConstantCameraAngle; // New flag to toggle between modes
    public float speed = 1.0f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        camEulerAngX = cam.localEulerAngles.x;
        camEulerAngY = cam.localEulerAngles.y;
        camEulerAngZ = cam.localEulerAngles.z;

        if (canMovePlayer)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (horizontal == 0 && vertical == 0)
            {
                lastCamAngle = new Vector3(camEulerAngX, camEulerAngY, camEulerAngZ);
            }

            bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
            bool isWalking = hasHorizontalInput || hasVerticalInput;

            direction = new Vector3(horizontal, 0f, vertical).normalized;
            anim.SetFloat("Move_Speed", direction.magnitude);

            if (isWalking)
            {
                if (useConstantCameraAngle)
                {
                    ApplyConstantCameraAngle();
                }
                else
                {
                    ApplyCurrentCameraAngle();
                }
            }
        }
    }

    private void ApplyConstantCameraAngle()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + lastCamAngle.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
        }
    }

    private void ApplyCurrentCameraAngle()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.localEulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir * speed * Time.deltaTime);
        }
    }

    // Method to toggle between modes
    public void ToggleCameraMode(bool useConstantAngle)
    {
        useConstantCameraAngle = useConstantAngle;
    }
}
