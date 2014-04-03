using UnityEngine;
using System.Collections;

public class AIInstantiate : MonoBehaviour {

    float posX = 1.9f;
    float posY = 1.9265374f;

    public Transform enemy;

    public Transform AINormalNavi;

    Transform player;

    void Start()
    {
        player = Instantiate(AINormalNavi, new Vector3(posX, posY, 0), Quaternion.identity) as Transform;
        player.transform.parent = gameObject.transform;
        player.GetComponentInChildren<NNaviAI>().SetUpNNaviValues(2, enemy);
    }
}
