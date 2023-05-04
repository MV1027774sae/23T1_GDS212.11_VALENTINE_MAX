using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip whip;
    [SerializeField] private AudioSource source;
    private float waitTime = 0.8f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
        Debug.Log("Moving to next scene");
    }

    IEnumerator StartGameCoroutine()
    {
        source.PlayOneShot(whip);

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(1);
    }
}
