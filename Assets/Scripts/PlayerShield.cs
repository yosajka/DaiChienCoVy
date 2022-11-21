using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject shield;
    private float shieldTime;// shieldTime of player
    private readonly float shieldTimeUnit = 4f; // each speedBoostItem will create a shield in 4 second.
    private bool activate; // true if player is in shieldTime
    private float activatedTime;

    // Start is called before the first frame update

    private void Init()
    {
        activate = false;
        shieldTime = 0f;
        activatedTime = 0f;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            // UpdatePosition();
            activatedTime += Time.deltaTime;
            // if out off shieldTime => turn off
            if (activatedTime > shieldTime) 
            {
                activate = false;
                activatedTime = 0f;
                shieldTime = 0f;
                shield.SetActive(false);
            }
        }
    }

    public bool ActivateShield
    {
        get
        {
            return activate;
        }
        set
        {
            if (activate == false)
            {
                activate = true;
                shield.SetActive(true);
            }
            shieldTime += shieldTimeUnit;
        }
    }
}
