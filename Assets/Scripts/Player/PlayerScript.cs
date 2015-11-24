using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    public int id;
    public Color playerColor;
    public CanvaManager.POSITION position = CanvaManager.POSITION.MID;
    public List<GameObject> monsters;

    public int life = 15;
    public int AP = 3;

    // Use this for initialization
    void Start () {
        PlayerManager.instance.addPlayer(this);
	}
	
	// Update is called once per frame
	void Update () {
	    if(AP <= 0)
        {
            TurnManager.Instance.Endturn();
            AP = 3;
        }
	}

    public int GetId()
    {
        return id;
    }

    public void SetId(int Id)
    {
        id = Id;
    }

    public void receiveDamage(int damage)
    {
        life -= damage;
        Debug.Log(life + " Ouch !");
        if (life <= 0)
        {
            Debug.Log("loul j'ai perdu");
        }
    }

    public void MoveMonsters()
    {
        foreach(GameObject monster in monsters)
        {
            monster.GetComponent<MonsterScript>().doAction(id);
        }
    }

}
