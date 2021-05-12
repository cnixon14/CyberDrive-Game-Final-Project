using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnabler : MonoBehaviour
{
    List<GameObject> children;
    public Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        children = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            children.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {

        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.name == "Player")
        {
            Debug.Log("Player has triggered");
            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.SetActive(true);
                Debug.Log(child.name);
                Debug.Log("Activated");
            }
        }
    }
}
