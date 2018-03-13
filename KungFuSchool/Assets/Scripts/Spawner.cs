using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public bool Death;
    public float Timer;
    public float Cooldown;
    public GameObject Enemy;
    public string EnemyName;
    string currentEnemyname;
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
            //If my enemy is death, a timer will start.
            Timer += Time.deltaTime;

        }
        //If the timer is bigger than cooldown.
        if(Timer >= Cooldown) {
            //It will create a new Enemy of the same class, at this position.
            Enemy.transform.position = transform.position;
            Enemy.name = currentEnemyname;
            Instantiate(Enemy,transform.position, Quaternion.identity);
            //My enemy won't be dead anymore.
            Death = false;
            //Timer will restart.
            Timer = 0;
        }
	}
}
