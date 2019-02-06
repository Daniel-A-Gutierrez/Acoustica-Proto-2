using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteOrigin : MonoBehaviour
{
    public GameObject Note;
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
