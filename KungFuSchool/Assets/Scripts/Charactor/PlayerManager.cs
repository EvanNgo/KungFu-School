using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    #region Singleton
    public static PlayerManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlayerManager>();
            }
            return _instance;
        }
    }
    static PlayerManager _instance;

    void Awake ()
    {   
        player = SQLiteCore.getPlayerInfor();
        _instance = this;
    }

    #endregion
	// Use this for initialization
    public Player player;

    public int getSta(){
        return player.Sta;
    }
    public int getAgi(){
        return player.Agi;
    }
    public int getStr(){
        return player.Str;
    }
    public int getIntel(){
        return player.Intel;
    }

}
