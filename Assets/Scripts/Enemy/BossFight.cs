using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject text;
    void Start()
    {
        player.GetComponent<PlayerMovementTutorial>().enabled = false;
        player.GetComponent<ThirdPersonShooterController>().enabled = false;
        boss.GetComponent<EnemyAIController>().enabled = false;

        player.GetComponent<Rigidbody>().AddForce(transform.forward * 0.1f, ForceMode.Impulse);
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(ActivateBoss());

            player.GetComponent<ThirdPersonShooterController>().enabled = true;
            player.GetComponent<ThirdPersonShooterController>().deadeyeActive = true;

            text.SetActive(false);
        }

        if (boss.GetComponent<EnemyHealth>().enemyHealth < 100)
        {
            StartCoroutine(BossDefeated());
        }
    }

    IEnumerator ActivateBoss()
    {
        yield return new WaitForSeconds(1.25f);

        boss.GetComponent<EnemyAIController>().enabled = true;
    }

    IEnumerator BossDefeated()
    {
        yield return new WaitForSeconds(0.25f);

        SceneManager.LoadScene(4);
    }
}
