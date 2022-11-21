using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 goPosition;
    private float speedModifier;
    private bool coroutineAllowed;
    private Helper helper;

    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.5f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoutine(routeToGo));
    }

    private IEnumerator GoByTheRoutine(int routeNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            goPosition = helper.CubicBezierCurves(tParam, p0, p1, p2, p3);
            transform.position = goPosition;
            yield return new WaitForEndOfFrame();
        }
        tParam = 0f;
        routeToGo += 1;
        if (routeToGo == routes.Length)
        {
            routeToGo = 0;
        }
        coroutineAllowed = true;
    }
}
