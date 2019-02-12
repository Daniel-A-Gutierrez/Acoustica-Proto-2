using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteParent : MonoBehaviour
{
    [HideInInspector]
    public GameObject Origin;
    [HideInInspector]
    public EdgeManager Edges;
    public int position;//0-127
    public float SpawnTime;//this can be interpreted as beat
    public float tempo;
    public int beatLife;
    [NonSerialized]
    public bool setup = false;
    // Start is called before the first frame update
    protected void Start()
    {
        GameObject Origin= GameObject.Find("Origin");
        Edges = Origin.GetComponent<EdgeManager>();
        //for testing only
    }

    public void Setup(int position, float SpawnTime,float tempo, int beatLife)
    {
        this.position = position;
        this.SpawnTime = SpawnTime;
        this.tempo = tempo;
        this.beatLife = beatLife;
        setup = true;//flag for whether the note was properly set up
    }

    // Update is called once per frame
    protected void Update()
    {
        float progress = (Time.time - SpawnTime ) / (beatLife/tempo*60);//potential weirdness if not spawning at proper time(ie super speed)
        transform.position = Vector3.Lerp(Edges.originPoints[position],Edges.terminalPoints[position],progress);
        if(progress>1.1f) // fine tune for allowance for hitting notes, etc.
        {
            Destroy(gameObject);
        }
    }
}
