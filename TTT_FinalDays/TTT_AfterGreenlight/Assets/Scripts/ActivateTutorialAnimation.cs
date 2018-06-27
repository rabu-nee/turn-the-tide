using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTutorialAnimation : MonoBehaviour {

    public Animator AnimationToPlay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null && !AnimationToPlay.enabled)
        {
            AnimationToPlay.enabled = true;
        }
    }
}
