using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Spline
{
    public Vector3[] points = new Vector3[3];
    public List<Vector3> handlesPos = new List<Vector3>();

    public void GetHandles()
    {
        List<Vector3> middleHandles = new List<Vector3>();
        //For middle handles
        for (int i = 1; i < points.Length - 1; i++)
        {
            var handleSize = Vector3.Distance(points[i - 1], points[i + 1])/6.0f;
            var handleDir = (points[i + 1] - points[i - 1]).normalized;
            //Left handle
            var handlePos = points[i] - (handleDir * handleSize);
            //Right handle
            var handlePos2 = points[i] + (handleDir * handleSize);
                
            middleHandles.Add(handlePos);
            middleHandles.Add(handlePos2);
        }
        
        //For first handle
        var handleHelperDir0 = (points[0] - points[1]).normalized;
        var handleHelperSize0 = Vector3.Distance(points[1], points[0])/3.0f;

        var handlePos0 = middleHandles[0] + (handleHelperDir0 * handleHelperSize0);
        handlesPos.Add(handlePos0);
        
        //For last handle
        var handleHelperDirLast = (points[points.Length - 1] - points[points.Length - 2]).normalized;
        var handleHelperSizeLast = Vector3.Distance(points[points.Length - 1], points[points.Length - 2])/3.0f;

        var handlePosLast = middleHandles[middleHandles.Count - 1] + (handleHelperDirLast * handleHelperSizeLast);
        
        //Add middle handles to list
        foreach (var middleHandle in middleHandles)
        {
            handlesPos.Add(middleHandle);
        }
        
        //Add last handle
        handlesPos.Add(handlePosLast);
    }

}
