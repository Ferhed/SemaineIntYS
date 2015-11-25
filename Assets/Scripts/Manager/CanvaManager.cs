using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanvaManager : MonoBehaviour {
    static CanvaManager instance;
    public static CanvaManager Instance
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
    public GameObject[] TilePlayer1; // Left / Mid / Right
    public GameObject[] TilePlayer2; // Left / Mid / Right
    public Canvas[] CanvaPlayer1; // Stat / Player / Spell
    public Canvas[] CanvaPlayer2; // Stat / Player / Spell

    public GameObject[] player = new GameObject[2];

    Transform[] tilesSaved;

    public enum POSITION
    {
        LEFT,
        MID,
        RIGHT
    }

    public enum CANVA
    {
        STAT,
        PLAYER,
        SPELL
    }

	void Start () {

        tilesSaved = new Transform[2];

        DontDestroyOnLoad(this);
	}
	
	void Update () {
	}

    POSITION getPosition(Transform t, int id)
    {
        if(id == 1)
        {
            for(int i = 0; i < 3; ++i)
            {
                if (t == TilePlayer1[i].transform)
                {
                    return (POSITION)i;
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                if (t == TilePlayer2[i].transform)
                {
                    return (POSITION)i;
                }
            }
        }
        return POSITION.MID;
    }

    void SetPosition(Canvas beforeCanvas, Transform after)
    {
        GameObject tileBefore = beforeCanvas.transform.parent.gameObject;
        //Canvas canAfter = after.GetChild(0).gameObject.GetComponent<Canvas>();

        if (tileBefore.GetComponent<TileScript>().idPlayer == after.GetComponent<TileScript>().idPlayer)
        {
            beforeCanvas.transform.SetParent(after);
            beforeCanvas.transform.localPosition = new Vector3(
            Mathf.Pow(-1, tileBefore.GetComponent<TileScript>().idPlayer), 0, -0.20f);

        }
        else
        {
            tilesSaved[0] = null;
            tilesSaved[1] = null;
        }
    }

    public void ClickListener(Transform t)
    {
        if (t.GetComponent<TileScript>().idPlayer != TurnManager.Instance.currentPlayer) return;

        if(!tilesSaved[0])
        {
            if (t.GetChild(0).gameObject.GetComponent<Canvas>() == CanvaPlayer1[(int)CANVA.PLAYER] || t.GetChild(0).gameObject.GetComponent<Canvas>() == CanvaPlayer2[(int)CANVA.PLAYER])
            {
                tilesSaved[0] = t;
            }
        }
        else
        {
            tilesSaved[1] = t;

            if(t.GetComponent<TileScript>().idPlayer ==1)
            {
                SetPosition(CanvaPlayer1[(int)CANVA.PLAYER], tilesSaved[1]);
            }
            else
            {
                SetPosition(CanvaPlayer2[(int)CANVA.PLAYER], tilesSaved[1]);
            }

            player[t.GetComponent<TileScript>().idPlayer - 1].GetComponent<PlayerScript>().position = getPosition(t, t.GetComponent<TileScript>().idPlayer);
            replaceOtherCanvas();

            player[t.GetComponent<TileScript>().idPlayer - 1].GetComponent<PlayerScript>().AP--;
            player[t.GetComponent<TileScript>().idPlayer - 1].GetComponent<PlayerScript>().UpdateSprites();
        }
    }

    void replaceOtherCanvas()
    {
        TileScript currentTS = tilesSaved[1].GetComponent<TileScript>();
        switch (currentTS.myPosition)
        {
            case POSITION.LEFT:
                if(currentTS.idPlayer == 1)
                {
                    SetPosition(CanvaPlayer1[(int)CANVA.STAT], TilePlayer1[(int)POSITION.MID].transform);
                    SetPosition(CanvaPlayer1[(int)CANVA.SPELL], TilePlayer1[(int)POSITION.RIGHT].transform);
                }
                else
                {
                    SetPosition(CanvaPlayer2[(int)CANVA.STAT], TilePlayer2[(int)POSITION.MID].transform);
                    SetPosition(CanvaPlayer2[(int)CANVA.SPELL], TilePlayer2[(int)POSITION.RIGHT].transform);
                }
                break;
            case POSITION.MID:
                if (currentTS.idPlayer == 1)
                {
                    SetPosition(CanvaPlayer1[(int)CANVA.STAT], TilePlayer1[(int)POSITION.LEFT].transform);
                    SetPosition(CanvaPlayer1[(int)CANVA.SPELL], TilePlayer1[(int)POSITION.RIGHT].transform);
                }
                else
                {
                    SetPosition(CanvaPlayer2[(int)CANVA.STAT], TilePlayer2[(int)POSITION.LEFT].transform);
                    SetPosition(CanvaPlayer2[(int)CANVA.SPELL], TilePlayer2[(int)POSITION.RIGHT].transform);
                }
                break;
            case POSITION.RIGHT:
                if (currentTS.idPlayer == 1)
                {
                    SetPosition(CanvaPlayer1[(int)CANVA.STAT], TilePlayer1[(int)POSITION.LEFT].transform);
                    SetPosition(CanvaPlayer1[(int)CANVA.SPELL], TilePlayer1[(int)POSITION.MID].transform);
                }
                else
                {
                    SetPosition(CanvaPlayer2[(int)CANVA.STAT], TilePlayer2[(int)POSITION.LEFT].transform);
                    SetPosition(CanvaPlayer2[(int)CANVA.SPELL], TilePlayer2[(int)POSITION.MID].transform);
                }
                break;
        }

        tilesSaved[0] = null;
        tilesSaved[1] = null;


    }
}
