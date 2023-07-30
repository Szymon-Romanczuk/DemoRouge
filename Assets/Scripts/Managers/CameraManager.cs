using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float Speed;
    [SerializeField] private Transform _cam;

    void Start(){
        Speed = 0.01f;
    }
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal") * Speed;
        float zAxisValue = Input.GetAxis("Vertical") * Speed;

        _cam.transform.position = new Vector3(_cam.transform.position.x + xAxisValue, _cam.transform.position.y + zAxisValue, _cam.transform.position.z);
        //Debug.Log("Speed? " + Speed);
    }
}