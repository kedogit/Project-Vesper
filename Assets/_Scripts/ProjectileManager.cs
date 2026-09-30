using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject m_playerProjectilePrefab;

    private List<GameObject> m_playerProjectiles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerProjectiles = new List<GameObject>();
    }

    public GameObject GetPlayerProjectile()
    {
        foreach (GameObject projectile in m_playerProjectiles)
        {
            if (!projectile.activeSelf)
            {
                //resets velocity and turn bullet active to ready it for use
                projectile.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
                projectile.AddComponent<PlayerProjectile>();
                projectile.SetActive(true);
                return projectile;
            }
        }

        //create a new bullet and add it to the list
        GameObject newGameObject = Instantiate(m_playerProjectilePrefab, this.transform);
        m_playerProjectiles.Add(newGameObject);

        return newGameObject;
    }
}
