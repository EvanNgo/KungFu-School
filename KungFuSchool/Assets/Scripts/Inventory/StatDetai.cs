using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatDetai : MonoBehaviour {
    public Text AttackDamage;
    public Text AbilityPower;
    public Text Armor;
    public Text Crit;
    public Text DameCrit;
    public Text AttackSpeed;
    public Text SpellSpeed;
    public Text RegentTime;
    public Text MoveSpeed;
    public Text HitHP;
    public Text HPRegent;
    public Text Freeze;
    public Text Stun;
    public Text Poison;
    public Text Execllent;

    private StatManager statManager;
	// Use this for initialization
	void Start () {
        statManager = StatManager.instance;
        UpdateStat();
	}

    public void UpdateStat(){
        AttackDamage.text = statManager.getDamage() + "";
        AbilityPower.text = statManager.getSpellDamage() + "";
        Armor.text = statManager.getArmor() + "";
        Crit.text = statManager.getCrit() + "";
        DameCrit.text = statManager.getDameCrit() + "";
        AttackSpeed.text = statManager.getAttackSpeed() + "";
        SpellSpeed.text = statManager.getSpellSpeed() + "";
        RegentTime.text = statManager.getRegentTime() + "";
        MoveSpeed.text = statManager.getMoveSpeed() + "";
        HitHP.text = statManager.getHitHP() + "";
        HPRegent.text = statManager.getHPRegent() + "";
        Freeze.text = statManager.getFreeze() + "";
        Stun.text = statManager.getStun() + "";
        Poison.text = statManager.getPoison() + "";
        Execllent.text = statManager.getExecllent() + "";
    }
}
