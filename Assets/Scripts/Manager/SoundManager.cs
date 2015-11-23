using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    static SoundManager instance;
    public static SoundManager Instance
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

	private int currentSound = 0;

	public GameObject currentZik;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
		if(PlayerPrefs.GetInt ("sound")!= null)
			currentSound = PlayerPrefs.GetInt ("sound");
		audioSource = GetComponent<AudioSource> ();
	}

	public void playSound(AudioClip audio, float volume)
	{
		GameObject currentMusique = (GameObject)Instantiate (currentZik, Vector3.zero, Quaternion.identity);
		AudioSource needZik =  currentMusique.AddComponent<AudioSource>();
		needZik.clip = audio;
		needZik.Play ();
		if(currentSound == 0)
			needZik.volume = volume;
		else
			needZik.volume = 0;
	}

}
