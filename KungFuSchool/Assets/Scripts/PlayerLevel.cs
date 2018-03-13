using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerLevel : MonoBehaviour {
    int ExpOfLevel;
    int CurrentLevel;
    private float currentExp;
    public Image expBar;
    public Text txtExp;
    public Text txtLevel;
    // Use this for initialization
    void Start()
    {
        if (SQLiteCore.rootPlayer.Level == 0)
        {
            CurrentLevel = 1;
            // Save to database;
        }
        else {
            CurrentLevel = SQLiteCore.rootPlayer.Level;
        }
        txtLevel.text = CurrentLevel + "";
        ExpOfLevel = SQLiteCore.levelManager[CurrentLevel - 1].Exp;
        currentExp = SQLiteCore.rootPlayer.Exp;
        expBar.fillAmount = currentExp / ExpOfLevel;
        txtExp.text = currentExp * 100 / ExpOfLevel + "%";
    }

    public void addExp(int exp)
    {   
        currentExp += exp;
        if (currentExp >= ExpOfLevel) {
            CurrentLevel += 1;
            currentExp = 0;
            ExpOfLevel = SQLiteCore.levelManager[CurrentLevel - 1].Exp;
            txtLevel.text = CurrentLevel + "";
        }
        expBar.fillAmount = currentExp / ExpOfLevel;
        float textExp = (float)Math.Round((double)currentExp * 100 / ExpOfLevel, 2);
        txtExp.text = textExp + "%";
        txtLevel.text = CurrentLevel + "";
    }
}
