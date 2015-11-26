using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {

    static TileManager instance;
    public static TileManager Instance
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

    public Vector3[,] sizesTiles = new Vector3[3, 2];
    public GameObject[,] tiles = new GameObject[4,3];

    // Use this for initialization
    void Start () {

        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach(GameObject go in allTiles)
        {
            if (go.name.Contains("1"))
            {
                if (go.name.Contains("Left"))
                {
                    tiles[0,0] = go;
                }
                else if (go.name.Contains("Mid"))
                {
                    tiles[0,1] = go;
                } else
                {
                    tiles[0,2] = go;
                }
            }
            else if (go.name.Contains("2"))
            {
                if (go.name.Contains("Left"))
                {
                    tiles[1,0] = go;
                }
                else if (go.name.Contains("Mid"))
                {
                    tiles[1,1] = go;
                }
                else 
                {
                    tiles[1,2] = go;
                }
            }
            else if (go.name.Contains("3"))
            {
                if (go.name.Contains("Left"))
                {
                    tiles[2,0] = go;
                }
                else if (go.name.Contains("Mid"))
                {
                    tiles[2,1] = go;
                }
                else
                {
                    tiles[2,2] = go;
                }
            }
            else if (go.name.Contains("4"))
            {
                if (go.name.Contains("Left"))
                {
                    tiles[3,0] = go;
                }
                else if (go.name.Contains("Mid"))
                {
                    tiles[3,1] = go;
                }
                else
                {
                    tiles[3,2] = go;
                }
            }
        }
        setSizes();
    }
	

    void setSizes()
    {
        //P1
        sizesTiles[0,0]= new Vector3(0f,0f,0f);
        sizesTiles[1,0]= new Vector3(0f,0f,0f);
        sizesTiles[2,0]= new Vector3(0f,0f,0f);
        //P2
        sizesTiles[0,1]= new Vector3(0f,0f,0f);
        sizesTiles[1,1]= new Vector3(0f,0f,0f);
        sizesTiles[2,1]= new Vector3(0f,0f,0f);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
