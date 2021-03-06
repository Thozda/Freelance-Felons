using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    public GameObject thePlayer;
    public bool isRunning;
    public bool isStepping = false;
    public float horizontalMove;
    public float verticalMove;
    public int stepNum;
    public AudioSource footStep1;
    public AudioSource footStep2;

    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (FiringPistol.isAiming == true)
            {
                thePlayer.GetComponent<Animator>().Play("Pistol Walk");
                horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
                verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 6;
                isRunning = true;
            }
            else
            {
                thePlayer.GetComponent<Animator>().Play("Running");
                horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * 150;
                verticalMove = Input.GetAxis("Vertical") * Time.deltaTime * 8;
                isRunning = true;
            }

            if (isStepping == false)
            {
                StartCoroutine(RunSound());
            }
            transform.Rotate(0, horizontalMove, 0);
            transform.Translate(0, 0, verticalMove);
        }
        else
        {
            if (FiringPistol.isAiming == false)
            {
                thePlayer.GetComponent<Animator>().Play("Idle");
                isRunning = false;
            }
            else
            {
                if (FiringPistol.isFiring == false)
                {
                    thePlayer.GetComponent<Animator>().Play("Pistol Idle");
                    isRunning = false;
                }
                else
                {
                    thePlayer.GetComponent<Animator>().Play("shooting");
                    isRunning = false;
                }
            }
        }
    }

    IEnumerator RunSound()
    {
        if (isRunning == true && isStepping == false)
        {
            isStepping = true;
            stepNum = Random.Range(1, 3);
            if (stepNum == 1)
            {
                footStep1.Play();
            }
            else
            {
                footStep2.Play();
            }
        }
        yield return new WaitForSeconds(0.3f);
        isStepping = false;
    }
}
