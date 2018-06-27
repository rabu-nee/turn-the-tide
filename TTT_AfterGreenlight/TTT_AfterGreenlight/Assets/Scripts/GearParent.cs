using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearParent : MonoBehaviour {
	
	public float rotationMultiplier = 2f;
	public float rotationFriction = 1f;
	public float velocityAmount = 40f;
	public float startVelocity = 0f;
	public bool continuous = false;
    public string whiteGearSound, blackGearSound;
    private bool hasPlayed;

	private float velocity = 0f;
	private float desiredVelocity = 0f;
	private GearScript childGS;
	private LevelRotation lr;
	private int lastScreen;

	//BUILT-IN FUNCTIONS===================================================================================================================
	void Start () {
		childGS = transform.GetChild (0).gameObject.GetComponent<GearScript> ();
		lr = GameObject.FindGameObjectWithTag ("CurrentLevel").GetComponent<LevelRotation> ();
		lastScreen = lr.getCurScreen ();
		velocity = startVelocity;
		if (!continuous) {
			desiredVelocity = 0;
		} else {
			desiredVelocity = startVelocity * lr.getCurScreen();
            SoundManager.instance.PlaySound(whiteGearSound);
        }
	}

	void Update () {
		//Apply rotation based on Turning
		if (lr.getCurScreen () != lastScreen) {
			lastScreen = lr.getCurScreen ();
			if (!continuous) {
				velocity += (velocityAmount * lr.getLastDir ());
				desiredVelocity = 0;

                SoundManager.instance.PlaySound(blackGearSound);
			}
			if (continuous) {
				desiredVelocity = startVelocity * lr.getLastDir();

                SoundManager.instance.StopSound(whiteGearSound);
                SoundManager.instance.PlaySoundDelayed(whiteGearSound, 0.3f);
                
            }
		}

		//Apply velocities
		velocity = Mathf.Lerp (velocity, desiredVelocity, Time.deltaTime * rotationFriction);



		//Rotate Gears based on velocity
		childGS.rotate (Time.deltaTime * (velocity * rotationMultiplier));
	}
}
