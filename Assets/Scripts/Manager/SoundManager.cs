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
    
    public AudioClip boost;
    public AudioClip fireBall;
    public AudioClip castleDamage;
    public AudioClip explosion;
    public AudioClip heal;


    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
        DontDestroyOnLoad(this);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

	public void playSound(AudioClip audio, float volume, GameObject go)
	{
        AudioSource needZik;
        if (!go.GetComponent<AudioSource>())
        {
            needZik = go.AddComponent<AudioSource>();
        }
        else
        {
            needZik = go.GetComponent<AudioSource>();
        }
		needZik.clip = audio;
		needZik.Play ();
	    needZik.volume = volume;
	}

}
