using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    GameObject player;
    int damage = 30;
    EnemyCtrl enemyCtrl;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Head");
        enemyCtrl=this.GetComponent<EnemyCtrl>();
        //플레이어 쪽으로 이동
        StartCoroutine(MoveToPlayer());
    }

    // Update is called once per frame
    
    IEnumerator MoveToPlayer(){
        //두 물체가 거의 근접할때까지 이동
        while((Vector3.Distance(this.transform.position,player.gameObject.transform.position))>=1.0f){
            this.transform.LookAt(player.transform.position);
            this.transform.position = Vector3.MoveTowards(this.transform.position,player.transform.position,0.05f);
            yield return null;
        }
        enemyCtrl.Attack(damage,player);
        enemyCtrl.HP=0;
        enemyCtrl.Attacked();
        
    }
}