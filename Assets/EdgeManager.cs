using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeManager : MonoBehaviour
{
    public LineRenderer olr;
    public LineRenderer tlr;
    public Vector3 gap;
    public Vector3[] originPoints;
    public Vector3[] terminalPoints;

    public LINE origin;
    public LINE terminal;


    public struct LINE
    {
        public Vector3[] points;
        public LineRenderer lr;
        public int offset;

        public LINE(Vector3[] points, LineRenderer lr)
        {
            this.points = points;
            this.lr = lr;
            offset = 0;
        }

        public void SetPositions()
        {
            lr.SetPositions(points);
        }

        public void Cycle()
        {
            offset++;
            Vector3 zero = points[0];
            for (int i = 0; i < 127; i++)
            {
                points[i] = points[i + 1];
            }
            points[127] = zero;
        }
    }
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

        LINE origin = new LINE(originPoints, olr);
        LINE terminal = new LINE(terminalPoints, tlr);
        horiz_line(10,4,terminal);
        horiz_line(10,-4,origin);
    }

    //function for a horizontal line
    public void horiz_line(float width,float y,LINE line)
    {
        Vector3[] horiz_line = new Vector3[128];
        horiz_line[0] = new Vector3(-width / 2, y);
        horiz_line[127] = new Vector3(width / 2, y);
        for (int i = 0; i < 128; i++)
        {
            horiz_line[i] = Vector3.Lerp(horiz_line[0], horiz_line[127], i / 127f);
        }
        StartCoroutine(ChangeShape(horiz_line, line, 9));
    }



    //this function is for looping forward a raw array. 
    //DO NOT use for the arrays in LINE objects, use the structs function to track the offset.
    public void Cycle(Vector3[] points)
    {
        Vector3 zero = points[0];
        for(int i = 0 ; i < 127 ; i++)
        {
            points[i] = points[i+1];
        }
        points[127] = zero;
        //lr.SetPositions(points);
    }

    IEnumerator ChangeShape(Vector3[] newShape,LINE line,float Seconds)
    {
        float StartTime = Time.time;
        while(Time.time - StartTime < Seconds)
        {
            for(int i = 0 ; i < 128; i++)
            {
                
                line.points[i] = Vector3.Lerp(line.points[i],newShape[i], (Time.time-StartTime)/(Seconds+Time.time-StartTime)/Seconds/3);
                
            }
            line.SetPositions();
            yield return null;
        }
        line.points = newShape;
        line.SetPositions();
    }

    IEnumerator ChangeAndCycle(Vector3[] newShape, LINE line, float Seconds)
    {
        float StartTime = Time.time;
        for (int i = 0; i < line.offset; i++)
            Cycle(newShape);
        while (Time.time - StartTime < Seconds)
        {
            Cycle(line.points);
            Cycle(newShape);
            for (int i = 0; i < 128; i++)
            {

                line.points[i] = Vector3.Lerp(line.points[i], newShape[i], (Time.time - StartTime) / (Seconds + Time.time - StartTime) / Seconds / 3);

            }
            line.SetPositions();
            yield return null;
        }
        line.points = newShape;
        line.SetPositions();
    }


    public void Update()
    {
       // Cycle(terminalPoints,tlr);
    }

}
