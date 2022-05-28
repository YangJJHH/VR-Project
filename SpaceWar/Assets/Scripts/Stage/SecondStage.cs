using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SecondStage : MonoBehaviour
{
    public GameObject[] trapTrigger;

    public GameObject[] trapFloor;

    public GameObject player;

    public GameObject message_window;

    public GameObject door;
    public GameObject spotLight;
    public GameObject battery;
    public GameObject flashLight;

    string password="0117";
    string password_input="";

    public GameObject[] Obstacle;

    public Text textUI;
    Vector3 pos;
    SoundManager soundManager;
    PlayerCtrl playerCtrl;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
        StartCoroutine(FlashLight());
    }

    // Update is called once per frame
    void Update()
    {
        Trap();
        Check_Respawn();
    }



    public void input_password(int num){
        //확인 버튼
        if(num == 10){
            if(password.Equals(password_input)){
                password_input="Door Gaze";
                door.tag="Interactive";
                soundManager.Play_Correct();
            }
            else{
                soundManager.Play_Wrong();
            }
        }
        //취소버튼
        else if(num == 11){
            password_input ="";
        }
        else{
            soundManager.Play_Interact();
            password_input+=num.ToString();
        }
        textUI.text=password_input;
    }

    void Check_Respawn(){
        //플레이어가 죽었을경우
        if(player.transform.position.y <= -20){
            StartCoroutine(Respawn());
        }
    }
      IEnumerator Respawn(){
        message_window.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("SecondScene");
    }
    void Trap(){
        //함정 아이템 박스 작동
        if(trapTrigger[0] == null){
            TrapFloor(0);
        }
        else if(trapTrigger[1] == null){
            TrapFloor(1);
        }
        else if(trapTrigger[2] == null){
            for(int i=0; i<Obstacle.Length; i++){
                Destroy(Obstacle[i]);
            }
        }
    }
    void TrapFloor(int index){
        pos = new Vector3(0,0,5) + trapFloor[index].transform.position;
        trapFloor[index].transform.position = Vector3.Lerp(trapFloor[index].transform.position,pos,1.0f );
        
    }

    IEnumerator FlashLight(){
        while(battery!=null || flashLight !=null){
            yield return null;
        }
        spotLight.SetActive(true);
    }

    


}
