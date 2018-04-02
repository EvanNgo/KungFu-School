using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public GameObject currentObject;
    public InteractionObject currentObjectScript;
    Inventory inventory;

    void Start(){
        inventory = Inventory.instance;
    }

    void Update(){
        if (Input.GetButtonDown("Interact") && currentObject != null)
        {   
            if (currentObjectScript.inventory)
            {
                inventory.AddItem(currentObjectScript.itemTest, currentObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InterObject")
        {   
            currentObject = collision.gameObject;
            currentObjectScript = currentObject.GetComponent<InteractionObject>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InterObject")
        {   
            currentObject = collision.gameObject;
            currentObjectScript = currentObject.GetComponent<InteractionObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InterObject")
        {   
            if (collision.gameObject == currentObject)
            {
                currentObject = null;
                currentObjectScript = null;
            }
        }
    }
}
