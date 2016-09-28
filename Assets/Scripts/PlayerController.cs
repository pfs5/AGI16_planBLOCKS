using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float rotationSpeed = 2f;
	public float translationSpeed = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Rotate from mouse input
		float h = rotationSpeed * Input.GetAxis("Mouse X");
		float v = rotationSpeed * Input.GetAxis("Mouse Y");
		transform.eulerAngles = transform.eulerAngles + new Vector3 (-v, h, 0);

		// Move with arrow keys
		float translationV = Input.GetAxis("Vertical") * translationSpeed;
		float translationH = Input.GetAxis("Horizontal") * translationSpeed;
		transform.Translate (translationH, 0, translationV);
	}
}
