using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public float health = 100;
    private GameObject target;
    public QuestManager questManager;
    float currentHealth;
    public float enemyExp = 600;
    public Image healthBar;
    public GameObject[] dropingItem;
    public GameObject Gold;
    public int goldDropping;
    // Use this for initialization
    void Start () {
        questManager = FindObjectOfType<QuestManager>();
        currentHealth = health;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDameged(int dameged){
        currentHealth -= dameged;
        healthBar.fillAmount = currentHealth / health;
        if (currentHealth <= 0)
        {
            BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
            col.enabled = false;
            int index = Random.Range(0, dropingItem.Length+1);
            if (index == dropingItem.Length)
            {
                InteractionObject inter = Gold.GetComponent<InteractionObject>();
                inter.gold = goldDropping;
                Instantiate(Gold, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(dropingItem[index], transform.position, Quaternion.identity);
            }
            target.gameObject.GetComponent<PlayerLevel>().SendMessage("addExp", enemyExp);
            string[] spawnerName = gameObject.name.Split('_');
            GameObject.Find(spawnerName[0]).GetComponent<Spawner>().Death = true;
            Destroy(gameObject, 0.5f);
        }
    }
}
