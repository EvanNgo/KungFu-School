using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    //Enemy Setup
    public float health = 100;
    public Image healthBar;
    public string enemyName;
    private float currentHealth;
    public bool attack;
    public Text txtEnemyName;
    public int Armor = 0;
    //Enemy Dropping Item
    public GameObject[] dropingItem;
    public GameObject Gold;
    public int goldDropping;
    public int enemyExp;

    public int ItemMaxLines = 3;
    //private GameObject target;

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
        dameged -= Armor;
        if (dameged < 1)
        {
            dameged = 1;
        }
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
                    int gold = (int)Random.Range((int)goldDropping * 0.9f, (int)goldDropping * 1.1f);
                    intaer.priceSell = gold;
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
