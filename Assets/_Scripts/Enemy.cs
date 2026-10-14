using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float m_maxHP = 100f;
    [SerializeField] private float m_hurtColorFlickSpeed = 0.1f;
    [SerializeField] private Color m_hurtColor = Color.red;
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_attackRange = 5f;

    private SpriteRenderer m_sprite;

    private EnemyState m_currentState;

    private float m_currHP;

    public float MoveSpeed => m_moveSpeed;
    public float AttackRange => m_attackRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currHP = m_maxHP;
        m_sprite = GetComponent<SpriteRenderer>();
        SetState(new EnemyStateChasing(this));
    }

    // Update is called once per frame
    void Update()
    {
        m_currentState.Execute();
    }

    private void FixedUpdate()
    {
        m_currentState.ExecuteFixed();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    public void Heal(float healAmount)
    {
        m_currHP += healAmount;
        if (m_currHP > m_maxHP)
        {
            m_currHP = m_maxHP;
        }
    }

    public void Hurt(float damageAmount)
    {
        m_currHP -= damageAmount;
        if (m_currHP <= 0)
        {
            m_currHP = 0;
            Die();
        }
        else
        {
            StartCoroutine(HitReact());
        }
    }

    public void SetState(EnemyState newState)
    {
        m_currentState = newState;
    }

    private IEnumerator HitReact()
    {
        m_sprite.color = m_hurtColor;
        yield return new WaitForSeconds(m_hurtColorFlickSpeed);
        m_sprite.color = Color.white;
        yield return new WaitForSeconds(m_hurtColorFlickSpeed);
        m_sprite.color = m_hurtColor;
        yield return new WaitForSeconds(m_hurtColorFlickSpeed);
        m_sprite.color = Color.white;
    }
}
