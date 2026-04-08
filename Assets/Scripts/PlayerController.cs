using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public PlayerInput playerInput;
    public Camera cam;

    [Header("Variables")]
    public float x_MoveSpeed;
    public float x_JumpSpeed;
    public float x_RotateSpeed;
    public float x_RotationClampAngle = 90.0f;

    Vector2 inputDirMove;
    Vector2 inputDirRotateDelta;
    float rotation;
    Vector3 direction;
    Vector3 directionOfJump;

    bool hasInput_Move;
    bool hasInput_Rotate;
    bool isJumping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegisterInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void RegisterInput()
    {
        InputSystem.actions["Move"].performed += ctx => StartMoving(ctx.ReadValue<Vector2>());
        InputSystem.actions["Move"].canceled += ctx => StopMoving();
        InputSystem.actions["Look"].performed += ctx => StartRotating(ctx.ReadValue<Vector2>());
        InputSystem.actions["Look"].canceled += ctx => StopRotating();
        InputSystem.actions["Jump"].performed += ctx => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasInput_Rotate)
            Rotate();

        if (hasInput_Move)
            Move();
    }

    void StartMoving(Vector2 dir)
    {
        inputDirMove = dir;
        hasInput_Move = true;
    }

    void StopMoving()
    {
        inputDirMove = Vector2.zero;
        hasInput_Move = false;
    }

    void StartRotating(Vector2 dir)
    {
        Debug.Log(dir);
        inputDirRotateDelta = dir;
        hasInput_Rotate = true;
    }
    void StopRotating()
    {
        inputDirRotateDelta = Vector2.zero;
        hasInput_Rotate = false;
    }

    void Move()
    {
        direction = new Vector3(inputDirMove.x, 0, inputDirMove.y);
        direction = transform.TransformDirection(direction);
        direction.Normalize();

        Vector3 finalDir = direction;
        float finalSpeed = x_MoveSpeed;

        //if (isJumping)
        //{
        //    finalDir = (direction * 0.01f + directionOfJump * 0.99f).normalized;
        //    directionOfJump = finalDir;
        //}

        transform.position += finalDir * (finalSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        transform.Rotate(new Vector3(0, inputDirRotateDelta.x, 0) * x_RotateSpeed * Time.deltaTime);
        rotation += -inputDirRotateDelta.y * x_RotateSpeed * Time.deltaTime;
        rotation = Mathf.Clamp(rotation, -x_RotationClampAngle, x_RotationClampAngle);
        cam.transform.localRotation = Quaternion.Euler(new Vector3(rotation, 0, 0));
    }

    void Jump()
    {
        if (isJumping)
            return;

        transform.GetComponent<Rigidbody>().AddForce(Vector3.up * x_JumpSpeed, ForceMode.Impulse);
        directionOfJump = direction;
        isJumping = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;
    }
}
