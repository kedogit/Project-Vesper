using UnityEngine;

public abstract class EnemyState
{
    protected Enemy m_enemyScript;
    protected float m_distance;
    protected Vector3 m_direction;

    public EnemyState(Enemy enemyScript)
    {
        m_enemyScript = enemyScript;
    }

    public virtual void Execute()
    {
        m_direction = (RunManager.Instance.Player.transform.position - m_enemyScript.transform.position).normalized;
        m_distance = Vector3.Distance(RunManager.Instance.Player.transform.position, m_enemyScript.transform.position);
    }

    public abstract void ExecuteFixed();
}
