 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    public BoxCollider2D attackTrigger;
    public SantaObject santaObject = null;
    public GameObject currentGameObject;
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
            if (Input.GetKeyDown(KeyCode.K) && !attacking)
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
            Debug.Log("Trigger Enter");
            currentGameObject = collision.gameObject;
            santaObject = currentGameObject.GetComponent<SantaObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            Debug.Log("Trigger Exit");
            currentGameObject = null;
            santaObject = null;
        }
    }
}
