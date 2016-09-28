﻿using UnityEngine;
using System.Collections;

public class AxeController : MonoBehaviour {

	// Throwing hand object
	public GameObject holdingHand;
	public GameObject elbow;
	public GameObject hip;

	// Parameters
	public float speedK = 2f;
	public float tresh = 10f;
	public float angleTresh = 60f;


	// References
	private JointPosition _handScript;
	private JointPosition _hipScript;
	private Rigidbody _rigidBody;
	private Collider _collider;

	// Throwing speed
	private Vector3 throwSpeed;

	// Flags
	public bool thrown;

	// Use this for initialization
	void Start () {
		init ();
	}

	private void init() {
		thrown = false;

		// Get references
		_handScript = holdingHand.GetComponent<JointPosition> ();
		_hipScript = hip.GetComponent<JointPosition> ();
		_rigidBody = GetComponent<Rigidbody> ();
		_collider = GetComponent<Collider> ();

		// Disable collider
		_collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Move axe with hand
		if (!thrown) {
			transform.position = holdingHand.transform.position;
		}

		if (checkThrow()) {
			// Calculate speed
			_rigidBody.velocity = throwSpeed * speedK;

			_rigidBody.useGravity = true;		//TODO may be removed

			// Enable collider
			_collider.enabled = true;

			// Throw
			thrown = true;
		}
	}

	private bool checkThrow() {
		// Check if ball already thrown
		if (thrown) {
			return false;
		}

		// Get hand speed
		Vector3[] handSpeed = _handScript.speed;
		Vector3[] hipSpeed = _hipScript.speed;

		int handIndex = _handScript.index;
		int handMax = _handScript.maxIndex;
		int hipIndex = _hipScript.index;
		int hipMax = _hipScript.maxIndex;


		int newHandIndex = (handIndex + 1) % handMax;
		int newHipIndex = (hipIndex + 1) % hipMax;

		Vector3 throwingSpeed = (handSpeed [newHandIndex] - handSpeed [handIndex]) - (hipSpeed [newHipIndex] - hipSpeed [hipIndex]);
		//print ("throwing speed = " + throwingSpeed.magnitude);

		// Check if speed droped
		//print (throwingSpeed.magnitude);
		if (throwingSpeed.magnitude > tresh) { 
			//throwSpeed = handSpeed [newHandIndex] - hipSpeed [newHandIndex];

			// Get max speed
			int maxMagnitudeIndex = getMaxSpeedIndex(handSpeed, hipSpeed, handMax);

			Vector3 potThrowSpeed = handSpeed [maxMagnitudeIndex] - hipSpeed [maxMagnitudeIndex];

			// Check if throw is behind player
//			if (potThrowSpeed.z > 0) {
//				return false;
//			}

			// Check angle in xz plane
//			float angle = Mathf.Abs(potThrowSpeed.z) / Mathf.Sqrt(Mathf.Pow(potThrowSpeed.x,2) + Mathf.Pow(potThrowSpeed.z,2));
//			if (angle < Mathf.Cos(angleTresh*180f/Mathf.PI) ){
//				return false;
//			}

			throwSpeed = potThrowSpeed;

			return true;
		}

		return false;
	}

	int getMaxSpeedIndex(Vector3 [] handSpeed, Vector3 [] hipSpeed, int maxIndex) {
		float maxMagnitude = (handSpeed[0] - hipSpeed[0]).magnitude;
		int maxMagnitudeIndex = 0;
		for (int i = 1; i < maxIndex; i++) {
			float m = (handSpeed [i] - hipSpeed [i]).magnitude;
			if (m > maxMagnitude) {
				maxMagnitude = m;
				maxMagnitudeIndex = i;
			}
		}
		return maxMagnitudeIndex;
	}
}
