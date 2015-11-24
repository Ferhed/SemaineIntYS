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
    public void openButton(RectTransform currentCanva)
    {
        StartCoroutine(open(currentCanva));
    }
    public void closeButton(RectTransform currentCanva)
    {
        StartCoroutine(close(currentCanva));
    }


    IEnumerator open(RectTransform currentCanva)
    {
        float size = currentCanva.sizeDelta.x;

        while(size < 1.0f)
        {
            size += 0.1f;
            currentCanva.sizeDelta = new Vector2(size, 1);

            yield return new WaitForSeconds(0.01f);
        }

    }
    IEnumerator close(RectTransform currentCanva)
    {
        float size = currentCanva.sizeDelta.x;

        while (size > 0.2f)
        {
            size -= 0.1f;
            currentCanva.sizeDelta = new Vector2(size, 1);

            yield return new WaitForSeconds(0.01f);
        }

    }
}
