using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
    #region Singleton

    public static DialogueManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<DialogueManager> ();
            }
            return _instance;
        }
    }
    static DialogueManager _instance;

    void Awake ()
    {
        _instance = this;
    }

    #endregion
    public GameObject dialogBox;
    public Text dialogText;
    private string[] dialogLines;
    private int currentLine;

    public void ShowBox(string[] dialogDe){
        Debug.Log("CallDialog");
        dialogBox.SetActive(!dialogBox.activeSelf);
        dialogLines = new string[dialogDe.Length];
        dialogLines = dialogDe;
        if (dialogLines.Length > 0)
        {
            dialogText.text = dialogLines[0];
        }
    }
    public void BoxClicked()
    {
        currentLine++;
        if (currentLine >= dialogLines.Length)
        {
            dialogBox.SetActive(!dialogBox.activeSelf);
            currentLine = 0;
        }
        else
        {
            dialogText.text = dialogLines[currentLine];
        }
    }
}
