using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public Transform[] patrolpoints;
	public Transform target;
	public float moveSpeed;
	public float chaseSpeed;
	public float attackRange;
	public float dame;
	public float health;
	int currentPoint;
	bool facingRight;
	float leftPoint;
	float rightPoint;
	float widthTarget;
	float zombieWidth;
	bool Attack;
	public float attackDelay;
    float timeToDead = 1.5f;
    float currentHealth;
	float lastAttackTime = 0;
	Animator anim;
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
        currentHealth = health;
	}


	// Update is called once per frame
	void FixedUpdate () {
        if (currentHealth > 0)
        {
            anim.SetFloat("Speed", moveSpeed);
            if (!(target.position.x + widthTarget <= rightPoint && target.position.x + widthTarget >= leftPoint && Mathf.Abs((target.position.y - transform.position.y)) < target.GetComponent<SpriteRenderer>().bounds.size.y / 1.5))
            {
                if (patrolpoints[currentPoint].position.x == transform.position.x)
                {
                    currentPoint++;
                }

                if (currentPoint >= patrolpoints.Length)
                {
                    currentPoint = 0;
                }

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(patrolpoints[currentPoint].position.x, transform.position.y), moveSpeed);
                if ((patrolpoints[currentPoint].position.x > transform.position.x && !facingRight)
                    || (patrolpoints[currentPoint].position.x <= transform.position.x && facingRight))
                {
                    faceControll();
                }
            }
            else
            {   
                if ((target.position.x < transform.position.x && facingRight) || (target.position.x > transform.position.x && !facingRight))
                {   
                    faceControll();
                }
                float distanceToPlayer = Vector3.Distance(transform.position, target.position);
                if (distanceToPlayer <= attackRange)
                {   
                    anim.SetFloat("Speed", 0);
                    if (Time.time > lastAttackTime + attackDelay)
                    {   
                        target.SendMessage("TakeDameged", dame);
                        anim.SetTrigger("Attack");
                        lastAttackTime = Time.time;
                    }
                }
                else
                {
                    if (facingRight)
                    {
                        transform.Translate(new Vector3(chaseSpeed * Time.deltaTime, 0, 0));
                    }
                    else
                    {
                        transform.Translate(new Vector3(-1 * chaseSpeed * Time.deltaTime, 0, 0));
                    }
                }
            }
        }
        else
        {
            if (timeToDead > 0)
            {
                timeToDead -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
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

	private void handleAttack(){
		if (Attack) {
			anim.SetTrigger ("Attack");
			Attack = false;
		}
	}
    public void TakeDameged(int dameged){
        health -= dameged;
        Debug.Log("Damged "+dameged);
        if (health <= 0)
        {   
            
            Debug.Log("Enemy Killed");
        }
    }
}