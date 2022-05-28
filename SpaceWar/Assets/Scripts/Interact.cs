using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public GameObject target_object;
    // Start is called before the first frame update
    SoundManager manager;
    SecondStage secondStage;
    PlayerCtrl playerCtrl;
    GunCtrl gunCtrl;

    // Start is called before the first frame update
    void Start(){
        manager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        if(SceneManager.GetActiveScene().name == "SecondScene"){
            secondStage = GameObject.Find("Map").GetComponent<SecondStage>();
        }
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
        gunCtrl = GameObject.Find("Weapon").GetComponent<GunCtrl>();
    }
    public void React(){
        string object_type = this.GetComponent<ObjectType>().object_Type;
        int object_index = this.GetComponent<ObjectType>().object_Index;
        switch(object_type){
            case "Door":
                manager.Play_Door();
                target_object.SetActive(false);
                break;
            case "Fuse":
                target_object.tag="Interactive";
                this.tag="Untagged";
                manager.Play_Spark();
                break;
            case "Key_Item":
                manager.Play_Interact();
                Destroy(target_object);
                break;
            case "Box":
                manager.Play_Interact();
                Destroy(this.gameObject);
                if(target_object != null){
                    target_object.SetActive(true);
                }
                break;
            case "Button":
                secondStage.input_password(object_index);
                break;
            case "Bullet":
                manager.Play_Interact();
                Destroy(this.gameObject);
                gunCtrl.totalBullet += 10;
                break;
            case "HealthPack":
                manager.Play_Interact();
                Destroy(this.gameObject);
                playerCtrl.player_HP += 10;
                if(playerCtrl.player_HP>=100){
                    playerCtrl.player_HP = 100;
                }
                break;
            case "Computer":
                manager.Play_Interact();
                target_object.GetComponent<Text>().text="Password : 0117";
                this.gameObject.tag="Untagged";
                break;
                

        }
        
         
    }

}
