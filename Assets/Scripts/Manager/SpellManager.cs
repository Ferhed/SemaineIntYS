using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {

    public GameObject[] monsters = new GameObject[4];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpellOne(int currentPlayer)
    {
        Spell(currentPlayer);
    }
    public void SpellTwo(int currentPlayer)
    {
        Spell(currentPlayer);
    }
    public void SpellThree(int currentPlayer)
    {
        Spell(currentPlayer);
    }
    public void SpellFour(int currentPlayer)
    {
        Spell(currentPlayer);
    }
    public void InvocationOne(int currentPlayer)
    {
        invoc(currentPlayer, monsters[0]);
    }
    public void InvocationTwo(int currentPlayer)
    {
        invoc(currentPlayer, monsters[0]);
    }
    public void InvocationThree(int currentPlayer)
    {
        invoc(currentPlayer, monsters[0]);
    }
    public void InvocationFour(int currentPlayer)
    {
        invoc(currentPlayer, monsters[0]);
    }

    void invoc(int currentPlayer, GameObject monster)
    {
        if (TurnManager.instance.canPlay(currentPlayer))
        {
            GameObject player = CanvaManager.Instance.player[currentPlayer - 1];
            PlayerScript PS = player.GetComponent<PlayerScript>();
            GameObject go;
            GameObject target;

            Vector2 pos = Vector2.zero;

            if(currentPlayer == 1)
            {
                pos = new Vector2(0, (int)PS.position);
            }
            else
            {
                pos = new Vector2(3, (int)PS.position);
            }

            target = TileManager.Instance.tiles[(int)pos.x, (int)pos.y];
            go = Instantiate(monster, target.transform.position , Quaternion.identity) as GameObject;
            target.GetComponent<TileScript>().monsterInside = go;
            MonsterScript MS = go.GetComponent<MonsterScript>();
            MS.currentTile = target;
            MS.currentPos = pos;
            MS.id = currentPlayer;

            PS.monsters.Add(go);

            player.GetComponent<PlayerScript>().AP--;
        }
    }

    void Spell(int currentPlayer)
    {
        if (TurnManager.instance.canPlay(currentPlayer))
        {
            GameObject player = CanvaManager.Instance.player[currentPlayer - 1];

            player.GetComponent<PlayerScript>().AP--;
        }
    }
}
