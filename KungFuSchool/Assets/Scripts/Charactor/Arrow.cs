using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public float dmg = 100;
    public Transform DestroyPointer;

    private void Update()
    {
       
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            collider.SendMessage("TakeDameged", dmg);
            Destroy(gameObject);
        }
    }
}
