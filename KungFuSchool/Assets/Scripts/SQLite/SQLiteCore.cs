using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

public static class SQLiteCore {
    public static string connectString = "URI=file:" + Application.dataPath + "/KungFuSchool.db";
    public static IDbConnection conn = new SqliteConnection(connectString);


    /// <summary>
    /// Player information
    /// </summary>
    /// 
    public static ApplicationPlayer rootPlayer = new ApplicationPlayer();
    public static List<LevelManager> levelManager = getLevelManager();
    public static void Connect() {
        if (conn.State != ConnectionState.Open) {
            conn.Open();
        }
    }

    public static void getPlayerInfor() {
        string query = "Select * From RootPlayer";
        Connect();
        try {
            using (conn) {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read()) {
                        rootPlayer.Level = Convert.ToInt32(datas[0]);
                        rootPlayer.Exp = Convert.ToInt32(datas[1]);
                    }
                }
            }
        } catch (Exception excp) {
            Debug.Log("ERROR:getPlayerInfor " + excp);
        }
    }

    public static List<LevelManager> getLevelManager()
    {
        string query = "Select * From Level";
        Connect();
        List<LevelManager> levels = new List<LevelManager>();
        try
        {
            using (conn)
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    IDataReader datas = cmd.ExecuteReader();
                    while (datas.Read())
                    {
                        LevelManager level = new LevelManager();
                        level.Level = Convert.ToInt32(datas[0]);
                        level.Exp = Convert.ToInt32(datas[1]);
                        levels.Add(level);
                    }

                    return levels;
                }
            }
        }
        catch (Exception excp)
        {
            Debug.Log("ERROR:getLevelManager " + excp);
            return levels;
        }
    }
}
