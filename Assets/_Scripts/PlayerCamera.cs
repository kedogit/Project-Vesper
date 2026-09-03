using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform m_followTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_followTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = m_followTarget.position;
        newPosition.z = -10;
        transform.position = newPosition;
    }
}
