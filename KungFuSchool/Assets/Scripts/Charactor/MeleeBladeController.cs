using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBladeController : MonoBehaviour {
    public float dmg = 0;
    StatManager statManager;
    void Start(){
        statManager = StatManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Enemy"))
        {
            collider.SendMessage("TakeDameged", statManager.getDamage());
        }
    }
}
