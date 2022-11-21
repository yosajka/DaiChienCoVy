using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public Vector2 LinearBezierCurves(float tParam, Vector2 p0, Vector2 p1)
    {
        return p0 + tParam * (p1 - p0);
    }
    public Vector2 QuadraticBezierCurves(float tParam, Vector2 p0,
                                        Vector2 p1, Vector2 p2)
    {
        return Mathf.Pow(1 - tParam, 2) * p0 +
                2 * (1 - tParam) * tParam * p1 +
                Mathf.Pow(tParam, 2) * p2;
    }

    public Vector2 CubicBezierCurves(float tParam, Vector2 p0, Vector2 p1,
                                    Vector2 p2, Vector2 p3)
    {
        return Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;
    }

    

}
