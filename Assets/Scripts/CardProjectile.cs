using UnityEngine;

public class CardProjectile : MonoBehaviour
{
    public float m_MoveSpeed;
    public float m_SpinSpeed;

    bool isMoving = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
            Spin();
        }

    }

    public void SetUpProjectile(float moveSpeed)
    {
        m_MoveSpeed = moveSpeed;
    }

    public void Move()
    {
        // moves in y-axis since its rotated
        transform.position += (transform.up * (m_MoveSpeed * Time.deltaTime));
    }

    public void Spin()
    {
        // spins locally in z axis
        transform.Rotate(Vector3.forward, (m_SpinSpeed * Time.deltaTime));
    }

    public void OnTriggerEnter(Collider other)
    {
        isMoving = false;
    }
}
