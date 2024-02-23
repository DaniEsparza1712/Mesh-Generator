using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadGenerator : MonoBehaviour
{
    public GameObject quadPrefab;
    public float extrudeMagnitude;
    public Vector3[] points = new Vector3[4];
    
    public void ExtrudeCurve(Vector3[] bezierPoints, int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {

            var pos0 = EvalCurve(bezierPoints, i, iterations);
            var pos1 = EvalCurve(bezierPoints, i + 1, iterations);

            QuadC quad = Instantiate(quadPrefab).GetComponent<QuadC>();
            quad.transform.position = Vector3.zero;
            quad.CreateQuad(new Vector3[4]
            {
                pos0 - Vector3.right * extrudeMagnitude/2.0f,
                pos0 + Vector3.right * extrudeMagnitude/2.0f,
                pos1 -  Vector3.right * extrudeMagnitude/2.0f,
                pos1 + Vector3.right * extrudeMagnitude/2.0f
            });
            QuadC quadB = Instantiate(quadPrefab).GetComponent<QuadC>();
            quadB.transform.position = Vector3.zero;
            quadB.CreateQuad(new Vector3[4]
            {
                pos0 + Vector3.forward * extrudeMagnitude/2.0f,
                pos0 - Vector3.forward * extrudeMagnitude/2.0f,
                pos1 +  Vector3.forward * extrudeMagnitude/2.0f,
                pos1 - Vector3.forward * extrudeMagnitude/2.0f
            });
        }
    }

    public Vector3[] EvalSpline(Spline spline, int interpol, int iterations)
    {
        var pos0 = EvalCurve(new Vector3[]
        {
            spline.points[0],
            spline.handlesPos[0],
            spline.handlesPos[1],
            spline.points[1]
        }, interpol, iterations);
        
        var pos1 = EvalCurve(new Vector3[]
        {
            spline.points[1],
            spline.handlesPos[2],
            spline.handlesPos[3],
            spline.points[2]
        }, interpol, iterations);
        
        return new Vector3[]{pos0, pos1};
    }

    public Vector3 EvalCurve(Vector3[] curvePoints, int interpol, int iterations)
    {
        var change = (float)interpol / (float)iterations;

        var p1 = (Mathf.Pow(1-change, 3)) * curvePoints[0];
        var p2 = 3 * Mathf.Pow(1 - change, 2) * change * curvePoints[1];
        var p3 = 3 * (1 - change) * Mathf.Pow(change, 2) * curvePoints[2];
        var p4 = Mathf.Pow(change, 3) * curvePoints[3];

        var pos = p1 + p2 + p3 + p4;

        return pos;
    }

    public void GenerateQuad(Vector3[] positions)
    {
        QuadC quad = Instantiate(quadPrefab).GetComponent<QuadC>();
        quad.transform.position = Vector3.zero;
        quad.CreateQuad(positions);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
