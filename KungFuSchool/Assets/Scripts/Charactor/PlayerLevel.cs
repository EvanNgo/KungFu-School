using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerLevel : MonoBehaviour {
    #region Singleton

    public static PlayerLevel instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerLevel>();
            }
            return _instance;
        }
    }
    static PlayerLevel _instance;

    void Awake()
    {
        _instance = this;
    }

    #endregion
    int ExpOfLevel;
    int CurrentLevel;
    private int currentExp;
    public Image expBar;
    public Text txtExp;
    public Text txtLevel;
    // Use this for initialization
    void Start()
    {
        CurrentLevel = PlayerManager.instance.player.Level;
        ExpOfLevel = SQLiteCore.levelManager[CurrentLevel - 1].Exp;
        currentExp = PlayerManager.instance.player.Exp;
        expBar.fillAmount = (float)currentExp / ExpOfLevel;
        float textExp = (float)Math.Round((double)currentExp * 100 / ExpOfLevel, 2);
        txtExp.text = textExp + "%";
        txtLevel.text = CurrentLevel + "";
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
        PlayerManager.instance.player.Level = CurrentLevel;
        PlayerManager.instance.player.Exp = currentExp;
        SQLiteCore.UpdatePlayer(PlayerManager.instance.player);
        expBar.fillAmount = (float)currentExp / ExpOfLevel;
        float textExp = (float)Math.Round((double)currentExp * 100 / ExpOfLevel, 2);
        txtExp.text = textExp + "%";
        txtLevel.text = CurrentLevel + "";
    }
}
