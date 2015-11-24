using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    public int idPlayer;

    public bool inPlayerRange = true;

    public GameObject monsterInside = null;

    public CanvaManager.POSITION myPosition;

	void Start () {
	    
	}
	
	void Update () {
	
	}
    void OnMouseDown()
    {
        if (inPlayerRange)
        {
            CanvaManager.Instance.ClickListener(gameObject.transform);
        }
    }
}
