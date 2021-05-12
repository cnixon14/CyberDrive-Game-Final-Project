using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerStatus : MonoBehaviour
{
    [SerializeField] ScreenFlash fl = null;
    public float playerHP;
    public float maxHP = 100f;
    public barChange slider;
    public CapsuleCollider collider;
    public static bool buzzDamage = false;
    Color damageColor = Color.red;
    Color healColor = Color.green;
    public static bool isDamage = false;
    public GameObject playerHolder;
    public GameObject playerCamera;
    public AudioSource damageSound;

    // Start is called before the first frame update
    void Start()
    {
        buzzDamage = false; 
        playerHP = maxHP;
        slider.maxHealth(maxHP);
        playerHolder = GameObject.Find("Player");
        playerCamera = GameObject.Find("Main Camera");
        DeathScreenHandler.isDead = false;
        WinScreenHandler.hasWon = false;

    }

    // Update is called once per frame
    void Update()
    {
        projectileDamage(isDamage);

        if (Input.GetKeyDown(KeyCode.K))
        {
            //PlayerMovement.canControl = false;
            fl.TriggerFlash(.25f, .5f, damageColor);
        }

        if (playerHP <= 0f)
        {
            playerDie();
        }
    }

    public void playerTakeDamage(float damage)
    {
        playerHP -= damage;
        slider.sliderUpdate(playerHP);
        damageSound.Play();
    }


    public void playerDie()
    {
        Screen.lockCursor = false;
        playerHolder.GetComponent<PlayerMovement>().enabled = false;
        playerCamera.GetComponent<MouseLook>().enabled = false;
        DeathScreenHandler.isDead = true;
    }

    public void playerWin()
    {
        Screen.lockCursor = false;
        playerHolder.GetComponent<PlayerMovement>().enabled = false;
        playerCamera.GetComponent<MouseLook>().enabled = false;
        WinScreenHandler.hasWon = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "test_projectile(Clone)")
        {
            projectileStuff temp = collider.gameObject.GetComponent<projectileStuff>();
            float temp_damage = temp.damage;
            playerTakeDamage(10f);
            fl.TriggerFlash(.25f, .5f, damageColor);

        }
        
        if (collider.gameObject.tag == "Buzzblade" && buzzDamage)
        {
            //PlayerMovement.canControl = false;
            playerTakeDamage(20);
            buzzDamage = false;
            fl.TriggerFlash(.25f, .5f, damageColor);
        }

        if (collider.gameObject.tag == "Health" && playerHP < 100)
        {
            playerTakeDamage(-50);
            if (playerHP > 100)
            {
                playerHP = 100;
            }
            fl.TriggerFlash(.25f, .5f, healColor);
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.name == "Death Barrier")
        {
            playerTakeDamage(100);
            fl.TriggerFlash(.25f, .5f, damageColor);
            playerDie();
        }

        if (collider.gameObject.name == "Portal (1)")
        {
            playerWin();
        }

        if (collider.gameObject.name == "Volatile Energy")
        {
            playerTakeDamage(100);
            fl.TriggerFlash(.25f, .5f, damageColor);
            playerDie();
        }


    }

    public void projectileDamage(bool isDamage)
    {
        if (isDamage == true)
        {
            playerTakeDamage(10);
            isDamage = false;
        }
    }
}
