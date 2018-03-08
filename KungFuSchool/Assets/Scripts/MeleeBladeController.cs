using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBladeController : MonoBehaviour {
    float dmg = 20;

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Enemy"))
        {
            collider.SendMessage("TakeDameged", dmg);
        }
    }
}
