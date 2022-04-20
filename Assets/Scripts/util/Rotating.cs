using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{

    public float velocity = 1f;
    
    private Transform t;
    
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        t.Rotate(0f, 0f, 90f * Time.deltaTime * velocity);
    }
}
