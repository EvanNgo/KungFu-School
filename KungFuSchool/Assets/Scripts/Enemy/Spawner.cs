using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public float Cooldown;
    public GameObject Enemy;
    public string EnemyName;
    string currentEnemyname;
    public bool Death;
    float Timer;
	// Use this for initialization
	void Start () {
        Death = false;
        Enemy.name = gameObject.name + "_" + EnemyName;
        currentEnemyname = gameObject.name + "_" + EnemyName;
        Instantiate(Enemy,transform.position, Quaternion.identity); 
	}
	
	// Update is called once per frame
	void Update () {
        if(Death == true) {
            Timer += Time.deltaTime;
        }
        if(Timer >= Cooldown) {
            Enemy.transform.position = transform.position;
            Enemy.name = currentEnemyname;
            Instantiate(Enemy,transform.position, Quaternion.identity);
            Death = false;
            Timer = 0;
        }
	}
}
