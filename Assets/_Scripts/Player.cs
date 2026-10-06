using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private Color m_hurtColor;
    [SerializeField] private InputActionAsset m_inputActions;
    [SerializeField] private GameObject m_projectilePrefab;
    [SerializeField] private float m_moveSpeed = 5.0f;
    [SerializeField] private float m_attackSpeed = 0.5f;
    [SerializeField] private float m_projectileSpeed = 1f;
    [SerializeField] private float m_projectileDamage = 50f;
    [SerializeField] private float m_movementSlowDuration = 0.2f;
    [SerializeField] private float m_movementSlowStrength = 0.2f;

    [SerializeField] private float m_hurtColorFlickSpeed = 0.1f;
    [SerializeField] private float m_maxHP = 100f;

    private float m_currHP;

    private InputAction m_move;
    private InputAction m_attack;

    private Animator m_animator;
    private Rigidbody2D m_rigidBody;
    private SpriteRenderer m_sprite;

    private float m_attackTimer;

    private bool m_isAttacking;
    private float m_movementMultiplier = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currHP = m_maxHP;

        m_move = m_inputActions.FindAction("Move");
        m_attack = m_inputActions.FindAction("Attack");

        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        //move the player
        Vector3 inputVector = m_move.ReadValue<Vector2>();
        m_rigidBody.linearVelocity = inputVector * m_moveSpeed * m_movementMultiplier;

        //handle animations
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
        //this function mostly handles the player animations. the functions handling the actual attack code are triggered by the animation events
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
        //if already attacking, don't do anything
        if (m_isAttacking == true) { return; }

        m_isAttacking = true;

        //grab the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        Vector3 normalizedDirection = (mousePos - this.transform.position).normalized;

        //set the player's animator direction relative to the mouse for animation handling
        m_animator.SetFloat("Vertical", normalizedDirection.y);
        m_animator.SetFloat("Horizontal", normalizedDirection.x);

        //slow down the player
        StartCoroutine(MovementLockout());

        //grab a projectile from the projectile manager
        GameObject projectile = RunManager.Instance.ProjectileManager.GetPlayerProjectile();

        //set position and rotation
        projectile.transform.position = transform.position;
        projectile.transform.right = normalizedDirection;

        //clear the trail to avoid smearing across the screen
        projectile.GetComponentInChildren<TrailRenderer>().Clear();

        //set projectile variables
        projectile.GetComponent<PlayerProjectile>().SetVariables(normalizedDirection, m_projectileSpeed, m_projectileDamage);
    }

    private IEnumerator MovementLockout()
    {
        m_movementMultiplier = m_movementSlowStrength;
        yield return new WaitForSeconds(m_movementSlowDuration);
        m_movementMultiplier = 1f;
    }

    //animator tells the script when the attack animation is over
    public void AttackEnd()
    {
        m_isAttacking = false;
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

        Debug.Log("my HP is at " + m_currHP);
    }

    public void Heal(float healAmount)
    {
        m_currHP += healAmount;
        if (m_currHP > m_maxHP)
        {
            m_currHP = m_maxHP;
        }

        Debug.Log("my HP is at " + m_currHP);
    }

    public void Die()
    {
        Debug.Log("I'M DEAD!");
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
