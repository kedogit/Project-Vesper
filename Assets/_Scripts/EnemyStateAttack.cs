using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    private Animator m_animator;

    public EnemyStateAttack(Enemy enemyScript) : base(enemyScript)
    {
        m_animator = enemyScript.gameObject.GetComponent<Animator>();

        m_animator.SetBool("Move", false);
        m_animator.SetBool("Attack", true);
    }

    public override void Execute()
    {
        Debug.Log("attacking");

        base.Execute();

        if (m_distance > m_enemyScript.AttackRange)
        {
            m_enemyScript.SetState(new EnemyStateChasing(m_enemyScript));
        }

        m_animator.SetFloat("Horizontal", m_direction.x);
        m_animator.SetFloat("Vertical", m_direction.y);
    }

    public override void ExecuteFixed()
    {
    }
}
