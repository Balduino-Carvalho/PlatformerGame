using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if(player != null)
        {
            Vector3 playerPos = transform.position;
            playerPos.z = 0f;
            player.position = transform.position;
        }
    }

    
}
