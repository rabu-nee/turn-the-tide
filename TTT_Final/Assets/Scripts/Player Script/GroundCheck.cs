using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Player p;
    public string LandingGroundSound, WallContactSound;

    private void Start()
    {
        p = this.transform.parent.gameObject.GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
		if (!p.grounded) {
			p.spawnDustCloud = true;
		}
        p.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p.grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            SoundManager.instance.PlaySound(WallContactSound);
			p.isHuggingWall = true;
			p.spawnDustCloud = true;
        }
        else
        {
            SoundManager.instance.PlaySound(LandingGroundSound);
			p.isHuggingWall = false;
        }
    }
}
