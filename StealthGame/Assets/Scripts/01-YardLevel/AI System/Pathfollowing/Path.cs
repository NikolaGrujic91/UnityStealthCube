using UnityEngine;
using System.Collections;
using System;

public class Path : MonoBehaviour {
    /// <summary>
    /// If the debug mode property is checked,
    /// the path formed by the positions entered will be drawn as   
    /// gizmos in the editor window
    /// </summary>
    public bool bDebug = true;

    /// <summary>
    /// The radius property is a range value for the path
    /// following entities to use so that they can know when they've reached a particular
    /// waypoint if they are in this radius range.
    /// </summary>
    public float Radius = 2.0f;

    public Vector3[] pointA;

    /// <summary>
    /// Length property that returns the length and size of the waypoint array
    /// </summary>
    public float Length
    {
        get { return pointA.Length; }
    }

    /// <summary>
    /// The GetPoint method returns the 
    /// Vector3 position of a particular waypoint at a specified index in the array.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector3 GetPoint(int index)
    {
       return pointA[index];
    }

    public Vector3[] Reverse()
    {
        Vector3[] reverse = new Vector3[pointA.Length];

        int j = 0;
        for(int i = pointA.Length-1; i>=0; i--)
        {
            reverse[j] = pointA[i];
            j++;
        }

        pointA = reverse;
        return pointA;
    }

    /// <summary>
    /// method that is called by Unity3D frame to draw components
    /// in the editor environment.
    /// </summary>
    void OnDrawGizmos()
    {
        if (!bDebug)
            return;

        for (int i = 0; i < pointA.Length; i++)
            if (i + 1 < pointA.Length)
                Debug.DrawLine(pointA[i], pointA[i + 1], Color.red);
    }
}
