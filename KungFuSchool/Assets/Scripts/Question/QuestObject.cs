using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {
    public int questNumber;
    public QuestManager questManager;
    public string[] txtStartQuest;
    public string[] txtEndQuest;
    public bool isItemQuest;
    public string targetItem;
    public bool isEnemyQuest;
    public string targetEnemy;
    public int enemyKilledCount;
    public int enemyKilledTarget;
	// Use this for initialization
	void Start () {
        questManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isItemQuest){
            if (questManager.itemCollected == targetItem)
            {   
                questManager.itemCollected = null;
                EndQuestion();
            }
        }
        if (isEnemyQuest)
        {   
            if (questManager.enemyKilled == targetEnemy)
            {
                questManager.enemyKilled = null;
                enemyKilledCount++;
            }

            if (enemyKilledCount >= enemyKilledTarget)
            {
                EndQuestion();
            }
        }
	}

    public void StartQuestion(){
        questManager.ShowQuestDialog(txtStartQuest);
    }
    public void EndQuestion(){
        questManager.ShowQuestDialog(txtEndQuest);
        questManager.questCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
