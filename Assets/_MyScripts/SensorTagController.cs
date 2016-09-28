using UnityEngine;
using System.Collections;
using System;

public class SensorTagController : MonoBehaviour {

	public string url = "http://localhost:8080";

	public float accelerometerTreshold;
	public bool isThrown;

	public Vector3 acc;

	public float magnetometerTreshold;
	private bool resetBallPosition;

	public int keyCode;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		isThrown = false;
		resetBallPosition = false;
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {
		WWW www = new WWW (url);
		StartCoroutine(WaitForRequest(www, false));
	}

	IEnumerator WaitForRequest(WWW www, bool init){
		yield return www;

		// check for errors
		if (www.error == null) {
			string data = www.text;
			evaluateSensorData (data);
		} else {
			// TODO
		}
	}

	void evaluateSensorData(string data){
		string[] parts = data.Split (new []{ '#' });

		string sensor = parts [1];

		if (sensor.Equals ("keypress")) {
			int buttonCode = int.Parse (parts [2]);
			evaluateKeypressData (buttonCode);
			return;
		}

		float x = float.Parse (parts [2]);
		float y = float.Parse (parts [3]);
		float z = float.Parse (parts [4]);

		switch (sensor) {
		case "accelerometer":
			evaluateAccelerometerData (x, y, z);
			break;
		case "gyroscope":
			evaluateGyroscopeData (x, y, z);
			break;
		case "magnetometer":
			evaluateMagnetometerData (x, y, z);
			break;
		default:
			print ("Invalid sensor parameter: " + sensor);
			break;
		}
			
	}

	/*
	 * Button code 1 -> right button
	 * Button code 2 -> left button
	 * Button code 3 -> both buttons
	*/
	void evaluateKeypressData(int buttonCode){

		keyCode = buttonCode;
//		switch(buttonCode){
//		case 1:
//			resetBall ();
//			break;
//		case 2:
//			throwTheBall ();
//			break;
//		default:
//			break;
//		}

	}

	void evaluateAccelerometerData(float x, float y, float z){
		// y += 0.0625f;

		float vx = x * Time.deltaTime;
		float vy = y * Time.deltaTime;
		float vz = z * Time.deltaTime;

		acc = new Vector3 (x, y, z);


		if (isBelowTreshold (x) && isBelowTreshold (y) && isBelowTreshold (z)) {
			isThrown = true;
//			throwTheBall ();
		} else {
//			isThrown = false;
		}

	}

	void evaluateGyroscopeData(float x, float y, float z){
		// float factor = 65536f / 500;

		transform.Rotate (new Vector3 (x, y, z) * Time.deltaTime);
	}

	void evaluateMagnetometerData(float x, float y, float z){
		Vector3 magnetVector = new Vector3 (x, y, z);

		if (resetBallPosition && magnetVector.sqrMagnitude > magnetometerTreshold) {
//			resetBall ();
		}
	}

	bool isBelowTreshold(float value){
		return (Math.Abs (value) < accelerometerTreshold);
	}

//	void throwTheBall(){
//		if (!isThrown) {
//			rb.velocity = new Vector3 (30f, 30f, 30f);
//			isThrown = true;
//			resetBallPosition = true;
//		}
//	}
//
//	void resetBall(){
//		transform.position = new Vector3 (0, 6, 0);
//		rb.velocity = Vector3.zero;
//		resetBallPosition = false;
//		isThrown = false;
//	}
		
}
