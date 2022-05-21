using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public ProgressBar pb;
    public Text bullet_info;

    GunCtrl gunCtrl;
    PlayerCtrl playerCtrl;
    // Start is called before the first frame update
    void Start()
    {
        gunCtrl = GameObject.Find("Weapon").GetComponent<GunCtrl>();
        playerCtrl = GameObject.Find("Head").GetComponent<PlayerCtrl>();
        pb.BarValue = playerCtrl.player_HP;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStateUI();
    }

    void UpdateStateUI(){
        pb.BarValue = playerCtrl.player_HP;
        bullet_info.text = gunCtrl.currentBullet+"/"+gunCtrl.totalBullet;
    }
}
