using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicManager : MonoBehaviour 
{


    public static MusicManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    [Range(0f, 1f)]
    public float m_musicVolume;
    [Range(0f, 5f)]
    public float m_fxVolume;

    public bool m_musicEnabled;
    public bool m_fxEnabled;

    //public AudioClip m_backgroundMusic;
    public AudioClip m_tapSound;
    public AudioClip m_collectSound;
    public AudioClip m_gameOverSound;
    public AudioClip m_amazing;
    public AudioClip m_incredible;
    public AudioClip m_yeah;

    public AudioSource m_musicSource;

    public AudioClip[] m_musicClips;
    AudioClip m_randomMusicClip;
    

    public AudioClip GetRandomClip(AudioClip[] clips)
    {
        AudioClip randomClip = clips[Random.Range(0, clips.Length)];
        return randomClip;
    }

    void Start () 
	{
        PLayBackgroundMusic(GetRandomClip(m_musicClips));
    }
	
	

	void Update () 
	{
		
	}

    public void PLaySound(AudioClip sound, float volume)
    {
        AudioSource.PlayClipAtPoint(sound, Camera.main.transform.position, volume);
    }


    public void playRandomBkGroundMusic()
    {
        PLayBackgroundMusic(GetRandomClip(m_musicClips));
    }
    public void PLayBackgroundMusic(AudioClip musicClip)
    {
        if(!m_musicEnabled || !musicClip || !m_musicSource)
        {
            return;
        }
        else
        {
            m_musicSource.Stop();

            m_musicSource.clip = musicClip;

            m_musicSource.loop = true;

            m_musicSource.volume = m_musicVolume;

            m_musicSource.Play();
        }
    }


    public void UpdateMusic()
    {
        if(m_musicSource.isPlaying != m_musicEnabled)
        {
            if(m_musicEnabled)
            {
                PLayBackgroundMusic(GetRandomClip(m_musicClips));
            }
            else
            {
                m_musicSource.Stop();
            }
        }
    }

    public void ToggleMusic()
    {
        m_musicEnabled = !m_musicEnabled;
        UpdateMusic();
    }


    void SetInitialReferences()
	{

	}
}
