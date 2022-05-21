using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject player;
    public Transform mainCam;
    

    public Image cursorGaugeImage;
     public Image cursor;
    public Image aim;
    private float guageTimer;
    //탐지할 범위
    public float range;

    //탐지한 물체정보 저장변수
    public string objectTag;

    //적을 조준할때는 움직이지 않게 하기위해서
    private bool isAiming =false;

    public Animator anim;

    RaycastHit hit;
    
    public int player_HP=100;

    SoundManager manager;
    


    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Hp")){
            player_HP = PlayerPrefs.GetInt("Hp");
        }
        manager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {

        DetectiveObjcet();
        Move();
    }

   

    public void Move(){

        //y방향으로는 이동하지 않기 위해 원래 y값 저장
        if(Input.GetMouseButton(0) && !isAiming){
            float y = this.transform.position.y;
            this.transform.Translate(mainCam.transform.forward * 0.1f);
            this.transform.position =  new Vector3(transform.position.x,y,transform.position.z);
            anim.SetBool("Run",true);
        }
        else{
            anim.SetBool("Run",false);
        }
        
        
       
        
    }

    public void DetectiveObjcet(){
                     
        Vector3 forward = mainCam.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(this.transform.position, forward, Color.blue);
        cursorGaugeImage.fillAmount = guageTimer;

        //UI와 탐지된 물체 초기화
        isAiming = false;
        cursor.gameObject.SetActive(true);
        aim.gameObject.SetActive(false);
        objectTag="Nothing";

        if(Physics.Raycast(this.transform.position, forward, out hit, range)){
            objectTag = hit.collider.tag;
            //상호작용 가능한 물체일 경우
            if(objectTag=="Interactive"){
                Interactive();
            }
            //적인 경우 게이지 커서를 크로스헤드로 바꿈
            else if(objectTag=="Enemy"){
                Aiming();
            }
            else{
                guageTimer =0.0f;
            }
        }else{
            guageTimer =0.0f;
        }
        
    }

    void Interactive(){
        guageTimer += 1.0f /2.0f * Time.deltaTime;
        if(guageTimer >= 1.0f){
            hit.transform.GetComponent<Interact>().React();
            guageTimer =0.0f;
        }
    }

    void Aiming(){
        isAiming = true;
        cursor.gameObject.SetActive(false);
        aim.gameObject.SetActive(true);
    }


    //레이에 감지된 물체 다른 스크립트에서 사용하기위해 getter
    public RaycastHit getHit(){
        return hit;
    }


    //공격당했을때
    public void Attacked(int damage){
        player_HP -= damage;
    }
}
