using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPositon;
    void Update()
    {
        transform.position = cameraPositon.position;    
    }
}
