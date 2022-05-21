using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevator;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        
        StartCoroutine(Up());
    }

    IEnumerator Up(){
        Vector3 newPos =new Vector3(elevator.transform.localPosition.x,20.0f,elevator.transform.localPosition.z);
        while(elevator.transform.position.y<=newPos.y){
            elevator.transform.position += Vector3.up * Time.deltaTime * 2.0f;
            yield return null;
        }
        
    }
}
