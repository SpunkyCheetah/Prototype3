using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startingPos;
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < startingPos.x - repeatWidth)
        {
            transform.position = startingPos;
        }
    }
}
