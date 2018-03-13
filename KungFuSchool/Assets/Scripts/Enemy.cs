using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float health = 100;
    float currentHealth;
    // Use this for initialization
    void Start () {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update () {

    }

    public void TakeDameged(int dameged){
        currentHealth -= dameged;
        if (currentHealth <= 0)
        {
            //BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
            //col.enabled = false;
            //anim.SetTrigger("Dead");
            //Do everything you want with this part, but before destroying the enemy, add this:
            string[] spawnerName = gameObject.name.Split('_');
            GameObject.Find(spawnerName[0]).GetComponent<Spawner>().Death = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
