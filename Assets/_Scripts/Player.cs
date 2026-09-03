using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_inputActions;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_moveSpeed = 5.0f;
    [SerializeField] private float m_attackSpeed = 0.5f;
    [SerializeField] private float m_projectileSpeed = 1f;
    [SerializeField] private float m_movementSlowDuration = 0.2f;
    [SerializeField] private float m_movementSlowStrength = 0.2f;

    private InputAction m_move;
    private InputAction m_attack;

    private Animator m_animator;
    private Rigidbody2D m_rigidBody;

    private float m_attackTimer;

    private bool m_isAttacking;
    private float m_movementMultiplier = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        m_move = m_inputActions.FindAction("Move");
        m_attack = m_inputActions.FindAction("Attack");

        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        Vector3 inputVector = m_move.ReadValue<Vector2>();
        m_rigidBody.linearVelocity = inputVector * m_moveSpeed * m_movementMultiplier;

        if (m_move.WasPressedThisFrame())
        {
            m_animator.SetBool("Move", true);
        }

        if (m_move.WasReleasedThisFrame())
        {
            m_animator.SetBool("Move", false);
        }

        if (m_move.IsPressed() && !m_isAttacking)
        {
            m_animator.SetFloat("Vertical", inputVector.y);
            m_animator.SetFloat("Horizontal", inputVector.x);
        }
    }

    private void Attack()
    {
        m_attackTimer += Time.deltaTime;

        if (m_attack.WasReleasedThisFrame())
        {
            m_animator.SetBool("Attack", false);
        }

        if (m_attack.IsPressed())
        {
            if (m_attackTimer >= m_attackSpeed)
            {
                m_animator.SetBool("Attack", true);
                m_attackTimer = 0f;
            }
        }
    }

    public void Shoot()
    {
        m_isAttacking = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector3 normalizedDirection = (mousePos - this.transform.position).normalized;

        m_animator.SetFloat("Vertical", normalizedDirection.y);
        m_animator.SetFloat("Horizontal", normalizedDirection.x);

        StartCoroutine(MovementLockout());

        GameObject projectile = Instantiate(m_projectilePrefab, this.transform.position, Quaternion.identity);

        projectile.transform.right = normalizedDirection;
        projectile.GetComponent<PlayerProjectile>().SetVariables(normalizedDirection, m_projectileSpeed);
    }

    private IEnumerator MovementLockout()
    {
        m_movementMultiplier = m_movementSlowStrength;
        yield return new WaitForSeconds(m_movementSlowDuration);
        m_movementMultiplier = 1f;
    }

    public void AttackEnd()
    {
        m_isAttacking = false;
    }

}
