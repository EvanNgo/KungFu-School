using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlelr : MonoBehaviour {
    public float maxSpeed;
    public float jumpHeight;
    public GameObject inventoryUI;
    public GameObject dialogManager;
    Rigidbody2D myBody;
    Animator myAni;
    bool facingRight = true;
    bool grounded;
    bool borderLeft = false;
    bool borderRight = false;
    bool isBagging;
    bool isShopping;
    void Start() {
        myBody = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
    } 
    // Update is called once per frame
    void FixedUpdate() {
        if (inventoryUI.activeSelf)
        {
            return;
        }
        float move = Input.GetAxis("Horizontal");
        if (!(move <= 0 && borderLeft) || !(move >= 0 && borderRight))
        {
            myBody.velocity = new Vector2(move * maxSpeed, myBody.velocity.y);
        }
        myAni.SetFloat("Speed", Mathf.Abs(move));
        if (move > 0 && !facingRight) {
            faceControll();
        }
        else if (move < 0 && facingRight)
        {
            faceControll();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (grounded) {
                myAni.SetFloat("Speed", 0);
                myAni.SetBool("Jumping", true);
                grounded = false;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpHeight);
            }
        }
    }

    void faceControll()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {	
		if (collision.gameObject.tag == "Ground" && collision.relativeVelocity.y >= 0)
		{
			myAni.SetBool("Jumping",false);
			grounded = true;
		}
        if (collision.gameObject.tag == "BorderLeft")
        {
            borderLeft = true;
        }
        if (collision.gameObject.tag == "BorderRight")
        {
            borderRight = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {   
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
