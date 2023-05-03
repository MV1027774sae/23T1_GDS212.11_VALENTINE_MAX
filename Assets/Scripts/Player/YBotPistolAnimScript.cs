using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBotPistolAnimScript : MonoBehaviour
{
    [SerializeField] private PlayerMovementTutorial playerMovement;
    [SerializeField] private ThirdPersonShooterController shooterCon;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(anim);
    }

    void Update()
    {
        if (Input.GetKey("w"))
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        if (Input.GetKey("s"))
            anim.SetBool("isWalkingBack", true);
        else
            anim.SetBool("isWalkingBack", false);

        if (Input.GetKey("a"))
            anim.SetBool("isStrafeLeft", true);
        else
            anim.SetBool("isStrafeLeft", false);

        if (Input.GetKey("d"))
            anim.SetBool("isStrafeRight", true);
        else
            anim.SetBool("isStrafeRight", false);

        if (Input.GetKey("w") && Input.GetKey("left shift"))
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
        if (Input.GetKey("mouse 0") && shooterCon.readyToShoot && !shooterCon.reloading && shooterCon.bulletsLeft > 0)
            anim.SetTrigger("shoot");
    }
}
