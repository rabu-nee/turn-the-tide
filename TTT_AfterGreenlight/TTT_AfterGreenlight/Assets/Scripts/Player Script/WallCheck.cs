using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{

    private Boy p;

    // Use this for initialization
    void Start()
    {
        p = gameObject.GetComponentInParent<Boy>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wall"))
        {
            p.canWallJump = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        p.canWallJump = false;
    }
}
