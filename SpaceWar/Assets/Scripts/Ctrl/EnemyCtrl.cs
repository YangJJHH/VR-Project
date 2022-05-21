using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    public Animator anim;
    public int HP = 20;
    public GameObject destroyEffect;
    SoundManager manager;

    // Start is called before the first frame update
    void Start(){
        manager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
    }
    public void Attacked(){
        if(HP<=0){
            manager.Play_Attack();
            Destroy(this.gameObject);
            GameObject effect = Instantiate(destroyEffect,gameObject.transform.position,Quaternion.identity);
            Destroy(effect,1.0f);
            
        }
        if(anim!=null){
            anim.SetTrigger("Attack");
        }
        
        HP-=5;
    }

    public void Attack(int damage, GameObject player){
        player.GetComponent<PlayerCtrl>().Attacked(damage);
        player.GetComponent<Rigidbody>().AddForce(player.transform.forward *  20.0f);
    }

  
}
