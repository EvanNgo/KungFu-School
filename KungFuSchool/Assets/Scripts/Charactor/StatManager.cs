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
    public int Defense = 0;
    public int StunTime = 0;
    public int FreezeTime = 0;
    public int RegentPer = 0;
    public int ArmorPerLevel = 0;
    public int DamePerLevel = 0;
    StatDetai statDetail;
    private void Start(){
        equipmentManager = EquipmentManager.instance;
        playerManager = PlayerManager.instance;
        statDetail = StatDetai.instance;
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

    public void UpdateEquipItem(Item item,bool equip){
        UpdateEquipItemOption(item.defaultOption.tag, item.defaultPoint, equip);
        if (item.options.Length == 0 || item.options == null)
        {
            return;
        }
        for (int i = 0; i < item.options.Length; i++)
        {
            UpdateEquipItemOption(item.options[i].tag, item.points[i], equip);
        }
    }

    public void UpdateEquipItemOption(TagManager tag,int point,bool equip){
        switch (tag)
        {
            case TagManager.AttackDamage: //1
                AttackDamage = (equip) ? AttackDamage + point : AttackDamage - point;
                break;
            case TagManager.SpellDame: //2
                SpellDame = (equip) ? SpellDame + point : SpellDame - point;
                break;
            case TagManager.Armor: //3
                Armor = (equip) ? Armor + point : Armor - point;
                break;
            case TagManager.HP: //4
                HP = (equip) ? HP + point : HP - point;
                break;
            case TagManager.MP:  //5
                MP = (equip) ? MP + point : MP - point;
                break;
            case TagManager.Crit: //6
                Crit = (equip) ? Crit + point : Crit - point;
                break;
            case TagManager.CritDame://7  
                CritDame = (equip) ? CritDame + point : CritDame - point;
                break;
            case TagManager.DamagePer:  //8
                DamagePer = (equip) ? DamagePer + point : DamagePer - point;
                break;
            case TagManager.AttackSpeed:  //9
                AttackSpeed = (equip) ? AttackSpeed + point : AttackSpeed - point;
                break;
            case TagManager.SpellSpeed:  //10
                SpellSpeed = (equip) ? SpellSpeed + point : SpellSpeed - point;
                break;
            case TagManager.MoveSpeed:  //11
                MoveSpeed = (equip) ? MoveSpeed + point : MoveSpeed - point;
                break;
            case TagManager.HitHP:  //12
                HitHP = (equip) ? HitHP + point : HitHP - point;
                break;
            case TagManager.Freeze:  //13
                Freeze = (equip) ? Freeze + point : Freeze - point;
                break;
            case TagManager.Stun:  //14
                Stun = (equip) ? Stun + point : Stun - point;
                break;
            case TagManager.Poison:  //15
                Poison = (equip) ? Poison + point : Poison - point;
                break;
            case TagManager.Execllent:  //16
                Execllent = (equip) ? Execllent + point : Execllent - point;
                break;
            case TagManager.Lucky:  //17
                Lucky = (equip) ? Lucky + point : Lucky - point;
                break;
            case TagManager.MoreGold:  //18
                MoreGold = (equip) ? MoreGold + point : MoreGold - point;
                break;
            case TagManager.Redamage:  //19
                Redamage = (equip) ? Redamage + point : Redamage - point;
                break;
            case TagManager.AddSta:  //20
                AddSta = (equip) ? AddSta + point : AddSta - point;
                break;
            case TagManager.AddStr:  //21
                AddStr = (equip) ? AddStr + point : AddStr - point;
                break;
            case TagManager.AddAgi:  //22
                AddAgi = (equip) ? AddAgi + point : AddAgi - point;
                break;
            case TagManager.AddIntel:  //23
                AddIntel = (equip) ? AddIntel + point : AddIntel - point;
                break;
            case TagManager.RegentTime:  //24 Done
                RegentTime = (equip) ? RegentTime + point : RegentTime - point;
                break;
            case TagManager.Defense:  //25
                Defense = (equip) ? Defense + point : Defense - point;
                break;
            case TagManager.FreezeTime:  //26
                FreezeTime = (equip) ? FreezeTime + point : FreezeTime - point;
                break;
            case TagManager.StunTime:  //27
                StunTime = (equip) ? StunTime + point : StunTime - point;
                break;
            case TagManager.RegentPer: //28
                RegentPer = (equip) ? RegentPer + point : RegentPer - point;
                break;
            case TagManager.ArmorPerLevel:  //29
                ArmorPerLevel = (equip) ? ArmorPerLevel + point : ArmorPerLevel - point;
                break;
            case TagManager.DamePerLevel: //30
                DamePerLevel = (equip) ? DamePerLevel + point : DamePerLevel - point;
                break;
        }
    }

    public enum TagManager{AttackDamage,SpellDame,Armor,HP,MP,Crit,CritDame,DamagePer,AttackSpeed,SpellSpeed,MoveSpeed,HitHP,
                            RegentTime,RegentHP,Freeze,Stun,Poison,Execllent,Lucky,MoreGold,Redamage,AddStr,AddAgi,AddSta,AddIntel,
                            Defense,FreezeTime,StunTime,RegentPer,ArmorPerLevel,DamePerLevel,SpellDamePerLevel}
}
