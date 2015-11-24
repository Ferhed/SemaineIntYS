﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.Networking;

public class TurnManager : MonoBehaviour {

    public static TurnManager instance = null;
    public static TurnManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        playerIdList = new List<int>();
    }

    public List<int> playerIdList;
    public int currentPlayer;

    // Use this for initialization
    void Start () {
        currentPlayer = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(playerIdList.Count);
	}

    public void Endturn()
    {
        currentPlayer = currentPlayer % playerIdList.Count;

        currentPlayer++;
        if(currentPlayer == 1)
        {
            EndOfGlobalTurn();
        }
        
        PlayerManager.Instance.playerList[currentPlayer - 1].GetComponent<PlayerScript>().MoveMonsters();
    }

    public void EndOfGlobalTurn()
    {
        Debug.Log("fin de tour global");
    }

    public bool canPlay(int idPlayer)
    {
        return idPlayer == currentPlayer;
    }
}
