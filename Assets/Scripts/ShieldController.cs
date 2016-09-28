using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	public GameObject position1;
	public GameObject position2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = (position1.transform.position + position2.transform.position)/2;
	}
}
