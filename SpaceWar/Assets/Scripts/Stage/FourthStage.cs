using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourthStage : MonoBehaviour
{
    public GameObject message_window;
    public GameObject floor;
    public GameObject boomb;
    public GameObject player;
    public GameObject[] moveObstacle;

    public GameObject drone;
    PlayerCtrl playerCtrl;
    int index;
    float x,y,z;
    Vector3 newPos;
    bool move = true;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl  = player.GetComponent<PlayerCtrl>();
        StartCoroutine(Move());
        StartCoroutine(MoveObstacle());
        
    }

    // Update is called once per frame
    void Update()
    {
        Check_Respawn();
    }

    IEnumerator Move(){
        //가스통을 터트리면 발판이 움직이게
        while(move){
            if(boomb==null){
                floor.transform.Translate(Vector3.forward * Time.deltaTime*2.0f, Space.World);
                //floor.transform.Rotate(Vector3.right * Time.deltaTime * 2.0f);
            }
            yield return null;
        }
    }

    IEnumerator MoveObstacle(){
        while(move){
            if(boomb==null){
                index = Random.Range(0,moveObstacle.Length);
                x = player.transform.position.x + Random.Range(-5,5);
                y = player.transform.position.y + Random.Range(-5,5);
                z = player.transform.position.z + Random.Range(20,30);
                newPos = new Vector3(x,y,z);
                GameObject newobj = Instantiate(moveObstacle[index],newPos,Quaternion.Euler(x,y,z));
                SpawnDrone();
                yield return new WaitForSeconds(5.0f);
            }
            yield return null;
        }
    }

    void Check_Respawn(){
        //플레이어가 죽었을경우
        if(playerCtrl.player_HP<= 0 || player.transform.position.y<=-60){
            StartCoroutine(Respawn());
        }
        
    }
    void SpawnDrone(){
        for(int i=0; i<1; i++){
            x = player.transform.position.x + Random.Range(-20,20);
            y = player.transform.position.y + Random.Range(-1,20);
            z = player.transform.position.z + Random.Range(-20,20);
            newPos = new Vector3(x,y,z);
            Instantiate(drone,newPos,Quaternion.identity);
        }
    }
      IEnumerator Respawn(){
        message_window.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("FourthScene");
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Finish"){
            move =false;
        }
    }
}
