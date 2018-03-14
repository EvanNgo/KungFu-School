using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    private bool isBoxOpen = false;
    public GameObject currentObject;
    public InteractionObject currentObjectScript;
    public Inventory inventory;

    void Update(){
        if (Input.GetButtonDown("Interact") && currentObject != null)
        {   
            if (currentObjectScript.inventory)
            {   
                inventory.AddItem(currentObject);
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
