using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
    void Start()
    {        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickPlay()
    {
        Application.LoadLevel(2);
    }

    public void BackNavigationButton() 
    {
        Application.LoadLevel(Application.loadedLevel - 1);
    }
}
