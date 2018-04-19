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
    public int RegentTime = 0;
    public int Freeze = 0;
    public int Stun = 0;
    public int Poison = 0;
    public int Execllent = 0;
    public int Lucky = 0;
    public int MoreGold = 0;
    public int Redamage = 0;
    public int AddStr = 0;
    public int AddAgi = 0;
    public int AddSta = 0;
    public int AddIntel = 0;
    private void Start(){
        equipmentManager = EquipmentManager.instance;
        playerManager = PlayerManager.instance;
    }

    public int getHealth(){
        return 75 + (PlayerManager.instance.getSta() + AddSta) * 5 + HP;
    }

    public int getMana(){
        return 25 + (PlayerManager.instance.getSta() + AddIntel) * 3 + MP;
    }

    public int getDamage(){
        int damage = 0;
        damage = (PlayerManager.instance.getStr() + AddStr) * 2;
        damage += (damage * DamagePer) / 100;
        damage += AttackDamage;
        return damage;
    }

    public int getArmor(){
        int armor = 0;
        armor = (PlayerManager.instance.getAgi() + AddAgi) * 2 + Armor;
        return armor;
    }

    public int getSpellDamage(){
        int spellAttack = 0;
        spellAttack = (PlayerManager.instance.getIntel() + AddIntel) * 3;
        spellAttack += SpellDame;
        return spellAttack;
    }

    public int getCrit(){
        return Crit;
    }
    public int getDameCrit(){
        int spellAttack = 0;
        return spellAttack;
    }

    public int getSpellSpeed(){
        return CritDame;
    }

    public int getRegentTime(){
        return RegentTime;
    }
    public int getMoveSpeed(){
        return MoveSpeed;
    }
    public int getHitHP(){
        return HitHP;
    }

    public int getHPRegent(){
        return RegentHP;
    }

    public int getStun(){
        return Stun;
    }
    public int getPoison(){
        return Poison;
    }
    public int getExecllent(){
        return Execllent;
    }

    public int getFreeze(){
        return Freeze;
    }

    public int getAttackSpeed(){
        return AttackSpeed;
    }

    public void EquipItemUpdate(TagManager tag,int point,bool equip){
        switch (tag)
        {
            case TagManager.AttackDamage: 
                AttackDamage = (equip) ? AttackDamage + point : AttackDamage - point;
                break;
            case TagManager.SpellDame: 
                SpellDame = (equip) ? SpellDame + point : SpellDame - point;
                break;
            case TagManager.Armor: 
                Armor = (equip) ? Armor + point : Armor - point;
                break;
            case TagManager.HP: 
                HP = (equip) ? HP + point : HP - point;
                break;
            case TagManager.MP:  
                MP = (equip) ? MP + point : MP - point;
                break;
            case TagManager.Crit: 
                Crit = (equip) ? Crit + point : Crit - point;
                break;
            case TagManager.CritDame:  
                CritDame = (equip) ? CritDame + point : CritDame - point;
                break;
            case TagManager.DamagePer:  
                DamagePer = (equip) ? DamagePer + point : DamagePer - point;
                break;
            case TagManager.AttackSpeed:  
                AttackSpeed = (equip) ? AttackSpeed + point : AttackSpeed - point;
                break;
            case TagManager.SpellSpeed:  
                SpellSpeed = (equip) ? SpellSpeed + point : SpellSpeed - point;
                break;
            case TagManager.MoveSpeed:  
                MoveSpeed = (equip) ? MoveSpeed + point : MoveSpeed - point;
                break;
            case TagManager.HitHP:  
                HitHP = (equip) ? HitHP + point : HitHP - point;
                break;
            case TagManager.Freeze:  
                Freeze = (equip) ? Freeze + point : Freeze - point;
                break;
            case TagManager.Stun:  
                Stun = (equip) ? Stun + point : Stun - point;
                break;
            case TagManager.Poison:  
                Poison = (equip) ? Poison + point : Poison - point;
                break;
            case TagManager.Execllent:  
                Execllent = (equip) ? Execllent + point : Execllent - point;
                break;
            case TagManager.Lucky:  
                Lucky = (equip) ? Lucky + point : Lucky - point;
                break;
            case TagManager.MoreGold:  
                MoreGold = (equip) ? MoreGold + point : MoreGold - point;
                break;
            case TagManager.Redamage:  
                Redamage = (equip) ? Redamage + point : Redamage - point;
                break;
            case TagManager.AddSta:  
                AddSta = (equip) ? AddSta + point : AddSta - point;
                break;
            case TagManager.AddStr:  
                AddStr = (equip) ? AddStr + point : AddStr - point;
                break;
            case TagManager.AddAgi:  
                AddAgi = (equip) ? AddAgi + point : AddAgi - point;
                break;
            case TagManager.AddIntel:  
                AddIntel = (equip) ? AddIntel + point : AddIntel - point;
                break;
        }
    }

    public enum TagManager{AttackDamage,SpellDame,Armor,HP,MP,Crit,CritDame,DamagePer,AttackSpeed,SpellSpeed,MoveSpeed,HitHP,RegentTime,RegentHP,Freeze,Stun,Poison,Execllent,Lucky,MoreGold,Redamage,AddStr,AddAgi,AddSta,AddIntel}
}
