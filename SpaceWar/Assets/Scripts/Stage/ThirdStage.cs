using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdStage : MonoBehaviour
{
    public GameObject startTrigger;
    public Text timerUI;
    GameObject[] tiles;
    Transform[] fires;
    float timer = 0.0f;
    int maxObstacle = 100;
    int currentObstacle = 0;
    int maxTime = 90;
    bool[] isObstacle;
    bool isStart =false;
    Color defaultColor;
    // Start is called before the first frame update
    void Start()
    {
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
        if(startTrigger == null){
            Timer();
            isStart=true;
        }
        
    }
    void Timer(){
        timer += Time.deltaTime;
        timerUI.text="Survive\n"+(maxTime-(int)timer);
    }

    void MakeObstacle(){
        int index;
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
    }
    void DeleteObstacle(){
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                tiles[i].GetComponent<Renderer>().material.color = defaultColor;
                isObstacle[i]=false;
                currentObstacle--;
            }
        }
    }

    IEnumerator FireStart(){
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                fires[i].gameObject.SetActive(true);
            }
        }
        yield return new WaitForSeconds(5.0f);
        for(int i=0; i<isObstacle.Length; i++){
            if(isObstacle[i]){
                fires[i].gameObject.SetActive(false);
            }
        }
    }

    IEnumerator StartStage(){
        while(true){
            if(timer>=maxTime){
                break;
            }
            if(isStart){
                Timer();
                
                yield return new WaitForSeconds(3.0f);
                MakeObstacle();
                yield return new WaitForSeconds(3.0f);
                StartCoroutine(FireStart());
                yield return new WaitForSeconds(6.0f);
                DeleteObstacle();
            }
            else{
                yield return null;
            }
        }
        yield return null;
    }
}
