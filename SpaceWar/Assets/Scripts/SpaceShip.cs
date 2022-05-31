using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShip : MonoBehaviour
{

    float cur_angle;
    float prev_angle;
    float delta_angle;

    public GameObject head;
    public GameObject mainCamera;
    public GameObject cloneObject;

    public GameObject ship;

    

    
    // Start is called before the first frame update
    void Start()
    {
        head.SetActive(false);
        mainCamera.SetActive(true);
        Destroy(cloneObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move(){
        //수업자료 참고
        Debug.Log("이동");
        this.transform.Translate(mainCamera.transform.forward * 0.1f);

        cur_angle = mainCamera.transform.eulerAngles.y;
        delta_angle = cur_angle - prev_angle;
        prev_angle = cur_angle;

        if(delta_angle < 0){
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(ship.transform.eulerAngles.x, ship.transform.eulerAngles.y, 45), Time.deltaTime);
        }

        else if(delta_angle > 0){
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(ship.transform.eulerAngles.x, ship.transform.eulerAngles.y, -45), Time.deltaTime);
        }

        else{
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(ship.transform.eulerAngles.x, ship.transform.eulerAngles.y, 0), Time.deltaTime);
        }
    }
}
