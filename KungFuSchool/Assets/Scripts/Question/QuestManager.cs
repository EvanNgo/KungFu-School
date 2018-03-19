using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {
    public QuestObject[] quests;
    public bool[] questCompleted;
    public DialogueManager dialogManager;
    public string itemCollected;
    public string enemyKilled;
	// Use this for initialization
	void Start () {
        questCompleted = new bool[quests.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestDialog(string[] txtShow){
        dialogManager.ShowBox(txtShow);
    }

    public void startEquest(int numberOfQuest){
        quests[numberOfQuest].StartQuestion();
    }
    public void endEquest(int numberOfQuest){
        quests[numberOfQuest].EndQuestion();
    }
}
