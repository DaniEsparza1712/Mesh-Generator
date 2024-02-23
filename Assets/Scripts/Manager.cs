using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QuadGenerator))]
public class Manager : MonoBehaviour
{
    private QuadGenerator quadGenerator;
    public int xMeshIterations;
    public int zMeshIterations;
    public List<SplineContainer> XSplinesContainers;
    public List<SplineContainer> ZSplinesContainers;
    // Start is called before the first frame update
    void Start()
    {
        quadGenerator = GetComponent<QuadGenerator>();
        GenerateMesh();
    }

    /*private void ExtrudeSpline(Spline spline)
    {
        quadGenerator.ExtrudeCurve(new Vector3[4]
        {
            spline.points[0].position,
            spline.handlesPos[0],
            spline.handlesPos[1],
            spline.points[1].position
                
        }, iterations);
        quadGenerator.ExtrudeCurve(new Vector3[4]
        {
            spline.points[1].position,
            spline.handlesPos[2],
            spline.handlesPos[3],
            spline.points[2].position
                
        }, iterations);
    }*/

    private void GenerateMesh()
    {
        List<Spline> zSplines = new List<Spline>();
        List<Spline> xSplines = new List<Spline>();
        //Create zSplines
        foreach (var zSplineContainer in ZSplinesContainers)
        {
            Spline spline =  new Spline();
            spline.points = new Vector3[]
            {
                zSplineContainer.points[0].position,
                zSplineContainer.points[1].position,
                zSplineContainer.points[2].position
            };
            spline.GetHandles();
            zSplines.Add(spline);
        }
        //Create xSplines
        foreach (var xSplineContainer in XSplinesContainers)
        {
            Spline spline =  new Spline();
            spline.points = new Vector3[]
            {
                xSplineContainer.points[0].position,
                xSplineContainer.points[1].position,
                xSplineContainer.points[2].position
            };
            spline.GetHandles();
            xSplines.Add(spline);
        }

        for (int i = 0; i < zMeshIterations; i++)
        {
            //For first curve
            Spline zSpline = new Spline();
            Spline zSplineNext = new Spline();

            var zP1 = quadGenerator.EvalSpline(zSplines[0], i, zMeshIterations)[0];
            var zP2 = quadGenerator.EvalSpline(zSplines[1], i, zMeshIterations)[0];
            var zP3 = quadGenerator.EvalSpline(zSplines[2], i, zMeshIterations)[0];

            zSpline.points = new Vector3[]{zP1, zP2, zP3};
            zSpline.GetHandles();
            
            var zP1B = quadGenerator.EvalSpline(zSplines[0], i+1, zMeshIterations)[0];
            var zP2B = quadGenerator.EvalSpline(zSplines[1], i+1, zMeshIterations)[0];
            var zP3B = quadGenerator.EvalSpline(zSplines[2], i+1, zMeshIterations)[0];
            
            zSplineNext.points = new Vector3[]{zP1B, zP2B, zP3B};
            zSplineNext.GetHandles();
            
            //For second curve
            Spline zSplineSec = new Spline();
            Spline zSplineSecNext = new Spline();

            var zP1Sec = quadGenerator.EvalSpline(zSplines[0], i, zMeshIterations)[1];
            var zP2Sec = quadGenerator.EvalSpline(zSplines[1], i, zMeshIterations)[1];
            var zP3Sec = quadGenerator.EvalSpline(zSplines[2], i, zMeshIterations)[1];

            zSplineSec.points = new Vector3[]{zP1Sec, zP2Sec, zP3Sec};
            zSplineSec.GetHandles();
            
            var zP1BSec = quadGenerator.EvalSpline(zSplines[0], i+1, zMeshIterations)[1];
            var zP2BSec = quadGenerator.EvalSpline(zSplines[1], i+1, zMeshIterations)[1];
            var zP3BSec = quadGenerator.EvalSpline(zSplines[2], i+1, zMeshIterations)[1];
            
            zSplineSecNext.points = new Vector3[]{zP1BSec, zP2BSec, zP3BSec};
            zSplineSecNext.GetHandles();
            
            for (int j = 0; j < xMeshIterations; j++)
            {
                //For first curve
                var pos0 = quadGenerator.EvalSpline(zSpline, j, xMeshIterations)[0];
                var pos1 = quadGenerator.EvalSpline(zSpline, j + 1, xMeshIterations)[0];
                var pos2 = quadGenerator.EvalSpline(zSplineNext, j, xMeshIterations)[0];
                var pos3 = quadGenerator.EvalSpline(zSplineNext, j + 1, xMeshIterations)[0];
                
                quadGenerator.GenerateQuad(new Vector3[]
                {
                    pos0, pos1, pos2, pos3
                });
                
                //For second curve
                var pos0Sec = quadGenerator.EvalSpline(zSplineSec, j, xMeshIterations)[0];
                var pos1Sec = quadGenerator.EvalSpline(zSplineSec, j + 1, xMeshIterations)[0];
                var pos2Sec = quadGenerator.EvalSpline(zSplineSecNext, j, xMeshIterations)[0];
                var pos3Sec = quadGenerator.EvalSpline(zSplineSecNext, j + 1, xMeshIterations)[0];
                
                quadGenerator.GenerateQuad(new Vector3[]
                {
                    pos0Sec, pos1Sec, pos2Sec, pos3Sec
                });
                
                //For third curve
                var pos0Third = quadGenerator.EvalSpline(zSpline, j, xMeshIterations)[1];
                var pos1Third = quadGenerator.EvalSpline(zSpline, j + 1, xMeshIterations)[1];
                var pos2Third = quadGenerator.EvalSpline(zSplineNext, j, xMeshIterations)[1];
                var pos3Third = quadGenerator.EvalSpline(zSplineNext, j + 1, xMeshIterations)[1];
                
                quadGenerator.GenerateQuad(new Vector3[]
                {
                    pos0Third, pos1Third, pos2Third, pos3Third
                });
                
                //For fourth curve
                var pos0Four = quadGenerator.EvalSpline(zSplineSec, j, xMeshIterations)[1];
                var pos1Four = quadGenerator.EvalSpline(zSplineSec, j + 1, xMeshIterations)[1];
                var pos2Four = quadGenerator.EvalSpline(zSplineSecNext, j, xMeshIterations)[1];
                var pos3Four = quadGenerator.EvalSpline(zSplineSecNext, j + 1, xMeshIterations)[1];
                
                quadGenerator.GenerateQuad(new Vector3[]
                {
                    pos0Four, pos1Four, pos2Four, pos3Four
                });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
