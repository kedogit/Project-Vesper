using UnityEngine;

public class RunManager : Singleton<RunManager>
{
    [SerializeField] private Transform m_playerSpawn;
    [SerializeField] private GameObject m_playerPrefab;

    [Header("Managers")]
    [SerializeField] private ProjectileManager m_projectileManager;

    public ProjectileManager ProjectileManager => m_projectileManager;

    protected override void Awake()
    {
        base.Awake();
        Instantiate(m_playerPrefab, m_playerSpawn);
    }
}
