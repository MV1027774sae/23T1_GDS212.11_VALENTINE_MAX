using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour
{
    public int enemiesKilled;
    [SerializeField] int totalEnemies = 16;
    [SerializeField] private GameObject player;

    void Start()
    {
        enemiesKilled = 0;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void Update()
    {
        if (enemiesKilled == totalEnemies)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
    }
}
