using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
    }
}
