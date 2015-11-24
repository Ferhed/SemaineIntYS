using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {


    public static PlayerManager instance = null;
    public static PlayerManager Instance
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

        playerList = new List<PlayerScript>();
    }

    public List<PlayerScript> playerList;

    public GameObject prefab;

    // Use this for initialization
    void Start () {
        InstanciatePlayer(1);
        InstanciatePlayer(2);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void addPlayer(PlayerScript player)
    {
        playerList.Add(player);
        player.playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void InstanciatePlayer(int n)
    {
        GameObject instance = Instantiate(prefab) as GameObject;
        instance.GetComponent<PlayerScript>().SetId(n);
        TurnManager.instance.playerIdList.Add(n);
        if(n == 1)
        {
            instance.transform.parent = CanvaManager.Instance.CanvaPlayer1[(int)CanvaManager.CANVA.PLAYER].transform;
            
        }
        else
        {
            instance.transform.parent = CanvaManager.Instance.CanvaPlayer2[(int)CanvaManager.CANVA.PLAYER].transform;
            instance.transform.Rotate(new Vector3(0, 180, 0));
        }
        CanvaManager.Instance.player[n-1] = instance;
        instance.transform.localPosition = new Vector3(0.5f, 0, -0.50f);
        
    }
}
