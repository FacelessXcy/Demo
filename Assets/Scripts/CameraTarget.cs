using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public float speed;
    public bool isMoving;
    
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(new Vector3(1,0,0)*speed);
        }
        
    }
}
