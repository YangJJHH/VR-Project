using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.Translate(player.transform.position*10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
