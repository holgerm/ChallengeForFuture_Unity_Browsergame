using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camT;
    public Transform[] planes;
    
    [Range(0.05f, 10f)]
    public float velocity = 1f;

    [SerializeField]
    private float[] _scales;
    private Vector3 lastCamPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!camT)
            camT = Camera.main.transform;
        
        _scales = new float[planes.Length];
        
        for (int i=0; i<planes.Length; i++)
        {
            _scales[i] = (camT.position.z - planes[i].position.z);
        }

        lastCamPosition = camT.position;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<planes.Length; i++)
        {
            float xDelta = camT.position.x - lastCamPosition.x; // how much did cam move to the right?
            planes[i].position = new Vector3(
                    planes[i].position.x + (xDelta * velocity * 10f) / _scales[i], 
                    planes[i].position.y, 
                    planes[i].position.z);
        }
      
        lastCamPosition = camT.position;
    }
}
