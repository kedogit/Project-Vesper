using System.Collections;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float m_lifetime = 5f;
    private Rigidbody2D m_rigidBody;
    private Vector3 m_direction;

    private float m_damageDealt;
    private float m_projectileSpeed;
    private float m_elapsed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_elapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLifetime();
        m_rigidBody.linearVelocity = m_direction * m_projectileSpeed;
    }

    public void SetVariables(Vector3 direction, float projectileSpeed, float damageAmount)
    {
        m_direction = direction;
        m_projectileSpeed = projectileSpeed;
        m_damageDealt = damageAmount;
    }

    private void CheckLifetime()
    {
        m_elapsed += Time.deltaTime;
        
        if (m_elapsed >= m_lifetime)
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.Hurt(m_damageDealt);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }
}
