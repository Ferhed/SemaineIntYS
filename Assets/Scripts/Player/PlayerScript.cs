using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    public int id;
    public Color playerColor;
    public CanvaManager.POSITION position = CanvaManager.POSITION.MID;
    public List<GameObject> monsters;

    public int life = 15;
    public int MaxAP = 3;
    [HideInInspector]public int AP;

    public SpriteRenderer lifeSprite;
    public SpriteRenderer APSprite;

    // Use this for initialization
    void Start () {

        lifeSprite = transform.Find("Life").GetComponent<SpriteRenderer>();
        APSprite = transform.Find("PAs").GetComponent<SpriteRenderer>();


        PlayerManager.instance.addPlayer(this);

        AP = 0;

        UpdateSprites();
    }

    public void UpdateSprites()
    {
        lifeSprite.sprite = MenuManager.Instance.lifes[life];
        APSprite.sprite = MenuManager.Instance.APs[AP];

        if (AP <= 0)
        {
            Debug.Log(id);

            TurnManager.Instance.Endturn();
        }
    }

    // Update is called once per frame
    void Update () {
	    
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
        UpdateSprites();
        if (life <= 0)
        {
            Debug.Log("loul j'ai perdu");
        }
    }

    public void MoveMonsters()
    {
        List<MonsterScript> deadMonsters = new List<MonsterScript>();

        foreach(GameObject monster in monsters)
        {
            if (monster.GetComponent<MonsterScript>().type == MonsterScript.TYPE.OLD)
            {
                deadMonsters.Add(monster.GetComponent<MonsterScript>().doOldAction(id));
            }
            else if (monster.GetComponent<MonsterScript>().type == MonsterScript.TYPE.FAT)
            {
                deadMonsters.Add(monster.GetComponent<MonsterScript>().doFatAction(id));
            }
            else
            {
                deadMonsters.Add(monster.GetComponent<MonsterScript>().doAction(id));
            }
            monster.GetComponent<MonsterScript>().UpdateSprites();
        }

        foreach (MonsterScript monster in deadMonsters)
        {
            if (monster)
            {
                monster.destroy();
            }
        }
        deadMonsters.Clear();
    }

}
