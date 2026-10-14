using UnityEngine;

public class RunManager : Singleton<RunManager>
{
    [SerializeField] private Texture2D m_cursorTexture;
    [SerializeField] private Transform m_playerSpawn;
    [SerializeField] private GameObject m_playerPrefab;

    [Header("Managers")]
    [SerializeField] private ProjectileManager m_projectileManager;

    public ProjectileManager ProjectileManager => m_projectileManager;

    private GameObject m_player;

    public GameObject Player => m_player;

    protected override void Awake()
    {
        base.Awake();
        m_player = Instantiate(m_playerPrefab, m_playerSpawn);
        Cursor.SetCursor(m_cursorTexture, new Vector2(m_cursorTexture.width/2, m_cursorTexture.height/2), CursorMode.Auto);
    }
}
