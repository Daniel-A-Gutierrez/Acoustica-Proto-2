using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOrigin : MonoBehaviour
{
    public GameObject Note;
    //so our general flow: read from a file to instantiate a few big ol deques of notes.
    //add note objects to the deques back, so that when they are being read, they are read from the front.
    //each frame, check the time, and generate notes from the deque until their start time is past the current time. 
    //"automation notes" will contain a string corresponding to a function, and strings corresponding to args for those functions.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,1f)>.9f)
        {
            Instantiate(Note,Vector3.zero,Quaternion.identity);
        }
    }
}
