using UnityEngine;
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

        playerIdList = new List<int>();
    }

    public List<int> playerIdList;
    public int currentPlayer;
    public bool gameFinished = false;

    // Use this for initialization
    void Start () {
        currentPlayer = 1;
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
        

        PlayerManager.Instance.playerList[currentPlayer - 1].GetComponent<PlayerScript>().AP = PlayerManager.Instance.playerList[currentPlayer - 1].GetComponent<PlayerScript>().MaxAP;
        PlayerManager.Instance.playerList[currentPlayer - 1].GetComponent<PlayerScript>().UpdateSprites();

        MenuManager.Instance.Affichage("A toi de jouer",currentPlayer-1,false);
        MenuManager.Instance.Affichage("A ton adversaire",currentPlayer%2,false);
    }

    public void EndOfGlobalTurn()
    {
    }

    public bool canPlay(int idPlayer)
    {
        return idPlayer == currentPlayer;
    }
}
