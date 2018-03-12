using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour {
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
            Debug.Log ("Dead");
            Enemy enemy = GameObject.FindObjectOfType<Enemy>();
            enemy.Dead = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
