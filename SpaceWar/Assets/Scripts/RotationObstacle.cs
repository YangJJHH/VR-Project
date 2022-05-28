using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObstacle : MonoBehaviour
{
    int randomRotate;
    void Start(){
        randomRotate = Random.Range(0,3);
    }
    // Update is called once per frame
    void Update()
    {
        switch(randomRotate){
            case 0:
                this.transform.Rotate(Vector3.down *10.0f *Time.deltaTime);
                break;
            case 1:
                this.transform.Rotate(Vector3.right *10.0f *Time.deltaTime);
                break;
            case 2:
                this.transform.Rotate(Vector3.up *10.0f *Time.deltaTime);
                break;
        }
        
    }
}
