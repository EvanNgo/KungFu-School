using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControlelr : MonoBehaviour {
    [Header("Basic Setting")]
    public float moveSpeed;
    private bool playerMoving;
    private Vector2 lastMove;
    public GameObject inventoryUI;
    public GameObject dialogManager;
    public GameObject talkingMenu;
    private bool attack = false;
    Rigidbody2D myBody;
    Animator myAni;

    [Header("Enemy Finding")]
    public float rangeForAttackEnemy;
    public float rangeForFoundEnemy;
    private GameObject currentEnemy;

    [Header("NPC Finding")]
    public NPC currentNPC;
    public bool NPCTalkingEnable;
    void Start() {
        myBody = GetComponent<Rigidbody2D>();
        myAni = GetComponent<Animator>();
        lastMove = new Vector2(0, -1);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void FixedUpdate() {
        playerMoving = false;
        if (inventoryUI.activeSelf || dialogManager.activeSelf || talkingMenu.activeSelf)
        {
            myAni.SetBool("Sword_Attack", false);
            myAni.SetBool("Moving", playerMoving);
            attack = false;
            return;
        }
        if (currentEnemy == null)
        {
            myAni.SetBool("Sword_Attack", false);
            attack = false;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.1f || Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            attack = false;
            myAni.SetBool("Sword_Attack", attack);
        }

        if (Input.GetAxisRaw("Vertical") > 0.1f || Input.GetAxisRaw("Vertical") < -0.1f)
        {
            transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            attack = false;
            myAni.SetBool("Sword_Attack", attack);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentEnemy != null)
            {
                attack = true;
            }else if (currentNPC != null)
            {
                currentNPC.Talking();
            }
            
        }

        if (attack)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (distanceToEnemy <= rangeForAttackEnemy)
            {
                attack = true;
                myBody.velocity = Vector2.zero;
                myAni.SetBool("Sword_Attack", attack);
                playerMoving = false;
            }
            else
            {
                Vector3 dir = currentEnemy.transform.position - transform.position;
                transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
                playerMoving = true;
            }
        }

        myAni.SetBool("Moving", playerMoving);
        myAni.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        myAni.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        myAni.SetFloat("LastMoveX", lastMove.x);
        myAni.SetFloat("LastMoveY", lastMove.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeForAttackEnemy);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeForFoundEnemy);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= rangeForFoundEnemy)
        {
            currentEnemy = nearestEnemy;
        }
        else
        {
            currentEnemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            currentNPC = collision.gameObject.GetComponent<NPC>();
            currentNPC.txtname.color = Color.yellow;
            NPCTalkingEnable = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" && currentNPC == null)
        {
            currentNPC = collision.gameObject.GetComponent<NPC>();
            currentNPC.txtname.color = Color.yellow;
            NPCTalkingEnable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            currentNPC.txtname.color = Color.white;
            currentNPC = null;
            NPCTalkingEnable = false;
        }
    }
}
