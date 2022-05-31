using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipDestroy : MonoBehaviour
{
    public GameObject destroyEffect;
    public GameObject[] message_window;
    public GameObject mainCamera;
    bool isCollision = false;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other) {
        if(!isCollision){
            mainCamera.GetComponent<SoundManager>().Play_Boomb();
            GameObject effect = Instantiate(destroyEffect,gameObject.transform.position,Quaternion.identity);
            Destroy(effect,1.0f);
            isCollision  = true;
            StartCoroutine(Respawn());
        }
    }

     IEnumerator Respawn(){
        message_window[0].SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("FourthScene");
    }

    private void OnTriggerEnter(Collider other) {
        message_window[1].SetActive(true);
    }
}
