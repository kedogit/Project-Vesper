using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float m_maxHP = 100f;
    [SerializeField] private float m_hurtColorFlickSpeed = 0.1f;
    [SerializeField] private Color m_hurtColor;

    private SpriteRenderer m_sprite;

    private float m_currHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currHP = m_maxHP;

        m_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Debug.Log("I'M DEAD!");
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
