using UnityEngine;

public class EnemyStateChasing : EnemyState
{
    private Rigidbody2D m_rigidBody;
    private Animator m_animator;

    public EnemyStateChasing(Enemy enemyScript) : base(enemyScript)
    {
        m_rigidBody = enemyScript.gameObject.GetComponent<Rigidbody2D>();
        m_animator = enemyScript.gameObject.GetComponent<Animator>();

        m_animator.SetBool("Move", true);
        m_animator.SetBool("Attack", false);
    }

    public override void Execute()
    {
        Debug.Log("chasing");

        base.Execute();

        if (m_distance <= m_enemyScript.AttackRange)
        {
            m_enemyScript.SetState(new EnemyStateAttack(m_enemyScript));
        }

        m_animator.SetFloat("Horizontal", m_direction.x);
        m_animator.SetFloat("Vertical", m_direction.y);
    }

    public override void ExecuteFixed()
    {
        m_rigidBody.MovePosition(m_enemyScript.transform.position + (m_direction * Time.fixedDeltaTime * m_enemyScript.MoveSpeed));
    }
}
