using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public static CameraFollow S; // Singleton Instance

    // Fields shown in Unity Inspector pane
    public float easing = 0.05f;

    // Fields set dynamically	
    public GameObject poi; // The Point Of Interest
    private float camZ; // Desired Camera Z Position


    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {

        Vector3 destination;

        // If the point of interest is empty, set it to (0,0,0)
        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Otherwise, get the poi's position
            destination = poi.transform.position;
        }

        // Interpolate between current camera position and poi
        destination = Vector3.Lerp(transform.position, destination, easing);

        // Save the camZ in this destination
        destination.z = camZ;

        // Set camera to this destination
        transform.position = destination;
    }
}
