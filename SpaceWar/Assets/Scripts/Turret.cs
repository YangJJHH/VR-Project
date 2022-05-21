using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    int damage =1;
    void OnParticleCollision(GameObject other) {
        if(other.tag=="Player"){
            //Debug.Log("damage");
            StartCoroutine(delayAttack(other));
        }
        
        
    }
    IEnumerator delayAttack(GameObject other){
        this.GetComponent<EnemyCtrl>().Attack(damage,other);
        yield return new WaitForSeconds(1.0f);
    }
    
}
