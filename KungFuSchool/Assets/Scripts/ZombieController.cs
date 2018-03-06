using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {
	public Transform[] patrolpoints;
	public Transform target;
	public float moveSpeed;
	Transform targeting;
	int currentPoint;
	bool facingRight;
	float leftPoint;
	float rightPoint;
	float widthTarget;
	float zombieWidth;
	bool Attack;
	Animator anim;
	Rigidbody2D zomBody;
	// Use this for initialization
	void Start () {
		//StartCoroutine ("Patrol");
		facingRight = true;
		if (patrolpoints [0].position.x > patrolpoints [1].position.x) {
			leftPoint = patrolpoints [1].position.x;
			rightPoint = patrolpoints [0].position.x;
		} else {
			leftPoint = patrolpoints [0].position.x;
			rightPoint = patrolpoints [1].position.x;
		}
		widthTarget = target.GetComponent<SpriteRenderer>().bounds.size.x/1.6f;
		anim = GetComponent<Animator>();
		zomBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!(target.position.x + widthTarget <= rightPoint && target.position.x  + widthTarget >= leftPoint && Mathf.Abs((target.position.y - transform.position.y)) < target.GetComponent<SpriteRenderer>().bounds.size.y/1.5)) {
			if (transform.position.x == patrolpoints [currentPoint].position.x) {
				currentPoint++;
			}
			if (currentPoint >= patrolpoints.Length) {
				currentPoint = 0;
			}
			moveToTarget (patrolpoints [currentPoint]);
		} else {
			moveToCharacter ();
		}
		if ((targeting.position.x > transform.position.x && !facingRight) 
			|| (targeting.position.x <= transform.position.x && facingRight)) {
			faceControll ();
		}
	}

	void faceControll()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void moveToTarget(Transform transformTarget){
		anim.SetFloat ("Speed", moveSpeed);
		targeting = transformTarget;
		transform.position = Vector2.MoveTowards (transform.position, new Vector2 (transformTarget.position.x , transform.position.y), moveSpeed);
	}

	void handleAttack(){
		if (Attack) {
			anim.SetTrigger ("Attack");
			Attack = false;
		}
	}

	void moveToCharacter(){
		if (transform.position.x == target.position.x - widthTarget || transform.position.x == target.position.x + widthTarget) {
			handleAttack ();
		} else {
			if (target.position.x > transform.position.x) {
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (target.position.x - widthTarget , transform.position.y), moveSpeed);
			} else {
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (target.position.x + widthTarget , transform.position.y), moveSpeed);
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{	
		if (collision.gameObject.tag == "Player")
		{
			
		}
	}
}
