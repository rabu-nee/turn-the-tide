using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuLoadScene : MonoBehaviour
{

    public float timeTilLoad = 3f;

    private Animator anim;
    private int sceneToLoad = 0;
    private bool transitionNow = false;
    private float elapsedTime = 0;

    public void loadSlotScene(int index)
    {
        if (index < 1)
        {
            sceneToLoad = index + 1;
        }
        else
        {
            sceneToLoad = index + 2;
        }
        anim.SetBool("GameStart", true);
        transitionNow = true;

        //Play flip sound
        SoundManager.instance.PlaySound("level flip");
        SoundManager.instance.FadeSound("ambience bird", 0.295f);
    }

    public bool startedTransition()
    {
        return transitionNow;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (transitionNow)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeTilLoad)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }

        //MENU DEBUG
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
