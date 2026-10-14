using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    public EnemyStateIdle(Enemy enemyScript) : base(enemyScript)
    {
    }

    public override void Execute()
    {
        Debug.Log("idle");
    }

    public override void ExecuteFixed()
    {
    }
}
