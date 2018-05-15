using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraFollowPlayer : MonoBehaviour {

	public bool FollowX;
	public bool FollowY;
	public GameObject[] TargetObjs;
	public float speed = 1;
	public float overcompensation = 1f;
	public Vector2 minPos;
	public Vector2 maxPos;

	void Update () {
		float nx = this.transform.position.x;
		float ny = this.transform.position.y;
		float nz = this.transform.position.z;

		if (FollowX) {
			nx = getAvrgPos().x;
		}
		if (FollowY) {
			ny = getAvrgPos().y;
		}

		//nx = Mathf.Clamp (nx, minPos.x, maxPos.x);
		//ny = Mathf.Clamp (ny, minPos.y, maxPos.y);

		Vector3 newPos = new Vector3 (nx, ny, nz);

		this.transform.position = Vector3.Lerp (this.transform.position, newPos, Time.deltaTime * speed);
	}

	private Vector2 getAvrgPos(){
		Vector2 retVec = Vector2.zero;
		for (int i = 0; i < TargetObjs.Length; i++) {
			retVec.x += TargetObjs [i].transform.position.x;
			retVec.y += TargetObjs [i].transform.position.y;
		}
		retVec = (retVec / TargetObjs.Length) * overcompensation;

		return retVec;
	}
}
