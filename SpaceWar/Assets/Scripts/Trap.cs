using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    SoundManager soundManager;
    void Start(){
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
        soundManager.Play_Fall();
    }

}
