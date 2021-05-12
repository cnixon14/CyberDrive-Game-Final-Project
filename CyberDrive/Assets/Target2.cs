using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2 : MonoBehaviour
{
    public float health = 50f;
    public int scoreVal = 10;
    public bool remove;


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            ScoreScript.scoreValue += 10;
            Destroy(gameObject);
            if (remove)
            {
                AddToRoomCount2.RemoveCount();
                print(RoomChecker2.enemyCount);
            }
        }
    }
}
