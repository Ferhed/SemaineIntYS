using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterScript : MonoBehaviour {

    public enum TYPE
    {
        NORMAL,
        LITTLE,
        FAT,
        OLD,
    }

    public TYPE type = TYPE.NORMAL;

    public int life;
    public int attack;
    public int nbMove;
    int nbMoveMax;
    int normalAttack;

    public int id;
    public float speed = 1f;

    public GameObject currentTile;
    GameObject targetTile;
    bool arrived = false;

    public List<MonsterScript> monsterTarget = new List<MonsterScript>();

    public Vector2 pos;
    public Vector2 currentPos;

    public SpriteRenderer lifeSprite;
    public SpriteRenderer attackSprite;

    // Use this for initialization
    void Start () {

        MonsterManager mM = MonsterManager.Instance;

        lifeSprite = transform.Find("Life").GetComponent<SpriteRenderer>();
        attackSprite = transform.Find("Attack").GetComponent<SpriteRenderer>();

        switch (type)
        {
            case TYPE.NORMAL:
                life = mM.normalLife;
                attack = mM.normalAttack;
                nbMoveMax = mM.normalMove;
                break;
            case TYPE.LITTLE:
                life = mM.littleLife;
                attack = mM.littleAttack;
                nbMoveMax = mM.littleMove;
                break;
            case TYPE.FAT:
                life = mM.fatLife;
                attack = mM.fatAttack;
                nbMoveMax = mM.fatMove;
                break;
            case TYPE.OLD:
                life = mM.oldLife;
                attack = mM.oldAttack;
                nbMoveMax = mM.oldMove;
                break;
        }
        nbMove = nbMoveMax;
        normalAttack = attack;

        UpdateSprites();
    }
	
    public void UpdateSprites()
    {
        if(life >= 0)
        {
            lifeSprite.sprite = MenuManager.Instance.lifes[life];
        }
        else
        {
            lifeSprite.sprite = MenuManager.Instance.lifes[0];
        }
        attackSprite.sprite = MenuManager.Instance.attacks[attack];
    }

	// Update is called once per frame
	void Update () {
    }

    public void ReceiveBuff(Vector2 buff)
    {
        nbMove += (int)buff.x;
        attack += (int)buff.y;
        SoundManager.Instance.playSound(SoundManager.Instance.boost, 1f, gameObject);
        GameObject boeuf = Instantiate(SpellManager.Instance.spells[2], transform.position, Quaternion.identity) as GameObject;
        boeuf.transform.Rotate(new Vector3(-90,0,0));
        Destroy(boeuf, 2f);
        UpdateSprites();
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
            else
            {
                arrived = true;
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
        return pos;

    }

    public MonsterScript doAction(int id)
    {
        pos = getPos(currentPos, id);

        targetTile = TileManager.Instance.tiles[(int)pos.x, (int)pos.y];
        TileScript TS = targetTile.GetComponent<TileScript>();
        GameObject monsterInside = TS.monsterInside;
        if (arrived)
        {
            if (id == 1)
            {
                PlayerScript pS = CanvaManager.Instance.player[1].GetComponent<PlayerScript>();
                if ((int)pS.position == pos.y)
                {
                    return this;
                }
                else
                {
                    pS.receiveDamage(attack);
                    attack = normalAttack;
                }
            }
            else
            {
                PlayerScript pS = CanvaManager.Instance.player[0].GetComponent<PlayerScript>();
                if ((int)pS.position == pos.y)
                {
                    return this;
                }
                else
                {
                    pS.receiveDamage(attack);
                    attack = normalAttack;
                }
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
            attack = normalAttack;
        }

        return null;
    }
    public MonsterScript doFatAction(int id)
    {
        pos = getPos(currentPos, id);

        Vector2[] posTab = new Vector2[3];
        if (!arrived)
        {
            if (id == 1)
            {
                posTab[0] = new Vector2(currentPos.x + 1, 0);
                posTab[1] = new Vector2(currentPos.x + 1, 1);
                posTab[2] = new Vector2(currentPos.x + 1, 2);
            }
            else
            {
                posTab[0] = new Vector2(currentPos.x - 1, 0);
                posTab[1] = new Vector2(currentPos.x - 1, 1);
                posTab[2] = new Vector2(currentPos.x - 1, 2);
            }



            foreach (Vector2 tile in posTab)
            {
                GameObject GO = TileManager.Instance.tiles[(int)tile.x, (int)tile.y].GetComponent<TileScript>().monsterInside;
                if (GO)
                {
                    if (GO.GetComponent<MonsterScript>().id != id)
                    {
                        monsterTarget.Add(GO.GetComponent<MonsterScript>());
                    }
                }

            }
        }


        targetTile = TileManager.Instance.tiles[(int)pos.x, (int)pos.y];
        TileScript TS = targetTile.GetComponent<TileScript>();
        GameObject monsterInside = TS.monsterInside;
        if (arrived)
        {
            if (id == 1)
            {
                PlayerScript pS = CanvaManager.Instance.player[1].GetComponent<PlayerScript>();
                if ((int)pS.position == pos.y)
                {
                    return this;
                }
                else
                {
                    pS.receiveDamage(attack);
                    attack = normalAttack;
                }
            }
            else
            {
                PlayerScript pS = CanvaManager.Instance.player[0].GetComponent<PlayerScript>();
                if ((int)pS.position == pos.y)
                {
                    return this;
                }
                else
                {
                    pS.receiveDamage(attack);
                    attack = normalAttack;
                }
            }
        }
        else if (monsterTarget.Count > 0)
        {
            foreach(MonsterScript MS in monsterTarget)
            {
                MS.receiveDamage(attack);
            }
            monsterTarget.Clear();
        }
        else if (monsterTarget.Count == 0)
        {
            if(!monsterInside)
            {
                currentPos = pos;
                move();
            }

        }

        return null;
    }

    public MonsterScript doOldAction(int id)
    {
        Vector2[] posTab = new Vector2[3];
        posTab[0] = getPos(currentPos, id);
        posTab[1] = getPos(posTab[0], id);
        posTab[2] = getPos(posTab[1], id);
        

        foreach(Vector2 tile in posTab)
        {
            GameObject GO = TileManager.Instance.tiles[(int)tile.x, (int)tile.y].GetComponent<TileScript>().monsterInside;
            if (GO)
            {
                if (GO.GetComponent<MonsterScript>().id != id)
                {
                    monsterTarget.Add(GO.GetComponent<MonsterScript>());
                }
            }

        }

        pos = getPos(currentPos, id);

        targetTile = TileManager.Instance.tiles[(int)pos.x, (int)pos.y];
        TileScript TS = targetTile.GetComponent<TileScript>();
        GameObject monsterInside = TS.monsterInside;
        if (arrived)
        {
            if (id == 1)
            {
                PlayerScript pS = CanvaManager.Instance.player[1].GetComponent<PlayerScript>();
                if ((int)pS.position == pos.y)
                {
                    return this;
                }
                else
                {
                    pS.receiveDamage(attack);
                    attack = normalAttack;
                }
            }
            else
            {
                PlayerScript pS = CanvaManager.Instance.player[0].GetComponent<PlayerScript>();
                if ((int)pS.position == pos.y)
                {
                    return this;
                }
                else
                {
                    pS.receiveDamage(attack);
                    attack = normalAttack;
                }
            }
        }
        else if (monsterTarget.Count > 0)
        {
            monsterTarget[0].GetComponent<MonsterScript>().receiveDamage(attack);
            monsterTarget.Clear();
        }
        else if (monsterTarget.Count == 0)
        {
            if (!monsterInside)
            {
                currentPos = pos;
                move();
            }

        }

        return null;
    }

    public void receiveDamage(int damage)
    {
        life -= damage;
        if(life < 0)
        {
            life = 0;
        }
        UpdateSprites();
        if (life <=0)
        {
            destroy();
        }
    }

    public void destroy()
    {
        CanvaManager.Instance.player[id - 1].GetComponent<PlayerScript>().monsters.Remove(gameObject);

        currentTile.GetComponent<TileScript>().monsterInside = null;
        //Destroy(gameObject);

        StartCoroutine(DestroyAnimation());
    }

    IEnumerator DestroyAnimation()
    {

        while(GetComponent<SpriteRenderer>().color.a > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a - 0.1f);
           lifeSprite.color = new Color(lifeSprite.color.r, lifeSprite.color.g, lifeSprite.color.b, lifeSprite.color.a - 0.1f);
           attackSprite.color = new Color(attackSprite.color.r, attackSprite.color.g, attackSprite.color.b, attackSprite.color.a - 0.1f);
            yield return new WaitForSeconds(0.06f);
        }
        Destroy(gameObject);
    }

    void move()
    {

        currentTile.GetComponent<TileScript>().monsterInside = null;
        currentTile = targetTile;
        currentTile.GetComponent<TileScript>().monsterInside = gameObject;
        StartCoroutine(moveToPosition(currentTile.transform.position));
    }

    IEnumerator moveToPosition(Vector3 posi)
    {
        

        while (Vector3.Distance(transform.position, posi) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, posi, Time.deltaTime*speed);

            yield return null;
        }


        nbMove--;

        if (nbMove > 0)
        {
            pos = getPos(currentPos, id);

            targetTile = TileManager.Instance.tiles[(int)pos.x, (int)pos.y];
            TileScript TS = targetTile.GetComponent<TileScript>();
            GameObject monsterInside = TS.monsterInside;

            if (!monsterInside)
            {
                currentTile.GetComponent<TileScript>().monsterInside = null;
                currentTile = targetTile;
                currentTile.GetComponent<TileScript>().monsterInside = gameObject;

                currentPos = pos;

                StartCoroutine(moveToPosition(currentTile.transform.position));
            }

            
        }
        else
        {
            nbMove = nbMoveMax;
        }

        yield return null;
    }
}
