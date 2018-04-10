using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlelr : MonoBehaviour {
    public float moveSpeed;
    private bool playerMoving;
    private Vector2 lastMove;
    public GameObject inventoryUI;
    public GameObject dialogManager;
    private float attackTime = 0.3f;
    private float attackCounter = 0;
    private bool attack = false; 
    Rigidbody2D myBody;
    Animator myAni;

    void Start() {
        myBody = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
    } 
    // Update is called once per frame
    void FixedUpdate() {
        playerMoving = false;
//        if (inventoryUI.activeSelf)
//        {
//            return;
//        }
        if (!attack)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.1f || Input.GetAxisRaw("Horizontal") < -0.1f)
            {
                transform.Translate( new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f,0f));
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.1f || Input.GetAxisRaw("Vertical") < -0.1f)
            {
                transform.Translate( new Vector3(0, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime,0f));
                playerMoving = true;
                lastMove = new Vector2(0f,Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetKeyDown(KeyCode.K))
            {   
                attackCounter = attackTime;
                attack = true; 
                myBody.velocity = Vector2.zero;
                myAni.SetBool("Attack", true);
            }
        }

        if (attackCounter >= 0)
        {
            attackCounter -= Time.deltaTime;
        }

        if (attackCounter <= 0)
        {
            myAni.SetBool("Attack", false);
            attack = false; 
        }

        myAni.SetBool("Moving", playerMoving);
        myAni.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        myAni.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        myAni.SetFloat("LastMoveX", lastMove.x);
        myAni.SetFloat("LastMoveY", lastMove.y);
    }
}
