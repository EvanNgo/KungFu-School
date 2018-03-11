 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlelr : MonoBehaviour {
    public float maxSpeed;
    public float jumpHeight;
    float health = 100;
    Rigidbody2D myBody;
    Animator myAni;
    bool facingRight = true;
    bool grounded;
    bool borderLeft = false;
    bool borderRight = false;
    bool isShopping;
    void Start() {
        myBody = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
    }
    // Update is called once per frame
    void FixedUpdate() {
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

        if (Input.GetKeyUp(KeyCode.B))
        {
            if (!isShopping)
            {
                OpenInventory();
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

    private void OpenInventory() {
        Debug.Log("Open Inventory");
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
    public void TakeDameged(int dameged){
        health -= dameged;
        if (health <= 0)
        {
            Debug.Log("Dead");
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
