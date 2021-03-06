﻿using UnityEngine;
using System.Collections;

public class InstantiatePlayer : MonoBehaviour {

    // where should we instantiate?
    float posX = -1.934036f;
    float posY = 1.9265374f;
    
    // objects we'll need
    public Transform enemy;
    public Transform NormalNavi;
    Transform player;

    void Start()
    {
        // instantiate the player
        player = Instantiate(NormalNavi) as Transform;
        player.transform.parent = gameObject.transform;
        player.transform.position = new Vector3(posX, posY, 0);
        player.GetComponentInChildren<NormalNavi>().SetUpNNaviValues(1, enemy);
    }

}
