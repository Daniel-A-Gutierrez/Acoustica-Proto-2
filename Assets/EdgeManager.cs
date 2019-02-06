using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    public LineRenderer olr;
    public LineRenderer tlr;
    public Vector3 gap ;
    public Vector3[] originPoints;
    public Vector3[] terminalPoints;
    // Start is called before the first frame update
    void Start()
    {
        olr = GetComponent<LineRenderer>();
        olr.positionCount = 128;
        tlr = transform.GetChild(0).GetComponent<LineRenderer>();
        tlr.positionCount = 128;

        originPoints = new Vector3[128];//this only works if the initial y is zero
        originPoints[0] = new Vector3(-2,0);
        originPoints[127] = new Vector3(2,0);

        terminalPoints = new Vector3[128];
        for(int i = 0 ; i < 128 ; i++)
        {
            terminalPoints[i] = originPoints[i]+ gap;
        }

        olr.SetPositions(originPoints);
        tlr.SetPositions(terminalPoints);
        terminal_horiz_line(10,4);
        origin_horiz_line(10,-4);
    }

    public void origin_horiz_line(float width, float y)
    {
        Vector3[] horiz_line = new Vector3[128];
        horiz_line[0] = new Vector3(-width/2,y);
        horiz_line[127] = new Vector3(width/2,y);
        for(int i = 0 ; i < 128 ; i++)
        {
            horiz_line[i] = Vector3.Lerp(horiz_line[0],horiz_line[127],i/127f);
        }
        StartCoroutine(ChangeShape(horiz_line,originPoints,olr,9));
    }

    public void terminal_horiz_line(float width, float y)
    {
        Vector3[] horiz_line = new Vector3[128];
        horiz_line[0] = new Vector3(-width/2,y);
        horiz_line[127] = new Vector3(width/2,y);
        for(int i = 0 ; i < 128 ; i++)
        {
            horiz_line[i] = Vector3.Lerp(horiz_line[0],horiz_line[127],i/127f);
        }
        StartCoroutine(ChangeShape(horiz_line,terminalPoints,tlr,9));
    }

    IEnumerator ChangeShape(Vector3[] newShape, Vector3[] originalShape, LineRenderer lr,float Seconds)//arrays are reference type so OK
    {
        float StartTime = Time.time;
        Vector3[] original = new Vector3[128];
        original = (Vector3[])originalShape.Clone();//shallow copy is OK for v3, probably
        while(Time.time - StartTime<Seconds)
        {
            for(int i = 0 ; i < 128; i++)
            {
                originalShape[i] = Vector3.Lerp(original[i],newShape[i],(Time.time-StartTime)/Seconds);
            }
            lr.SetPositions(originalShape);
            yield return null;
        }
        originalShape = (Vector3[])newShape.Clone();
        lr.SetPositions(originalShape);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
