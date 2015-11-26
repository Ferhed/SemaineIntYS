using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    static MenuManager instance;
    public static MenuManager Instance
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

    public Sprite[] lifes;
    public Sprite[] APs;
    public Sprite[] attacks;
    public Sprite[] MPs;
    public GameObject[] darkVoid;
    public GameObject button;
    public GameObject rainAndFirework;

    public GameObject[] panelText;

    public GameObject[] fire;
    public GameObject[] fireLight;

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickPlay()
    {
        Application.LoadLevel(1);
    }

    public void goMenu() 
    {
        Application.LoadLevel(0);
    }
    public void openButton(RectTransform currentCanva)
    {
        StartCoroutine(open(currentCanva));
    }
    public void closeButton(RectTransform currentCanva)
    {
        StartCoroutine(close(currentCanva));
    }

    public void Affichage(string text, int ID, bool finish)
    {
        Text test = panelText[ID].GetComponentInChildren<Text>();
        test.text = text;
        StartCoroutine(UIAffichage(panelText[ID], panelText[ID].GetComponentInChildren<Text>(), finish));
    }

    public void finishGame(int ID)
    {
        fire[(int)Mathf.Pow(ID - 1, 2)].SetActive(false);
        fireLight[(int)Mathf.Pow(ID - 1, 2)].SetActive(false);

        darkVoid[0].SetActive(true);
        darkVoid[1].SetActive(true);
        darkVoid[ID].GetComponent<Image>().color = new Color(0,0,0,0.3f);
        rainAndFirework.SetActive(true);
        rainAndFirework.transform.Rotate(new Vector3(0, Mathf.Pow(ID - 1, 2) * 180,0));
        Invoke("launchButton", 1);
    }

    void launchButton()
    {
        button.SetActive(true);
    }
    

    IEnumerator UIAffichage(GameObject panel, Text text, bool finished)
    {
        Debug.Log(finished);
        Color colorPanel = panel.GetComponent<Image>().color;
        Color colorText = text.color;
        float currentColor = 0;
        while(currentColor < 1)
        {
            currentColor += 0.05f;
            panel.GetComponent<Image>().color = new Color(colorPanel.r, colorPanel.g, colorPanel.b, currentColor / 2);
            text.color = new Color(colorText.r, colorText.g, colorText.b, currentColor);

            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        if (!finished)
        {
            while (currentColor > 0)
            {
                currentColor -= 0.05f;
                currentColor -= 0.025f;
                panel.GetComponent<Image>().color = new Color(colorPanel.r, colorPanel.g, colorPanel.b, currentColor / 2);
                text.color = new Color(colorText.r, colorText.g, colorText.b, currentColor);

                yield return new WaitForSeconds(0.03f);
            }
        }

    }

    IEnumerator open(RectTransform currentCanva)
    {
        float size = currentCanva.sizeDelta.x;
        
        while(size < 1.0f)
        {
            size += 0.05f;
            currentCanva.localScale = new Vector3(size, size,1);

            yield return new WaitForSeconds(0.01f);
        }

    }
    IEnumerator close(RectTransform currentCanva)
    {
        float size = currentCanva.sizeDelta.x;

        while (size > 0.4f)
        {
            size -= 0.05f;
            currentCanva.localScale = new Vector3(size, size,1);

            yield return new WaitForSeconds(0.01f);
        }
        currentCanva.gameObject.SetActive(false);

    }
}
