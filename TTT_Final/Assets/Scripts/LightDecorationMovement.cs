using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDecorationMovement : MonoBehaviour {

	private Light l;
	private float standardIntensity;
	private float standardRange;
	private Vector3 standardPosition;
	private float elapsedTime = 0;

	private float desiredIntensity;
	private float desiredRange;
	private Vector3 desiredPosition;

	public float intensityDeviation = 1f;
	public float rangeDeviation = 8f;
	public float positionDeviation = 0.3f;
	public float deviationSpeed = 0.2f;
	public float deviationTime = 7f;

	void Start () {
		l = GetComponent<Light> ();
		standardIntensity = l.intensity;
		standardRange = l.range;
		standardPosition = transform.localPosition;

		setDesiredValues ();
	}

	void Update () {
		elapsedTime += Time.deltaTime;

		l.intensity = Mathf.Lerp (l.intensity, desiredIntensity, deviationSpeed * Time.deltaTime);
		l.range = Mathf.Lerp (l.range, desiredRange, deviationSpeed * Time.deltaTime);
		transform.localPosition = Vector3.Lerp (transform.localPosition, desiredPosition, deviationSpeed * Time.deltaTime);

		if (elapsedTime >= deviationTime) {
			setDesiredValues ();
			elapsedTime = 0;
		}
	}

	void setDesiredValues() {
		desiredIntensity = standardIntensity + (Random.Range(-intensityDeviation, intensityDeviation));
		desiredRange = standardRange + (Random.Range (-rangeDeviation, rangeDeviation));
		desiredPosition = new Vector3 (standardPosition.x + (Random.Range (-positionDeviation, positionDeviation)), standardPosition.y + (Random.Range (-positionDeviation, positionDeviation)), standardPosition.z);
	}
}
