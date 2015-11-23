using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    public int idPlayer;

    public CanvaManager.POSITION myPosition;

	void Start () {
	    
	}
	
	void Update () {
	
	}
    void OnMouseDown()
    {
        CanvaManager.Instance.ClickListener(gameObject.transform);
    }
}
