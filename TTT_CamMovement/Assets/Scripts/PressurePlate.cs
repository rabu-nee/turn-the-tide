using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obstacle;
        obstacle = GameObject.FindGameObjectWithTag("Obstacle");
        Destroy(obstacle);
    }
}
