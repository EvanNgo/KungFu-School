 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    public BoxCollider2D attackTrigger;
    SantaObject santaObject = null;
    BoxItem boxItem = null;
    GameObject currentGameObject;
    GameObject currentBoxItem;
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;
    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
        attackTrigger.enabled = false;
    }

    private void FixedUpdate()
    {
        
    }

    void Update(){
        if (currentGameObject!=null && santaObject!=null && santaObject.talks)
        {
            if (Input.GetKeyDown(KeyCode.K)) {
                santaObject.Talk();
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.K) && !attacking)
            {
                anim.SetTrigger("Attack");
                attackTrigger.enabled = true;
                attacking = true;
                attackTimer = attackCd;
            }
            if (attacking)
            {
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    attacking = false;
                    attackTrigger.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC")) {
            currentGameObject = collision.gameObject;
            santaObject = currentGameObject.GetComponent<SantaObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            if (collision.gameObject == currentGameObject)
            {
                currentGameObject = null;
                santaObject = null;
            }
        }
    }
}
