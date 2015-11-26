using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {
    static SpellManager instance;
    public static SpellManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }
    public GameObject[] monsters = new GameObject[4];


    public GameObject[] spells;

    public int damageFireBall = 2;
    public int damageExplosion = 1;
    public int healAmount = 4;
    public Vector2 boost = Vector2.one;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpellOne(int currentPlayer)
    {
        Spell(currentPlayer, 1);
    }
    public void SpellTwo(int currentPlayer)
    {
        Spell(currentPlayer, 2);
    }
    public void SpellThree(int currentPlayer)
    { 
        Spell(currentPlayer, 3);
    }
    public void SpellFour(int currentPlayer)
    {
        if (CanvaManager.Instance.player[currentPlayer - 1].GetComponent<PlayerScript>().AP >= 2)
        {
            Spell(currentPlayer, 4);
        }
    }
    public void InvocationOne(int currentPlayer)
    {
        invoc(currentPlayer, monsters[(currentPlayer-1) * 4]);
    }
    public void InvocationTwo(int currentPlayer)
    {
        invoc(currentPlayer, monsters[(currentPlayer - 1) * 4 + 1]);
    }
    public void InvocationThree(int currentPlayer)
    {
        invoc(currentPlayer, monsters[(currentPlayer - 1) * 4 + 2]);
    }
    public void InvocationFour(int currentPlayer)
    {
        invoc(currentPlayer, monsters[(currentPlayer - 1) * 4 + 3]);
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
            if (!target.GetComponent<TileScript>().monsterInside)
            {
                go = Instantiate(monster, target.transform.position + Vector3.up/10, Quaternion.identity) as GameObject;

                go.transform.rotation = (player.transform.rotation);

                target.GetComponent<TileScript>().monsterInside = go;
                MonsterScript MS = go.GetComponent<MonsterScript>();
                MS.currentTile = target;
                MS.currentPos = pos;
                MS.id = currentPlayer;

                PS.monsters.Add(go);

                player.GetComponent<PlayerScript>().AP--;
                player.GetComponent<PlayerScript>().UpdateSprites();


                GameObject spawn = Instantiate(SpellManager.Instance.spells[3], go.transform.position, Quaternion.identity) as GameObject;
                spawn.transform.Rotate(new Vector3(-90, 0, 0));
                Destroy(spawn, 2f);


            }

        }
    }

    void Spell(int currentPlayer, int spell)
    {
        if (TurnManager.instance.canPlay(currentPlayer))
        {
            GameObject player = CanvaManager.Instance.player[currentPlayer - 1];


            switch (spell)
            {
                case 1:
                    FireBall(currentPlayer);
                    player.GetComponent<PlayerScript>().AP--;
                    break;
                case 2:
                    Boost(currentPlayer);
                    player.GetComponent<PlayerScript>().AP--;
                    break;
                case 3:
                    Explosion(currentPlayer);
                    player.GetComponent<PlayerScript>().AP--;
                    break;
                case 4:
                    Heal(currentPlayer);
                    player.GetComponent<PlayerScript>().AP -= 2;
                    break;
            }
            player.GetComponent<PlayerScript>().UpdateSprites();
        }
    }

    void FireBall(int idPlayer)
    {

        GameObject player = CanvaManager.Instance.player[idPlayer - 1];
        
        List<MonsterScript> monsterTarget = new List<MonsterScript>();

        Vector2[] posTab = new Vector2[4];


        if(idPlayer == 1)
        {
            posTab[0] = new Vector2(0, (int)player.GetComponent<PlayerScript>().position);
        }
        else
        {
            posTab[0] = new Vector2(3, (int)player.GetComponent<PlayerScript>().position);
        }

        posTab[1] = getPos(posTab[0], idPlayer);
        posTab[2] = getPos(posTab[1], idPlayer);
        posTab[3] = getPos(posTab[2], idPlayer);

        foreach (Vector2 tile in posTab)
        {
            GameObject GO = TileManager.Instance.tiles[(int)tile.x, (int)tile.y].GetComponent<TileScript>().monsterInside;
            if (GO)
            {
                if (GO.GetComponent<MonsterScript>().id != idPlayer)
                {
                    monsterTarget.Add(GO.GetComponent<MonsterScript>());
                }
            }
        }
        
        GameObject go = Instantiate(spells[0], player.transform.position, Quaternion.identity) as GameObject;


        if (monsterTarget.Count > 0)
        {
            go.GetComponent<FireBall>().SetTarget(monsterTarget[0].gameObject, idPlayer);
        }
        else
        {
            GameObject target;
            if (idPlayer == 1)
            {
                if(player.GetComponent<PlayerScript>().position == CanvaManager.POSITION.LEFT)
                {
                    target = CanvaManager.Instance.TilePlayer2[(int)CanvaManager.POSITION.RIGHT];
                }
                else
                {
                     target = CanvaManager.Instance.TilePlayer2[(int)player.GetComponent<PlayerScript>().position%2];
                }
            }
            else
            {
                if (player.GetComponent<PlayerScript>().position == CanvaManager.POSITION.LEFT)
                {
                    target = CanvaManager.Instance.TilePlayer1[(int)CanvaManager.POSITION.RIGHT];
                }
                else
                {
                    target = CanvaManager.Instance.TilePlayer1[(int)player.GetComponent<PlayerScript>().position % 2];
                }
            }
            go.GetComponent<FireBall>().SetTarget(target.gameObject, idPlayer);
            //PlayerManager.Instance.playerList[idPlayer % 2].GetComponent<PlayerScript>().receiveDamage(damageFireBall);
        }

    }

    void Boost(int idPlayer)
    {
        GameObject player = CanvaManager.Instance.player[idPlayer - 1];
        List<MonsterScript> monsterTarget = new List<MonsterScript>();

        Vector2[] posTab = new Vector2[4];


        if (idPlayer == 1)
        {
            posTab[0] = new Vector2(0, (int)player.GetComponent<PlayerScript>().position);
        }
        else
        {
            posTab[0] = new Vector2(3, (int)player.GetComponent<PlayerScript>().position);
        }

        posTab[1] = getPos(posTab[0], idPlayer);
        posTab[2] = getPos(posTab[1], idPlayer);
        posTab[3] = getPos(posTab[2], idPlayer);

        foreach (Vector2 tile in posTab)
        {
            GameObject GO = TileManager.Instance.tiles[(int)tile.x, (int)tile.y].GetComponent<TileScript>().monsterInside;
            if (GO)
            {
                if (GO.GetComponent<MonsterScript>().id == idPlayer)
                {
                    monsterTarget.Add(GO.GetComponent<MonsterScript>());
                }
            }
        }

        if (monsterTarget.Count > 0)
        {
            monsterTarget[0].GetComponent<MonsterScript>().ReceiveBuff(boost);
        }
    }

    void Explosion(int idPlayer)
    {
        Vector2[] posTab = new Vector2[3];
        if (idPlayer == 1)
        {
            posTab[0] = new Vector2(0, 0);
            posTab[1] = new Vector2(0, 1);
            posTab[2] = new Vector2(0, 2);
        }
        else
        {
            posTab[0] = new Vector2(3, 0);
            posTab[1] = new Vector2(3, 1);
            posTab[2] = new Vector2(3, 2);
        }


        StartCoroutine(Explosions(posTab, idPlayer));
        
        
    }

    IEnumerator Explosions(Vector2[] tiles, int idPlayer)
    {
        foreach (Vector2 tile in tiles)
        {
            GameObject GO = TileManager.Instance.tiles[(int)tile.x, (int)tile.y].GetComponent<TileScript>().monsterInside;

            GameObject explosion = Instantiate(spells[1], TileManager.Instance.tiles[(int)tile.x, (int)tile.y].transform.position + Vector3.up / 2, Quaternion.identity) as GameObject;
            explosion.transform.Rotate(new Vector3(-90, 0, 0));
            Destroy(explosion, 2f);

            SoundManager.Instance.playSound(SoundManager.Instance.explosion, 1f, TileManager.Instance.tiles[(int)tile.x, (int)tile.y]);

            if (GO)
            {
                if (GO.GetComponent<MonsterScript>().id != idPlayer)
                {
                    GO.GetComponent<MonsterScript>().receiveDamage(damageExplosion);
                }
            }

            yield return new WaitForSeconds (0.3f);

        }
    }

    void Heal(int idPlayer)
    {
        //fx soin
        CanvaManager.Instance.player[idPlayer - 1].GetComponent<PlayerScript>().life += healAmount;

        if (CanvaManager.Instance.player[idPlayer - 1].GetComponent<PlayerScript>().life > 20)
        {
            CanvaManager.Instance.player[idPlayer - 1].GetComponent<PlayerScript>().life = 20;
        }

        CanvaManager.Instance.player[idPlayer - 1].transform.Find("Soin").GetComponent<ParticleSystem>().Play();
        SoundManager.Instance.playSound(SoundManager.Instance.heal, 1f, CanvaManager.Instance.player[idPlayer - 1]);
    }

    Vector2 getPos(Vector2 currentPos, int id)
    {
        Vector2 pos;
        pos = currentPos;

        if (id == 1)
        {
            if (pos.x < 3)
            {
                pos.x += 1;
            }
            if (pos.x == 2)
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
        else
        {
            if (pos.x > 0)
            {
                pos.x -= 1;
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
        return pos;

    }
}
