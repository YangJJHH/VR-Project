using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    public GameObject door;
    void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
        door.SetActive(true);
        door.tag="Untagged";
    }
}
