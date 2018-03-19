using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {
    private QuestManager questManager;
    public int questNumber;
    public bool startQuest;
    public bool endQuest;
	// Use this for initialization
	void Start () {
        questManager = GameObject.FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player")
        {
            if (!questManager.questCompleted[questNumber])
            {
                if (startQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.startEquest(questNumber);
                    gameObject.SetActive(false);
                }

                if (endQuest && questManager.quests[questNumber].gameObject.activeSelf)
                {
                    questManager.endEquest(questNumber);
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
