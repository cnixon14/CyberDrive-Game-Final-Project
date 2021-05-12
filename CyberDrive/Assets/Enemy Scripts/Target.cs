
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public int scoreVal = 10;
    public bool remove;
    public bool remove2;


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            ScoreScript.scoreValue += 10;
            Destroy(gameObject);
            if (remove)
            {
                AddToRoomCount.RemoveCount();
                print(RoomChecker.enemyCount);
            }
            if (remove2)
            {
                AddToRoomCount2.RemoveCount();
                print(RoomChecker2.enemyCount);
            }
        }
    }

}
