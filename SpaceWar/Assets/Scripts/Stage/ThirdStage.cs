using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdStage : MonoBehaviour
{
    public GameObject key;
    public GameObject hpItem;
    public GameObject player;
    public GameObject message_window;
    public GameObject startTrigger;
    public Text timerUI;
    GameObject[] tiles;
    Transform[] fires;
    float timer = 0.0f;
    int maxObstacle = 100;
    int currentObstacle = 0;
    int maxTime = 120;
    bool[] isObstacle;
    bool isStart =false;
    Color defaultColor;
    PlayerCtrl playerCtrl;
    SoundManager soundManager;
    //함정이 생기기까지 걸리는 시간을 통제할 변수(난이도 조절용)
    float activeTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
        tiles = GameObject.FindGameObjectsWithTag("Tile"); //24X24 576

        defaultColor = tiles[0].GetComponent<Renderer>().material.color; 

        isObstacle = new bool[tiles.Length];
        fires = new Transform[tiles.Length];
        for(int i=0; i<isObstacle.Length; i++){
            isObstacle[i] = false;
            fires[i] = tiles[i].transform.GetChild(0);
        }
        
        StartCoroutine(StartStage());
        
    }

    // Update is called once per frame
    void Update()
    {  
        if(startTrigger == null){ //플레이어가 문을 넘어서 스테이지에 진입했을 때 부터 타이머 시작
            Timer();
            isStart=true;
        }
        Check_Respawn();
        
    }
    void Timer(){
        timer += Time.deltaTime;
        timerUI.text="Survive\n"+(maxTime-(int)timer);
         if(timer>=maxTime){
            Destroy(key.gameObject);
            timerUI.text = "Survive\nSuccess";
        }
       
       //시간에 따라 난이도 상승
        if(timer>=100.0f){
            maxObstacle = 500; 
        }
        else if(timer>=90.0f){
            maxObstacle = 400; 
        }
        else if(timer>=70.0f){
            activeTime = 1.0f;
            maxObstacle = 250;
        }
        else if(timer>=50.0f){
            maxObstacle = 150;
        }
        else if(timer>=20.0f){
            activeTime = 2.0f;
            maxObstacle = 120;
        }
    }

    void MakeObstacle(){  // 함정이 생길 바닥타일을 랜덤한 패턴으로 정하고 색깔을 빨간색으로 바꾸어 미리 위험을 알려준다.
        int index;
        soundManager.Play_Stage();
        while(currentObstacle<maxObstacle){
            for(int i=0; i<maxObstacle; i++){
                index=Random.Range(0,tiles.Length);
                if(!isObstacle[index]){
                    tiles[index].GetComponent<Renderer>().material.color = Color.red;
                    isObstacle[index]=true;
                    currentObstacle++;
                }
            }  
        }
        SpawnItem();
    }
    void DeleteObstacle(){ // 다음 패턴을 위해 바닥색을 지우고 초기화 시키는 작업
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                tiles[i].GetComponent<Renderer>().material.color = defaultColor;
                isObstacle[i]=false;
                currentObstacle--;
            }
        }
    }

    IEnumerator FireStart(){ //발깐색으로 장해진 바닥에 불기둥 오브젝트 활성화
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                fires[i].gameObject.SetActive(true);
            }
        }
        yield return new WaitForSeconds(5.0f);  //5초동안 활성화 시키고 다시 비활성화
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                fires[i].gameObject.SetActive(false);
            }
        }
    }
    void SpawnItem(){
        int index  =Random.Range(0,576);
        int random = Random.Range(0,10); //10분의 1확률로 아이템 생성
        if(random == 7){ //7은 확률을 위한 임의의 숫자 
            Instantiate(hpItem,tiles[index].transform.position,Quaternion.identity);
        }
    }

    //발판 제거 함정
    IEnumerator DeleteFloor(){
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                tiles[i].gameObject.SetActive(false);
            }
        }
        yield return new WaitForSeconds(5.0f);  //5초동안 활성화 시키고 다시 비활성화
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                tiles[i].gameObject.SetActive(true);
            }
        }
    }

    IEnumerator StartStage(){
        int index;
        while(true){
            if(timer>=maxTime){
                break;
            }
            if(isStart){
                Timer();
                
                yield return new WaitForSeconds(activeTime);
                MakeObstacle();
                yield return new WaitForSeconds(activeTime);
                index = Random.Range(0,2);
                //70초 이상부터는 바닥제거 함정
                if(timer >= 60.0f){
                    StartCoroutine(DeleteFloor());
                }
                else{
                    StartCoroutine(FireStart());
                }
                yield return new WaitForSeconds(5.0f);
                DeleteObstacle();
            }
            else{
                yield return null;
            }
        }
        yield return null;
    }

    

    void Check_Respawn(){
        //플레이어가 죽었을경우
        if(playerCtrl.player_HP<= 0 || player.transform.position.y<=-10){
            StartCoroutine(Respawn());
        }
        
    }
      IEnumerator Respawn(){
        message_window.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("ThirdScene");
    }
}
