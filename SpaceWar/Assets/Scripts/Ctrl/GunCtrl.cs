using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{
    private float currentFireRate;
    //연사속도 제한 변수
    private float fireRate = 0.2f;

    public GameObject bulletSpawn;
    public GameObject muzzleFlash;
    public GameObject SpawnBullet;
    public AudioSource audioSource;

    public AudioClip[] gunSound = new AudioClip[3];


    public GameObject gun;

    //현재 장정된 총알갯수와 보유 총알수
    public int currentBullet;
    public int totalBullet;

    //리로드 애니메이션 위한 애니메이터
    public Animator anim;

    private bool isReload = false;
    PlayerCtrl playerCtrl;

 
 
    void Start()
    {
        if(PlayerPrefs.HasKey("TotalBullet")){;
            totalBullet = PlayerPrefs.GetInt("TotalBullet");
        }
        if(PlayerPrefs.HasKey("CurrentBullet")){
            currentBullet = PlayerPrefs.GetInt("CurrentBullet");
        }
        //플레이어가 탐지한 물체를 확인하기위해서 PlayerCtrl 스크립트 연결
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
      
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        
    }
    void GunFireRateCalc(){
        //연사속도
        if(currentFireRate > 0){
            currentFireRate -=Time.deltaTime;
        }
    }
    void TryFire(){
        //Debug.Log(objectTag);
        // 총을 쏠수있는 조건
        if(Input.GetMouseButton(0) && currentFireRate<=0 && (playerCtrl.objectTag == "Enemy")){
            if(currentBullet<=0){
               StartCoroutine(Reload());
            }
            else if(!isReload){
                Fire();
            }
            
        }
        
    }

    void Fire(){
        currentFireRate = fireRate;
        StartCoroutine(Recoil()); //반동 코루틴
        Shoot();
    }
    void Shoot(){
        //Debug.Log("총알 발사함");
        //총 섬광 이펙트 
        Instantiate(muzzleFlash,bulletSpawn.transform.position, bulletSpawn.transform.rotation * Quaternion.Euler(0,0,90));
        StartCoroutine(makeLine());
        Hit();
        audioSource.clip = gunSound[0];
        PlayshootSound();
        currentBullet--;
        
    }

    //적이 맞았을 경우
    void Hit(){
        //공격당한 애니메이션 실행
        Transform enemy = playerCtrl.getHit().transform;
        enemy.GetComponent<EnemyCtrl>().Attacked();
        //Debug.Log("맞았음");

    }

    void PlayshootSound(){

        audioSource.Play();
    }

    //총기반동 구현
    IEnumerator Recoil(){
        Vector3 originPos = gun.transform.localPosition;
        Vector3 recoilPos = new Vector3(gun.transform.localPosition.x,gun.transform.localPosition.y,gun.transform.localPosition.z+0.06f);
        //총기가 앞으로 가는 반동
        while(gun.transform.localPosition.z <= recoilPos.z - 0.001f){
            gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition,recoilPos,0.4f);
            yield return null;
        }
        //다시 원래자리로
        while(gun.transform.localPosition.z <= originPos.z + 0.001f){
            gun.transform.localPosition = Vector3.Lerp(gun.transform.localPosition,originPos,0.1f);
            yield return null;
        }
        gun.transform.localPosition = originPos;
    }

    IEnumerator Reload(){
        isReload = true;
        if(totalBullet>0){
            if(totalBullet<30){
                currentBullet = totalBullet;
                totalBullet = 0;
            }
            else{
                currentBullet = 30;
                totalBullet = totalBullet -30;
            }
            audioSource.clip = gunSound[1];
            PlayshootSound();
            anim.SetTrigger("Reload");
            //재장전중 총이 발사되는것을 막기위해 딜레이 추가
            yield return new WaitForSeconds(2.0f);
            isReload = false;
        }
        else{
            isReload = false;
        }

    }

    //line renerer로 총알궤적 표시
    IEnumerator makeLine(){
        SpawnBullet.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SpawnBullet.SetActive(false);
    }
    
}
