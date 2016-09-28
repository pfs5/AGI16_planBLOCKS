using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject head;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = head.transform.position;
	}
}
