using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private Vector3 m_direction;

    private float m_projectileSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rigidBody.linearVelocity = m_direction * m_projectileSpeed;
    }

    public void SetVariables(Vector3 direction, float projectileSpeed)
    {
        m_direction = direction;
        m_projectileSpeed = projectileSpeed;
    }
}
