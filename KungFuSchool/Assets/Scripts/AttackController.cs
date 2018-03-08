using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour {
    public BoxCollider2D attackTrigger;
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.3f;

    private Animator anim;
    void Start(){
        anim = GetComponent<Animator>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.K) && !attacking) {
            anim.SetTrigger ("Attack");
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
