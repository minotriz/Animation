using System;
using UnityEngine;

public class Animacion : MonoBehaviour
{
    private CharacterController controller;

    public float forwardSpeed;
    private float diagonalForwardSpeed;
    private float backSpeed;
    private float diagonalBackSpeed;
    public float jumpSpeed;
    public float gravity;

    private Vector3 moveDirection;

    private float inputV;
    private float inputH;
    private float jumpInput;

    Animator animator;
    private float xAxis, zAxis;
    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();

        diagonalForwardSpeed = (float)Math.Sqrt(forwardSpeed * forwardSpeed / 2);
        backSpeed = forwardSpeed / 2;
        diagonalBackSpeed = (float)Math.Sqrt(backSpeed * backSpeed / 2);

        moveDirection = Vector3.zero;
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Rotate();
        Move();

        bool caminar = Input.GetKey(KeyCode.W);
        animator.SetBool("Caminar", caminar);

        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(xAxis, 0, zAxis);
        transform.Translate(movement * Time.deltaTime*speed);
    }

    private void GetInput()
    {
        inputV = Input.GetAxis("Vertical");
        inputH = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxisRaw("Jump");
    }

    private void Move() {
        if (controller.isGrounded)
        {
            gravity = 0;
            if (inputV == 0 & inputH == 0)
            { //Quieto
                moveDirection.Set(0, 0, 0);
            }
            else if (inputV > 0 & inputH == 0) //Avanza
            {
                moveDirection.Set(0, 0, inputV * forwardSpeed);
            }
            else if (inputV < 0 & inputH == 0) //Retrocede
            {
                moveDirection.Set(0, 0, inputV * backSpeed);
            }
            else if (inputV == 0 & inputH > 0) //Derecha
            {
                moveDirection.Set(inputH * forwardSpeed, 0, 0);
            }
            else if (inputV == 0 & inputH < 0) //Izquierda
            {
                moveDirection.Set(inputH * forwardSpeed, 0, 0);
            }
            else if (inputV > 0 & inputH > 0)//Avanza-Derecha
            {
                moveDirection.Set(inputH * diagonalForwardSpeed, 0, inputV * diagonalForwardSpeed);
            }
            else if (inputV > 0 & inputH < 0)//Avanza-Izquierda
            {
                moveDirection.Set(inputH * diagonalForwardSpeed, 0, inputV * diagonalForwardSpeed);
            }
            else if (inputV < 0 & inputH > 0)//Retrocede-Derecha
            {
                moveDirection.Set(inputH * diagonalBackSpeed, 0, inputV * diagonalBackSpeed);
            }
            else if (inputV < 0 & inputH < 0)//Retrocede-Izquierda
            {
                moveDirection.Set(inputH * diagonalBackSpeed, 0, inputV * diagonalBackSpeed);
            }
            moveDirection = transform.TransformDirection(moveDirection);

            if (jumpInput > 0)
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else {
            gravity = 25f;
        }
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Rotate() {
        transform.Rotate(0, Input.GetAxis("Mouse X") * 4f, 0);
    }
}
