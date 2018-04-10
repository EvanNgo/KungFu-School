using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    #region Singleton
    public static PlayerManager instance {
        get {
            if (_instance == null) {
                Debug.Log("Khoi tao");
                GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
                _instance = gameManager.GetComponent<PlayerManager>();
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
	void Start () {
        Debug.Log("haha");

	}
}
