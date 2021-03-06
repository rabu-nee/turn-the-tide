﻿using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{

    public bool neverAllowInput = false;
    public float timeUntilActivation = 2f;
    public float rotationSpeed;
    public float movementSpeed = 2f;
    public float extraRotationAmount = 10f;
    public float velocityIntensity = 2f;
    public float joltTiming = 15f;
    public float turnAgainTiming = 1f;
    public float turnAgainTolerance = 5f;
    public string rotationSound;

    private Quaternion standardRotation;
    private Vector3 curEuler;
    private Vector3 desiredEuler;
    private Vector3 standardPosition;
    private Vector3 forwardAxis;
    private int curScreen = 1;
    private int lastDir;
    private bool buttonHit = false;
    private bool allowInput = true;
    private bool joltAdded = true;
    private bool shakeAdded = true;
    private bool addedExtraRotation = false;
    private bool waitFinished = false;
    private float elapsedTurnTime = 0;
    private float elapsedWaitTime = 0;
    private CameraEffects CamFX;

    //BUILT-IN FUNCTIONS===================================================================================================================

    void Start()
    {
        CamFX = Camera.main.GetComponent<CameraEffects>();

        standardRotation = transform.localRotation;
        desiredEuler = standardRotation.eulerAngles;
        curEuler = standardRotation.eulerAngles;
        standardPosition = transform.position;
        forwardAxis = transform.forward;
        waitFinished = (neverAllowInput) ? false : true;
    }

    void Update()
    {
        //Wait before turning (Menu)
        if (neverAllowInput && (elapsedWaitTime < timeUntilActivation))
        {
            elapsedWaitTime += Time.deltaTime;
        }
        else
        {
            checkAllowInput();
            if (!neverAllowInput)
            {
                controllerInput();
            }

            addCameraEffects();
            turnScreen();
            if (!waitFinished)
            {
                waitFinished = true;
                advanceScreen(1);
            }
        }
    }

    //CUSTOM FUNCTIONS===================================================================================================================

    public void advanceScreen(int dir)
    {
        //Check if last turn animation completed
        if (isDivBy(roundToMultiple(curEuler.z, 180, turnAgainTolerance), 180))
        {
            //Initiate screen turning
            elapsedTurnTime = 0;
            lastDir = dir;
            joltAdded = false;
            shakeAdded = false;
            addedExtraRotation = false;
            desiredEuler = addEulerRotation(desiredEuler, dir);
            resetOvershootRotation();
            curScreen = -curScreen;
            SoundManager.instance.PlaySound(rotationSound);
            if (!neverAllowInput)
            {
                addIndicatorArrow(GameObject.FindGameObjectWithTag("Player1").transform.parent.GetComponent<PlayerSelection>().getCurSelected());
            }
        }
    }

    private void turnScreen()
    {
        //Smooth out rotation speed based on Time (ease-in / ease-out)
        elapsedTurnTime += Time.deltaTime;
        float newRotSpeed = (elapsedTurnTime * elapsedTurnTime * elapsedTurnTime) * rotationSpeed;

        //Change camera rotation to frame current screen
        Vector3 extraRotation;
        if (((curEuler.z * lastDir) < ((desiredEuler.z * lastDir) + ((extraRotationAmount) * 0.9f))) && (!addedExtraRotation))
        {
            extraRotation = new Vector3(0, 0, extraRotationAmount * lastDir);
        }
        else
        {
            extraRotation = Vector3.zero;
            addedExtraRotation = true;
        }

        float slowDownTime;
        if (!addedExtraRotation)
        {
            slowDownTime = 1f;
        }
        else
        {
            slowDownTime = 0.1f;
        }

        curEuler = Vector3.Lerp(curEuler, desiredEuler + extraRotation, Time.deltaTime * newRotSpeed * slowDownTime);
        if (neverAllowInput)
        {
            transform.rotation = standardRotation;
            transform.Rotate(forwardAxis, curEuler.z);
        }
        else
        {
            Vector3 nRot;
            nRot = curEuler;
            transform.localRotation = Quaternion.Euler(nRot);
        }
    }

    private void resetOvershootRotation()
    {
        //Keep curEuler & desiredEuler as low as possible to avoid potential overflow
        while (Mathf.Abs(curEuler.z) >= 360)
        {
            curEuler.z -= (360 * getSign(curEuler.z));
            desiredEuler.z -= (360 * getSign(desiredEuler.z));
        }
    }

    public bool isActive()
    {
        return (neverAllowInput) ? waitFinished : true;
    }

    private void addIndicatorArrow(int pl)
    {
        IndicatorArrowSpawner ias = GameObject.Find("Players").GetComponent<IndicatorArrowSpawner>();
        if (ias != null)
        {
            ias.spawnIndicatorArrow(pl);
        }
    }


    private void addCameraEffects()
    {
        if ((elapsedTurnTime > turnAgainTiming) && (!shakeAdded))
        {
            shakeAdded = true;
            CamFX.addStandardCameraShake();
        }

        float eulerDifference = Mathf.Abs(Mathf.Abs(curEuler.z) - Mathf.Abs(desiredEuler.z));
        if ((eulerDifference < joltTiming) && (!joltAdded))
        {
            CamFX.addStandardCameraJolt();
            joltAdded = true;
        }
    }

    private void turnVelocity(int dir)
    {
        //Calculating Velocity direction
        Vector2 vel = new Vector2(1, 1);
        vel.x *= dir;
        vel *= velocityIntensity;

        //Adding velocity to Physics Objects
        GameObject[] po = GameObject.FindGameObjectsWithTag("PhysObj");
        if (po != null)
        {
            foreach (GameObject g in po)
            {
                int nDir = (int)Mathf.Clamp(g.GetComponent<Rigidbody2D>().gravityScale, -1, 1);
                g.GetComponent<Rigidbody2D>().AddForce(vel * nDir * 0.3f);
            }
        }
    }

    public bool checkAllowInput()
    {
        if (elapsedTurnTime > turnAgainTiming)
        {
            allowInput = true;
        }
        else
        {
            allowInput = false;
        }

        return allowInput;
    }

    private Vector3 addEulerRotation(Vector3 euler, int dir)
    {
        euler.z += 180 * (dir);

        return euler;
    }

    private int getSign(float num)
    {
        if (num >= 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    private bool isDivBy(float input, float div)
    {
        if ((input % div) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private float roundToMultiple(float input, float divider, float tolerance)
    {
        float[] multiplierPossibilities = new float[10];
        float closestDistance = divider;
        int returnIndex = 0;

        for (int i = 0; i < 5; i++)
        {
            multiplierPossibilities[i + 5] = divider * i;
            int negativeIndex = 4 - i;
            multiplierPossibilities[negativeIndex] = -multiplierPossibilities[i + 5];
        }

        for (int i = 0; i < 10; i++)
        {
            float curValue = multiplierPossibilities[i];
            float difference = Mathf.Abs(Mathf.Abs(curValue) - Mathf.Abs(input));

            if (difference < closestDistance)
            {
                closestDistance = difference;
                returnIndex = i;
            }
        }

        //ReturnConditions
        if (closestDistance < tolerance)
        {
            return multiplierPossibilities[returnIndex];
        }
        else
        {
            return input;
        }
    }

    //GETTER===================================================================================================================

    public int getCurScreen()
    {
        return curScreen;
    }

    public int getLastDir()
    {
        return lastDir;
    }

    //DEBUG===================================================================================================================

    void controllerInput()
    {
        //Add Input Handler!!
        if (allowInput)
        {

            //Turn Screen to the left
            if ((Input.GetAxis("L-Trigger") > 0) && (buttonHit == false))
            {
                advanceScreen(1);
                buttonHit = true;
            }

            //Turn Screen to the right
            if ((Input.GetAxis("R-Trigger") > 0) && (buttonHit == false))
            {
                advanceScreen(-1);
                buttonHit = true;
            }

            if ((Input.GetAxis("L-Trigger") == 0) && (Input.GetAxis("R-Trigger") == 0))
            {
                buttonHit = false;
            }




            if ((Input.GetKeyDown(KeyCode.X)) && (buttonHit == false))
            {
                advanceScreen(1);
                buttonHit = true;
            }

            //Turn Screen to the right
            if ((Input.GetKeyDown(KeyCode.C)) && (buttonHit == false))
            {
                advanceScreen(-1);
                buttonHit = true;
            }

            if (!(Input.GetKeyDown(KeyCode.X)) && !(Input.GetKeyDown(KeyCode.C)))
            {
                buttonHit = false;
            }

        }
    }

    public void setAllowInput(bool input)
    {
        allowInput = input;
    }
}