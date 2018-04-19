using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour {
    #region Singleton
    public static StatManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<StatManager>();
            }
            return _instance;
        }
    }
    static StatManager _instance;

    void Awake ()
    {   
        _instance = this;
    }

    #endregion
    private PlayerManager playerManager;
    private EquipmentManager equipmentManager;
    public int AttackDamage = 0;
    public int DamagePer = 0;
    public int SpellDame = 0;
    public int Armor = 0;
    public int HP = 0;
    public int MP = 0;
    public int Crit = 0;
    public int CritDame = 0;
    public int AttackSpeed = 0;
    public int SpellSpeed = 0;
    public int MoveSpeed;
    public int HitHP = 0;
    public int RegentHP = 0;
    public int Freeze = 0;
    public int Stun = 0;
    public int Poison = 0;
    public int Execllent = 0;

    public int Lucky = 0;
    public int MoreGold = 0;
    public int Redamage = 0;

    private void Start(){
        equipmentManager = EquipmentManager.instance;
        playerManager = PlayerManager.instance;
    }

    public int getHealth(){
        return 75 + playerManager.player.Sta * 5 + HP;
    }

    public int getMana(){
        return 25 + playerManager.player.Sta * 3 + MP;
    }

    public void EquipItemUpdate(TagManager tag,int point){
        switch (tag)
        {
            case TagManager.AttackDamage: 
                AttackDamage += point;
                break;
            case TagManager.SpellDame: 
                SpellDame += point;
                break;
            case TagManager.Armor: 
                Armor += point;
                break;
            case TagManager.HP: 
                HP += point;
                break;
            case TagManager.MP:  
                MP += point;
                break;
            case TagManager.Crit: 
                Crit += point;
                break;
            case TagManager.CritDame:  
                CritDame += point;
                break;
            case TagManager.DamagePer:  
                DamagePer += point;
                break;
            case TagManager.AttackSpeed:  
                AttackSpeed += point;
                break;
            case TagManager.SpellSpeed:  
                SpellSpeed += point;
                break;
            case TagManager.MoveSpeed:  
                MoveSpeed += point;
                break;
            case TagManager.HitHP:  
                HitHP += point;
                break;
            case TagManager.Freeze:  
                Freeze += point;
                break;
            case TagManager.Stun:  
                Stun += point;
                break;
            case TagManager.Poison:  
                Poison += point;
                break;
            case TagManager.Execllent:  
                Execllent += point;
                break;
            case TagManager.Lucky:  
                Lucky += point;
                break;
            case TagManager.MoreGold:  
                MoreGold += point;
                break;
            case TagManager.Redamage:  
                Redamage += point;
                break;
        }
    }

    public enum TagManager{AttackDamage,SpellDame,Armor,HP,MP,Crit,CritDame,DamagePer,AttackSpeed,SpellSpeed,MoveSpeed,HitHP,Freeze,Stun,Poison,Execllent,Lucky,MoreGold,Redamage}
}
