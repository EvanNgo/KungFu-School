using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player{
    //Basic Stat
    public int Level;
    public int Exp;
    public int Gold;
    public int Point;
    public int RemainPoint;
    public int Str;
    public int Agi;
    public int Sta;
    public int Intel;

    //Stat
    public int Health;
    public int Mana;
    public int AttackDame;
    public int SpellDame;
    public int Armor;

    public int GetAttackDame(){
        int dame = AttackDame;
        return dame;
    }
}
