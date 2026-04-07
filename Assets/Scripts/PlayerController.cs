using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float x_MoveSpeed;
    public PlayerInput playerInput;

    public Vector2 inputDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegisterInput();
    }

    void RegisterInput()
    {
        InputSystem.actions["Move"].performed += ctx => inputDir = ctx.ReadValue<Vector2>();
        InputSystem.actions["Move"].canceled += ctx => inputDir = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputSystem.actions["Move"].IsPressed())
            MoveUsingTransform();
    }

    void UpdateDir(Vector2 dir)
    {
        inputDir = dir;
    }
    void MoveUsingTransform()
    {
    }
}
