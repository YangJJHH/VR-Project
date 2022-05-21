using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstStage : MonoBehaviour
{
    SoundManager manager;
    PlayerCtrl playerCtrl;
    public GameObject[] door;
    public GameObject fuse;
    public GameObject cctv;
    public GameObject fireFloor;

    public GameObject message_window;
    public GameObject player;
    
    bool isPass= false;
    bool isCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        manager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCheck) Check_Fuse_Off();
        Check_Respawn();
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag=="Player" && !isPass){
            manager.Play_Siren();
            isPass = true; //왔다갔다 할때마다 중복되서 소리재생되지 않게
            StartCoroutine(StartClose());
            StartCoroutine(FireFloorMove());
            
        }
    }

    //첫번째 문부터 문 마다 코루틴 통해 닫히는 동작
    IEnumerator StartClose(){
        int i=0;
        while(i<door.Length){
            StartCoroutine(DoorClosing(i));
            i++;
            yield return new WaitForSeconds(0.5f);
        }
        
      
    }
    //문이 옆에서부터 닫히게끔 동작
    IEnumerator DoorClosing(int i){
        while(door[i].transform.localPosition.z>0.01f){
            door[i].transform.localPosition += new Vector3(0,0,-0.008f);
            yield return null;
        }
    }

    //사이렌이 시작되면 불기둥이 플레이러를 쫓아오게
    IEnumerator FireFloorMove(){
        fireFloor.SetActive(true);
        while(fireFloor.transform.position.z<=player.transform.position.z){
            fireFloor.transform.position += new Vector3(0,0,0.03f);
            yield return null;
        }
    }

    void Check_Fuse_Off(){
        //fuse를 off시켰을때 cctv가 파괴되었느지 확인
        if(fuse.tag=="Untagged"){
           if(cctv != null){
               fireFloor.SetActive(true);
               manager.Play_Siren();
               isCheck = true;
           }
        }
        
    }

    void Check_Respawn(){
        //플레이어가 죽었을경우
        if(playerCtrl.player_HP <= 0){
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn(){
        message_window.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("FirstScene");
    }
}
