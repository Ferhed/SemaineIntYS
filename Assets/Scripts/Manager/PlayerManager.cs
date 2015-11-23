using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {


    public static PlayerManager instance = null;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<PlayerScript> playerList;

    public GameObject prefab;
    public int regen;

    // Use this for initialization
    void Start () {
        playerList = new List<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addPlayer(PlayerScript player)
    {
        playerList.Add(player);
        player.playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void InstanciatePlayer()
    {
        GameObject instance = Instantiate(prefab) as GameObject;
        instance.GetComponent<PlayerScript>().SetId(playerList.Count+1);
        TurnManager.instance.playerIdList.Add(playerList.Count+1);
    }
}
