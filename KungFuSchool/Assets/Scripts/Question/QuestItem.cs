using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {
    public int questNumber;
    private QuestManager questManager;
    public string itemName;
	// Use this for initialization
	void Start () {
        questManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.name == "Player")
        {
            if (!questManager.questCompleted[questNumber] && questManager.quests[questNumber].gameObject.activeSelf)
            {
                questManager.itemCollected = itemName;
                gameObject.SetActive(false);
            }
        }
    }
}
