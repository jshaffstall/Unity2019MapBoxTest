﻿using System;
using System.Collections;
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

    private void FollowInput ()
    {
        Vector3 inputPosition = GetInputPosition();
        inputPosition.z = Camera.main.nearClipPlane * 7.5f;
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(inputPosition);

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 50.0f * Time.deltaTime);
    }

    private void UpdateInputStatus ()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
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
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            inputStatus = InputStatus.Releasing;
        else if (Input.touchCount == 1)
            inputStatus = InputStatus.Holding;
        else
            inputStatus = InputStatus.None;
#endif
    }

    private void Grab ()
    {
        Ray ray = Camera.main.ScreenPointToRay(GetInputPosition());
        RaycastHit point;

        if (Physics.Raycast(ray, out point, 100.0f) && point.transform == transform)
        {
            holding = true;
            transform.parent = null;
        }
    }

    private void Drag ()
    {
        lastX = GetInputPosition().x;
        lastY = GetInputPosition().y;
        
    }

    private void Release ()
    {
        if (lastY < GetInputPosition().y)
            Throw(GetInputPosition());
    }

    private void Throw(Vector2 targetPos)
    {
        rigidBody.useGravity = true;
        trackingCollisions = true;

        float yDiff = (targetPos.y - lastY) / Screen.height * 100;
        float speed = throwSpeed * yDiff;

        float x = (targetPos.x / Screen.width) - (lastX / Screen.width);

        x = Mathf.Abs(GetInputPosition().x - lastX) / (Screen.width * 100 * x);

        Vector3 direction = new Vector3(x, 0.0f, 1.0f);
        direction = Camera.main.transform.TransformDirection(direction);
        
        rigidBody.AddForce((direction * speed / 2.0f) + Vector3.up * speed);
        
        audioSource.PlayOneShot(throwSound);

        released = true;
        holding = false;
        Invoke(nameof(PowerDown), stallTime);
    }
    
    private Vector2 GetInputPosition()
    {
        Vector2 result = new Vector2();
        
#if UNITY_EDITOR
        result = Input.mousePosition;
#endif
        
#if NOT_UNITY_EDITOR
        result = Input.GetTouch(0).position;
#endif

        return result;
    }

    private void PowerDown()
    {
        CaptureSceneManager manager = FindObjectOfType<CaptureSceneManager>();
        
        if (manager != null)
            manager.OrbDestroyed();
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!trackingCollisions)
            return;

        trackingCollisions = false;

        if (other.gameObject.CompareTag(PocketDroidsConstants.TAG_DROID))
        {
            audioSource.PlayOneShot(successSound);

            Droid droid = other.gameObject.GetComponent<Droid>();
            droid.DroidHit(this.gameObject);
        }
        else
        {
            audioSource.PlayOneShot(dropSound);
        }
        
        Invoke(nameof(PowerDown), collisionStallTime);
    }
}
