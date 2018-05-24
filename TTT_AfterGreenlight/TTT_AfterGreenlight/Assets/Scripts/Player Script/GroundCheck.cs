using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private Player p;

    // Use this for initialization
    void Start()
    {
        p = gameObject.GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        p.grounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        p.grounded = false;
    }
}
