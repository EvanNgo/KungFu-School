using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public float health = 100;
    public string enemyName;
    public Text txtEnemyName;
    public int enemyExp;
    public Image healthBar;
    public GameObject[] dropingItem;
    public GameObject Gold;
    public int goldDropping;
    public bool attack;
    //private GameObject target;
    private float currentHealth;
    //private QuestManager questManager;
    private PlayerLevel playerLevel;
    // Use this for initialization
    void Start() {
        //questManager = QuestManager.instance;
        playerLevel = PlayerLevel.instance;
        currentHealth = health;
        //target = GameObject.FindGameObjectWithTag("Player");
        txtEnemyName.text = enemyName;
    }
    public void Update(){
        if (attack) {
            AttackPlayer();
        }
    }

    public void TakeDameged(int dameged) {
        currentHealth -= dameged;
        FloatingTextController.CreateFloatingText("-" + dameged, transform);
        healthBar.fillAmount = currentHealth / health;
        if (currentHealth <= 0)
        {
            playerLevel.addExp(enemyExp);
            CapsuleCollider2D col = gameObject.GetComponent<CapsuleCollider2D>();
            col.enabled = false;
            if (goldDropping > 0)
            {
                int index = Random.Range(0, dropingItem.Length + 1);
                if (index == dropingItem.Length)
                {
                    InteractionObject intaer = Gold.GetComponent<InteractionObject>();
                    intaer.gold = goldDropping;
                    Instantiate(Gold, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(dropingItem[index], transform.position, Quaternion.identity);
                }
            }
            else
            {
                if (dropingItem.Length > 0)
                {
                    int index = Random.Range(0, dropingItem.Length);
                    Instantiate(dropingItem[index], transform.position, Quaternion.identity);
                }

            }
            //target.gameObject.GetComponent<PlayerLevel>().SendMessage("addExp", enemyExp);
            string[] spawnerName = gameObject.name.Split('_');
            GameObject.Find(spawnerName[0]).GetComponent<Spawner>().Death = true;
            Destroy(gameObject, 0.5f);
        }
    }

    public void AttackPlayer() {
    }
}
