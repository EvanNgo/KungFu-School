using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
    public GameObject dialogBox;
    public Text dialogText;
    public bool dialogActive;
    public string[] dialogLines;
    public int currentLine;
	// Use this for initialization
	void Start () {
        dialogBox.SetActive(false);
        dialogActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (dialogActive && Input.GetButtonDown("Select"))
        {
            currentLine++;
            if (currentLine >= dialogLines.Length)
            {
                dialogBox.SetActive(false);
                dialogActive = false;
                currentLine = 0;
            }
            else
            {
                dialogText.text = dialogLines[currentLine];
            }
        }
	}

    public void ShowBox(string[] dialogDe){
        dialogLines = new string[dialogDe.Length];
        dialogLines = dialogDe;
        dialogActive = true;
        dialogBox.SetActive(true);
        dialogLines = new string[dialogDe.Length];
        dialogLines = dialogDe;
        if (dialogLines.Length > 0)
        {
            dialogText.text = dialogLines[0];
        }
    }
}
