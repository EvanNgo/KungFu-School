using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetails : MonoBehaviour {
    public float health = 500;
    public float mana = 500;
    private float currentHealth;
    private float currentMana;
    public Image healthBar;
    public Image manaBar;
    public Text txtHealth;
    public Text txtMana;

    // Use this for initialization
    void Start () {
        currentHealth = health;
        currentMana = mana;
        healthBar.fillAmount = currentHealth / health;
        manaBar.fillAmount = currentMana / mana;
        txtHealth.text = currentHealth + "/" + health;
        txtMana.text = currentMana + "/" + mana;
    }

    public void TakeDameged(int dameged)
    {       
        currentHealth -= dameged;
        healthBar.fillAmount = currentHealth / health;
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
        }
    }

    public void addExp() {
    }
}
