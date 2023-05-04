using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthSlider healthSlider;
    
    public float playerHealth;
    private float playerHealthMax = 100f;
    [SerializeField] private float healthRegen = 1f;

    public float healthIndicator;
    [SerializeField] private TextMeshProUGUI totalPlayerhealth;
    public ParticleSystem bloodSplatter;

    private void Start()
    {
        playerHealth = playerHealthMax;
    }

    private void Update()
    {
        playerHealth += healthRegen;

        if (playerHealth > 100f)
        {
            playerHealth = playerHealthMax;
        }

        if (playerHealth <= 0f)
        {
            playerHealth = 0f;

            GetComponent<ThirdPersonShooterController>().enabled = false;
            GetComponent<PlayerMovementTutorial>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene(0);
        }

        healthSlider.SetHealth(playerHealth);

        totalPlayerhealth.text = Mathf.Round(playerHealth).ToString();
    }
}
