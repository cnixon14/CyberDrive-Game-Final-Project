using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public GameObject self;

    // Update is called once per frame
    void Update()
    {
        if (RoomChecker.enemyCount == 0)
        {
            Destroy(self);
        }
    }
}
