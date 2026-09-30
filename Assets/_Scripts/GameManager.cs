using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Managers")]
    [SerializeField] private SceneTransitionManager m_sceneManager;

    public SceneTransitionManager SceneManager => m_sceneManager;

    protected override void Awake()
    {
        base.Awake();
        m_sceneManager.ChangeScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
