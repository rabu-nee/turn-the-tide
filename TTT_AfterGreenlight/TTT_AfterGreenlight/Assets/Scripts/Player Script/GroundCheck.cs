using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Player p;

    private void Start()
    {
        p = this.transform.parent.gameObject.GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        p.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        p.grounded = false;
    }
}
