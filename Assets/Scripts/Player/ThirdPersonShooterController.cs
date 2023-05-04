using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class ThirdPersonShooterController : MonoBehaviour
{
    //weapon stats
    public int weaponDamage, magazineCapacity, bulletsPerPress;
    public float timeBetweenShooting, timeBetweenShots, range, reloadTime;
    public bool allowButtonHold;
    public int bulletsLeft;

    //bools
    public bool shooting, readyToShoot, reloading;
    public bool deadeyeActive;

    //references
    public Camera playerCamera;
    public Transform muzzlePoint;
    public RaycastHit rayHit;
    public LayerMask isEnemy;

    //graphics
    public TextMeshProUGUI ammunitionCounter;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gunshot, reload, empty;

    [SerializeField] private ParticleSystem muzzleFlash;

    public float deadeyeResource = 100f;
    [SerializeField] float deadeyeDepleteOverTime = 0.1f;
    [SerializeField] float deadeyeDeplete = 15f;
    public float deadeyeIncrease;
    [SerializeField] private TextMeshProUGUI deadeyeCounter;

    [SerializeField] private HealthSlider slider;

    [SerializeField] private GameObject deadeyePPVolume;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Application.targetFrameRate = 60;
    }

    private void Awake()
    {
        bulletsLeft = magazineCapacity;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        ammunitionCounter.SetText(bulletsLeft + " | " + magazineCapacity);
        deadeyeCounter.text = Mathf.Round(deadeyeResource).ToString();
        if (!deadeyeActive)
        {
            Time.timeScale = 1;
        }
        if (deadeyeResource >= 100)
            deadeyeResource = 100;

        slider.SetHealth(deadeyeResource);

        if (deadeyeActive)
        {
            deadeyePPVolume.SetActive(true);
        }
        else
            deadeyePPVolume.SetActive(false);
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetButtonDown("Fire1");
        else shooting = Input.GetButtonDown("Fire1");
        deadeyeActive = Input.GetButton("Fire2");

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineCapacity && !reloading) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
            muzzleFlash.Play();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft == 0)
        {
            audioSource.PlayOneShot(empty);
        }

        if (deadeyeActive && deadeyeResource > 0)
        {
            Deadeye();
            deadeyeResource -= deadeyeDepleteOverTime;
        }
        else
            deadeyeActive = false;

        if (Input.GetButtonDown("Fire2"))
            deadeyeResource -= deadeyeDeplete;
    }

    private void Shoot()
    {
        readyToShoot = false;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out rayHit, range, isEnemy))
        {
            if (rayHit.collider.CompareTag("Enemy"))
                if (deadeyeActive)
                {
                    rayHit.collider.GetComponent<EnemyHealth>().enemyHealth -= weaponDamage * 2;
                    rayHit.collider.GetComponent<EnemyHealth>().bloodSplatter.Play();
                }
            else if (!deadeyeActive)
                {
                    rayHit.collider.GetComponent<EnemyHealth>().enemyHealth -= weaponDamage;
                    rayHit.collider.GetComponent<EnemyHealth>().bloodSplatter.Play();
                }
        }

        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting * Time.deltaTime * 60);

        audioSource.PlayOneShot(gunshot);
    }

    private void Deadeye()
    {
        Time.timeScale = 0.35f;
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime * Time.deltaTime * 60);

        audioSource.PlayOneShot(reload);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineCapacity;
        reloading = false;
    }
}
