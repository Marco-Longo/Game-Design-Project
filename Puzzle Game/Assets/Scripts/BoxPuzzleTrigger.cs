﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzleTrigger : MonoBehaviour
{
    public GameObject gm;
    private AudioSource boxSlideSFX;
    private Rigidbody boxRB;

    private void Start()
    {
        boxSlideSFX = GetComponent<AudioSource>();
        boxSlideSFX.volume *= PlayerPrefs.GetFloat("SOUND");
        boxRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) //Game is paused
            boxSlideSFX.Pause();
        else
        {
            if (boxRB.velocity.magnitude > 2.0f && !boxSlideSFX.isPlaying)
                boxSlideSFX.Play();
            else if (boxRB.velocity.magnitude < 2.0f && boxSlideSFX.isPlaying)
                boxSlideSFX.Stop();
        }

        //Stop playing the sliding sound when the box falls into the trapdoor
        if (boxSlideSFX.isPlaying && transform.position.y < 2.3f)
            boxSlideSFX.Stop();
        //Destroy the boxes when they fall into the trapdoor
        if (transform.position.y < -11.0f)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        //When the box enters the correct space (measured by a trigger box) increase puzzle completion
        if (other.isTrigger == true && other.CompareTag("boxPuzzleTrigger"))
        {
            gm.GetComponent<GameManager>().IncreasePuzzleCompletion();   
        }
    }

    void OnTriggerExit(Collider other)
    {
        //When the box is no longer in position, decrease puzzle completion
        if (other.isTrigger == true && other.CompareTag("boxPuzzleTrigger"))
        {
            gm.GetComponent<GameManager>().DecreasePuzzleCompletion();
        }
    }
}



