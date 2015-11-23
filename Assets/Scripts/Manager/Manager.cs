using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    //
    float waitToload = 5f;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        if (Application.loadedLevel == 0)
            Application.LoadLevelAsync(1);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
