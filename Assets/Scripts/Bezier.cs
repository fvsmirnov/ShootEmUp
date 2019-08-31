using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    //Cubic Bezier function
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        return Mathf.Pow(oneMinusT, 3) * p0 +
                3f * t * oneMinusT * oneMinusT * p1 +
                3f * t * t * oneMinusT * p2 +
                Mathf.Pow(t,3) * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        //Qubic first derivative func
        return 3f * oneMinusT * oneMinusT * (p1 - p0) +
               6f * oneMinusT * t * (p2 - p1) +
               3 * t * t * (p3 - p2);
    }
}
