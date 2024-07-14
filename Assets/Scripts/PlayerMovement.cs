using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float crouchSpeed = 2.5f;
    public float climbSpeed = 3f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    private bool isCrouching;
    private bool isClimbing;
    private bool reachedTop; // Variable para rastrear si el personaje ha alcanzado la parte superior

    private float originalJumpForce;
    private float jumpTime;
    private float maxJumpTime = 0.3f; // El tiempo máximo que el jugador puede mantener el salto

    public Transform groundCheck;
    public LayerMask groundMask;

    private float groundDistance = 0.4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        originalJumpForce = jumpForce;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!isClimbing)
        {
            Move();
            Jump();
            Crouch();
        }
        else
        {
            Climb();
        }

        CheckClimb();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;
            rb.MovePosition(transform.position + moveDirection * currentSpeed * Time.deltaTime);

            anim.SetFloat("Speed", currentSpeed);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        anim.SetFloat("Velocity", rb.velocity.magnitude);
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            anim.SetTrigger("Jump");
            jumpTime = 0f;
        }

        if (Input.GetButton("Jump") && !isGrounded && jumpTime < maxJumpTime)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpForce = originalJumpForce;
        }
    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                rb.GetComponent<CapsuleCollider>().height = 1f;
                rb.GetComponent<CapsuleCollider>().center = new Vector3(rb.GetComponent<CapsuleCollider>().center.x, -0.5f, rb.GetComponent<CapsuleCollider>().center.z);
                isCrouching = true;
                anim.SetBool("Crouch", true);
            }
        }
        else
        {
            if (isCrouching)
            {
                rb.GetComponent<CapsuleCollider>().height = 2f;
                rb.GetComponent<CapsuleCollider>().center = new Vector3(rb.GetComponent<CapsuleCollider>().center.x, 0f, rb.GetComponent<CapsuleCollider>().center.z);
                isCrouching = false;
                anim.SetBool("Crouch", false);
            }
        }
    }

    void Climb()
    {
        float moveY = Input.GetAxis("Vertical");

        if (moveY > 0 && !reachedTop) // Solo permitir el movimiento hacia arriba si no hemos alcanzado la parte superior
        {
            Vector3 move = transform.up * moveY;
            rb.MovePosition(transform.position + move * climbSpeed * Time.deltaTime);
            anim.SetFloat("ClimbSpeed", Mathf.Abs(moveY));
        }
        else
        {
            anim.SetFloat("ClimbSpeed", 0);
        }
    }

    void CheckClimb()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            {
                if (hit.collider.CompareTag("Ladder"))
                {
                    isClimbing = !isClimbing;
                    rb.useGravity = !isClimbing;
                    anim.SetBool("Climb", isClimbing);

                    if (isClimbing)
                    {
                        rb.velocity = Vector3.zero;
                        rb.isKinematic = true; // Evita que el Rigidbody interfiera con el movimiento de la trepada
                        reachedTop = false; // Reiniciar la variable reachedTop al comenzar a trepar
                    }
                    else
                    {
                        rb.isKinematic = false;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Realizar el Raycast para detectar si hay un objeto encima del personaje mientras trepa
        if (isClimbing)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, 1f))
            {
                if (!hit.collider.CompareTag("Ladder")) // Si el Raycast no golpea la escalera, significa que hemos alcanzado la parte superior
                {
                    reachedTop = true; // Establecer reachedTop en true para detener el movimiento hacia arriba
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

