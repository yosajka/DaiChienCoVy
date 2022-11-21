using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;
    private Vector2 gizmosPostion;
    private Helper helper;

    private void OnDrawGizmos()
    {
        Vector2 p0 = controlPoints[0].position;
        Vector2 p1 = controlPoints[1].position;
        Vector2 p2 = controlPoints[2].position;
        Vector2 p3 = controlPoints[3].position;
        Gizmos.color = Color.red;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPostion = Mathf.Pow(1 - t, 3) * p0 +
                            3 * Mathf.Pow(1 - t, 2) * t * p1 +
                            3 * (1 - t) * Mathf.Pow(t, 2) * p2 +
                            Mathf.Pow(t, 3) * p3;
            Gizmos.DrawSphere(gizmosPostion, 0.2f);
        }

        Gizmos.DrawLine(new Vector2(p0.x, p0.y),
                        new Vector2(p1.x, p1.y));

        Gizmos.DrawLine(new Vector2(p2.x, p2.y),
                        new Vector2(p3.x, p3.y));
    }
}
