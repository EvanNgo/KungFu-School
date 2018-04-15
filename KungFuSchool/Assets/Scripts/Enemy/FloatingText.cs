using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    public Animator animator;
    private Text damageText;

    void OnEnable()
    {
        Destroy(gameObject, 1.2f);
        damageText = animator.GetComponent<Text>();
    }

    public void SetText(string text)
    {
        Debug.Log("SetText");
        damageText.text = text;
    }
}
