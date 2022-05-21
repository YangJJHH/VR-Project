using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public GameObject key;
    public GameObject CanOpenLight;
    public string nextScene;
    bool haveKey = false;
    PlayerCtrl playerCtrl;
    GunCtrl gunCtrl;
    // Start is called before the first frame update
    void Start(){
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
        gunCtrl = GameObject.Find("Weapon").GetComponent<GunCtrl>();
    }
    void Update(){
        if(!haveKey) CheckKey();
    }
    void OnTriggerEnter(Collider other) {
        if(other.tag=="Player"){
            if(haveKey){
                //다음씬으로 넘어가기전 플레이어의 정보 저장
                PlayerPrefs.SetInt("Hp",playerCtrl.player_HP);
                PlayerPrefs.SetInt("TotalBullet",gunCtrl.totalBullet);
                PlayerPrefs.SetInt("CurrentBullet",gunCtrl.currentBullet);
                PlayerPrefs.Save();
                SceneManager.LoadScene(nextScene);
                //Debug.Log("NextStage");
            }
        }
    }
    void CheckKey(){
        if(key==null){
            haveKey = true;
            CanOpenLight.SetActive(true);
        }
    }
}
