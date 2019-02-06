using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParent : MonoBehaviour
{
    GameObject Origin;
    EdgeManager Edges;
    int position;//0-127
    float SpawnTime;
    public float tempo;
    public int beatLife;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Origin= GameObject.Find("Origin");
        Edges = Origin.GetComponent<EdgeManager>();
        //for testing only
        position = Random.Range(0,128);
        SpawnTime=  Time.time;
        beatLife= 4;
        tempo = 120;
        transform.position = Edges.originPoints[position];
    }

    // Update is called once per frame
    void Update()
    {
        float progress = (Time.time - SpawnTime ) / (beatLife/tempo*60);
        transform.position = Vector3.Lerp(Edges.originPoints[position],Edges.terminalPoints[position],progress);
        if(progress>1.1f) // fine tune for allowance for hitting notes, etc.
        {
            Destroy(gameObject);
        }
    }
}
