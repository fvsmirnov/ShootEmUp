using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public int lineSteps = 10;
    public Transform[] waypoints = new Transform[4];

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        if (waypoints != null)
        {
            Vector3 startPos = Bezier.GetPoint(waypoints[0].position, waypoints[1].position, 
                                            waypoints[2].position, waypoints[3].position, 0f);

            for (int i = 1; i <= lineSteps; i++)
            {
                Vector3 endPos = Bezier.GetPoint(waypoints[0].position, waypoints[1].position,
                                              waypoints[2].position, waypoints[3].position, i / (float)lineSteps);

                Gizmos.DrawLine(startPos, endPos);
                startPos = endPos;
            }
        } 
    }

    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetFirstDerivative(waypoints[0].position, waypoints[1].position,
                                                                  waypoints[2].position, waypoints[3].position, t)) - transform.position;
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
}
