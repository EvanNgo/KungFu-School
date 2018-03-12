using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    //spawn aliens. This would go on a script called GameManager on empty GameObject or something
    public GameObject enemy;
    public float CountDown = 3f;
    public bool Dead = false;
    float Timer = 0;
    void Start(){
        SpawnEnemy(); //example of calling a function from within the script
    }

    void Update(){
        if (Dead)
        {
            Timer += Time.deltaTime;
            if (Timer > CountDown)
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy(){
        //spawns alien gameObject at spawnPoint position, and give it a rotation to complete the function
        Instantiate(enemy,transform.position, Quaternion.identity);
        Dead = false;
        Timer = 0;
    }
}
