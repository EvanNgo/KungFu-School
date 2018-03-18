using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerDetails : MonoBehaviour {
    public float health = 500;
    public float mana = 500;
    private float currentHealth;
    private float currentMana;
    public Image healthBar;
    public Image manaBar;
    public Text txtHealth;
    public Text txtMana;
    public float countdown;
    public float teleportCD;
    public Image healthCD;
    public Image manaCD;
    public Image teleCD;
    public Text TxtteleCD;
    public Text TxthealthCD;
    public Text TxtmanaCD;
    private float healthTimeCount;
    private float manaTimeCount;
    private float teleTimeCount;
    private bool healthCounting;
    private bool manaCounting;
    private bool teleCounting;

    // Use this for initialization
    void Start () {
        currentHealth = health;
        currentMana = mana;
        healthBar.fillAmount = currentHealth / health;
        manaBar.fillAmount = currentMana / mana;
        txtHealth.text = currentHealth + "/" + health;
        txtMana.text = currentMana + "/" + mana;
        TxthealthCD.enabled = false;
        healthCD.enabled = false;
        TxtmanaCD.enabled = false;
        manaCD.enabled = false;
        TxtteleCD.enabled = false;
        teleCD.enabled = false;
    }

    private void Update()
    {
        if (healthCounting)
        {
            if (healthTimeCount <= 0)
            {
                healthCounting = false;
                healthCD.enabled = false;
                TxthealthCD.enabled = false;
            }
            else
            {
                healthTimeCount -= Time.deltaTime;
                healthCD.fillAmount = healthTimeCount / countdown;
                if (healthTimeCount <= 1)
                {
                    TxthealthCD.text = (float)Math.Round((double)healthTimeCount, 1) + "";
                }
                else
                {
                    TxthealthCD.text = (int)healthTimeCount + "";
                }
            }
        }
        if (Input.GetButtonDown("Health") && !healthCounting)
        {
            healthTimeCount = countdown;
            healthCounting = true;
            TxthealthCD.text = 10 + "";
            TxthealthCD.enabled = true;
            healthCD.fillAmount = 1;
            healthCD.enabled = true;
        }

        if (manaCounting)
        {
            if (manaTimeCount <= 0)
            {
                manaCounting = false;
                manaCD.enabled = false;
                TxtmanaCD.enabled = false;
            }
            else
            {
                manaTimeCount -= Time.deltaTime;
                manaCD.fillAmount = manaTimeCount / countdown;
                if (manaTimeCount <= 1)
                {
                    TxtmanaCD.text = (float)Math.Round((double)manaTimeCount, 1) + "";
                }
                else
                {
                    TxtmanaCD.text = (int)manaTimeCount + "";
                }
            }
        }
        if (Input.GetButtonDown("Mana") && !manaCounting)
        {
            manaTimeCount = countdown;
            manaCounting = true;
            TxtmanaCD.text = 10 + "";
            TxtmanaCD.enabled = true;
            manaCD.fillAmount = 1;
            manaCD.enabled = true;
        }

        if (teleCounting)
        {
            if (teleTimeCount <= 0)
            {
                teleCounting = false;
                teleCD.enabled = false;
                TxtteleCD.enabled = false;
            }
            else
            {
                teleTimeCount -= Time.deltaTime;
                teleCD.fillAmount = teleTimeCount / teleportCD;
                if (teleTimeCount <= 1)
                {
                    TxtteleCD.text = (float)Math.Round((double)teleTimeCount, 1) + "";
                }
                else
                {
                    TxtteleCD.text = (int)teleTimeCount + "";
                }

            }
        }
        if (Input.GetButtonDown("Teleport") && !teleCounting)
        {
            teleTimeCount = teleportCD;
            teleCounting = true;
            TxtteleCD.text = teleportCD + "";
            TxtteleCD.enabled = true;
            teleCD.fillAmount = 1;
            teleCD.enabled = true;
        }
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

    public void UsingHPPotion()
    {
        if (healthCounting)
        {
            return;
        }
        healthTimeCount = countdown;
        healthCounting = true;
        TxthealthCD.text = 10 + "";
        TxthealthCD.enabled = true;
        healthCD.fillAmount = 1;
        healthCD.enabled = true;
    }
    public void UsingMPPotion()
    {
        if (manaCounting)
        {
            return;
        }
        manaTimeCount = countdown;
        manaCounting = true;
        TxtmanaCD.text = 10 + "";
        TxtmanaCD.enabled = true;
        manaCD.fillAmount = 1;
        manaCD.enabled = true;
    }
    public void Teleport()
    {
        if (teleCounting)
        {
            return;
        }
        teleTimeCount = teleportCD;
        teleCounting = true;
        TxtteleCD.text = teleportCD + "";
        TxtteleCD.enabled = true;
        teleCD.fillAmount = 1;
        teleCD.enabled = true;
    }
}
