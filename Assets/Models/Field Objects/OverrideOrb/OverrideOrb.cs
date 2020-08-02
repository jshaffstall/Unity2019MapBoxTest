﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OverrideOrb : MonoBehaviour
{
    [SerializeField] private float throwSpeed = 30.0f;
    [SerializeField] private float collisionStallTime = 2.0f;
    [SerializeField] private float stallTime = 5.0f;

    [SerializeField] private AudioClip dropSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip throwSound;
    
    private float lastX;
    private float lastY;
    private bool released;
    private bool holding;
    private bool trackingCollisions = false;
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private InputStatus inputStatus;

    private enum InputStatus { Grabbing, Holding, Releasing, None}

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();

        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(rigidBody);
        Assert.IsNotNull(dropSound);
        Assert.IsNotNull(successSound);
        Assert.IsNotNull(throwSound);
    }

    private void Update ()
    {
        if (released)
            return;

        if (holding)
            FollowInput();

        // update input status
        UpdateInputStatus();

        // react to that status
        switch(inputStatus)
        {
            case InputStatus.Grabbing:
                Grab();
                break;

            case InputStatus.Holding:
                Drag();
                break;

            case InputStatus.Releasing:
                Release();
                break;
        }
    }

    public void FollowInput ()
    {

    }

    public void UpdateInputStatus ()
    {
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDownDown(0))
            inputStatus = InputStatus.Grabbing;
        else if (Input.GetMouseButtonDown(0))
            inputStatus = InputStatus.Holding;
        else if (Input.GetMouseButtonUp(0))
            inputStatus = InputStatus.Releasing;
        else
            inputStatus = InputStatus.None;
        #endif

        #if NOT_UNITY_EDITOR
        if (Input.GetTouch(0).phase == TouchPhase.Began)
            inputStatus = InputStatus.Grabbing;
        else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            inputStatus = InputStatus.Holding;
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            inputStatus = InputStatus.Releasing;
        else
            inputStatus = InputStatus.None;
        #endif


    }

    public void Grab ()
    {

    }

    public void Drag ()
    {

    }

    public void Release ()
    {

    }

    private Vector2 GetInputPosition()
    {
        return Vector2.down;
    }

    private void PowerDown()
    {
        Destroy(gameObject);
    }

}
