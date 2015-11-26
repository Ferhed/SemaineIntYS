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


    void Awake()
    {
        lifeSprite = transform.Find("Life").GetComponent<SpriteRenderer>();
        APSprite = transform.Find("PAs").GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {

        AP = 0;

        UpdateSprites();
    }

    public void UpdateSprites()
    {
        lifeSprite.sprite = MenuManager.Instance.lifes[life];
        APSprite.sprite = MenuManager.Instance.APs[AP];

        if (AP <= 0 && TurnManager.Instance.currentPlayer == id)
        {

            TurnManager.Instance.Endturn();
        }
        if(life == 0)
        {
            MenuManager.Instance.Affichage("Tu as perdu", id - 1, true);
            MenuManager.Instance.Affichage("Tu es Victorieux", id % 2, true);
            MenuManager.Instance.finishGame(id%2);
            TurnManager.instance.gameFinished = true;
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
        if (life <= 0)
        {
            life = 0;
        }
        screenshakeManager.instance.Shake(0.05f, 0.2f);
        UpdateSprites();

        SoundManager.Instance.playSound(SoundManager.Instance.castleDamage, 1f, gameObject);
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
