using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public bool loop;

    public bool bgm;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.loop = loop;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void PlayDelayed(float delay)
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.PlayDelayed(delay);
    }
}


public class SoundManager : MonoBehaviour
{


    public static SoundManager instance;

    [SerializeField]
    Sound[] sounds;

    private bool keepFadingIn, keepFadingOut;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                //Debug.Log("Playing " + sounds[i].name);
                return;
            }
        }
        //no sounds with _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

    public void PlaySoundDelayed(string _name, float delayTime)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].PlayDelayed(delayTime);
                return;
            }
        }

        //no sounds with _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        //no sounds with _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

    public void FadeSound(string _name, float speed)
    {
        StartCoroutine(FadeOut(_name, speed));
    }

    public IEnumerator FadeOut(string _name, float FadeTime)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                float startVolume = sounds[i].volume;

                while (sounds[i].volume > 0)
                {
                    sounds[i].volume -= startVolume * Time.deltaTime / FadeTime;

                    yield return null;
                }

                sounds[i].Stop();
                sounds[i].volume = startVolume;
            }
        }
    }
}
