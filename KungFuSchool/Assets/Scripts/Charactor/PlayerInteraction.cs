using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public GameObject currentObject;
    public InteractionObject currentObjectScript;
    Inventory inventory;
    public float rangeForTakeItem;
    void Start(){
        inventory = Inventory.instance;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update(){
        if (currentObject != null && currentObjectScript!=null)
        {   
            if (currentObjectScript.itemType == Item.ItemType.Gold)
            {
                inventory.TakeGold(currentObjectScript.item, currentObject);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    inventory.AddItem(currentObjectScript.item, currentObject);
                }
            }
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeForTakeItem);
    }

    void UpdateTarget ()
    {   
        GameObject[] items = GameObject.FindGameObjectsWithTag("InterObject");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestItem = null;
        foreach (GameObject item in items)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, item.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestItem = item;
            }
        }

        if (nearestItem != null && shortestDistance <= rangeForTakeItem)
        {   
            currentObject = nearestItem;
            currentObjectScript = nearestItem.GetComponent<InteractionObject>();
        } else
        {
            nearestItem = null;
        }
    }
}
