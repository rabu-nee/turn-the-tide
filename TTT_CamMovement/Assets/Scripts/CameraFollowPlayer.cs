using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraFollowPlayer : MonoBehaviour {

	public bool FollowX;
	public bool FollowY;
	public GameObject TargetObj;
	public float speed = 1;
	public Vector2 minPos;
	public Vector2 maxPos;

	void FixedUpdate () {
		float nx = this.transform.position.x;
		float ny = this.transform.position.y;
		float nz = this.transform.position.z;

		if (FollowX) {
			nx = TargetObj.transform.position.x;
		}
		if (FollowY) {
			ny = TargetObj.transform.position.y;
		}

		nx = Mathf.Clamp (nx, minPos.x, maxPos.x);
		ny = Mathf.Clamp (ny, minPos.y, maxPos.y);

		Vector3 newPos = new Vector3 (nx, ny, nz);

		this.transform.position = Vector3.Lerp (this.transform.position, newPos, Time.deltaTime * speed);
	}
}
