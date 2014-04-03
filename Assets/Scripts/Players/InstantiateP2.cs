using UnityEngine;
using System.Collections;

public class InstantiateP2 : MonoBehaviour {

    // where should we instantiate?
    float posX = 1.9f;
    float posY = 1.9265374f;

    // objects we'll need
    public Transform enemy;
    public Transform NormalNavi;
    Transform player;

    void Start()
    {
        // instantiate the player
        player = Instantiate(NormalNavi, new Vector3(posX, posY, 0), Quaternion.identity) as Transform;
        player.transform.parent = gameObject.transform;
        player.GetComponentInChildren<NormalNavi>().SetUpNNaviValues(2, enemy);
    }
}
