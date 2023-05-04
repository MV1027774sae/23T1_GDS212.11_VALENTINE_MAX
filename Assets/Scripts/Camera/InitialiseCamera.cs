using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseCamera : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    void Start()
    {
        Initialise();
    }

    private void Initialise()
    {
        mainCamera.SetActive(false);
        mainCamera.SetActive(true);
    }
}
