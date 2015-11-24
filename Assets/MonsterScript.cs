using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour {

    public enum TYPE
    {
        NORMAL,
    }

    public TYPE type = TYPE.NORMAL;

    public int life;
    public int attack;
    public int id;

    public GameObject currentTile;
    GameObject targetTile;
    bool arrived = false;

    public Vector2 pos;
    public Vector2 currentPos;

    // Use this for initialization
    void Start () {
        switch (type)
        {
            case TYPE.NORMAL:
                life = 1;
                attack = 2;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doAction(int id)
    {
        pos = currentPos;

        if (id == 1)
        {
            if(pos.x < 3)
            {
                pos.x += 1;
            }
            else
            {
                arrived = true;
            }
            if (pos.x == 2)
            {
                switch ((int)pos.y)
                {
                    case 0: pos.y = 2;
                        break;
                    case 2: pos.y = 0;
                        break;
                }
            }
        }
        else
        {
            if (pos.x > 0)
            {
                pos.x -= 1;
            }
            else
            {
                arrived = true;
            }
            if (pos.x == 1)
            {
                switch ((int)pos.y)
                {
                    case 0:
                        pos.y = 2;
                        break;
                    case 2:
                        pos.y = 0;
                        break;
                }
            }
        }

        targetTile = TileManager.Instance.tiles[(int)pos.x, (int)pos.y];
        TileScript TS = targetTile.GetComponent<TileScript>();
        GameObject monsterInside = TS.monsterInside;
        if (arrived)
        {
            Debug.Log("je suis arrivé");
            if (id == 1)
            {
                CanvaManager.Instance.player[1].GetComponent<PlayerScript>().receiveDamage(attack);
            }
            else
            {
                CanvaManager.Instance.player[0].GetComponent<PlayerScript>().receiveDamage(attack);
            }
        }
        else if (monsterInside == null)
        {
            currentPos = pos;
            move();
        }
        else if(monsterInside.GetComponent<MonsterScript>().id == id)
        {
            //do nothing ya deja un copain dessus
        }
        else if (monsterInside.GetComponent<MonsterScript>().id != id)
        {
            monsterInside.GetComponent<MonsterScript>().receiveDamage(attack);
        }

       
    }

    public void receiveDamage(int damage)
    {
        life -= damage;
        Debug.Log(life + " Aie !");
        if(life <=0)
        {
            destroy();
        }
    }
    void destroy()
    {
        CanvaManager.Instance.player[id - 1].GetComponent<PlayerScript>().monsters.Remove(gameObject);

        currentTile.GetComponent<TileScript>().monsterInside = null;
        Destroy(gameObject);
    }

    void move()
    {

        currentTile.GetComponent<TileScript>().monsterInside = null;
        currentTile = targetTile;
        currentTile.GetComponent<TileScript>().monsterInside = gameObject;
        transform.position = currentTile.transform.position;
    }
}
