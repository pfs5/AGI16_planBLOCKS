using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BoardController : MonoBehaviour {

	public JointPosition hand;

	public float velocityK = 5f;

	// Use this for initialization
	void Start () {

	}

	void OnTriggerEnter(Collider other) {
		// If collided with ball, hit ball
		if (other.gameObject.layer == LayerMask.NameToLayer ("Ball")) {
			print ("hit");
			other.GetComponent<Rigidbody> ().velocity = (velocityK) * (hand.speed[hand.index] + -other.gameObject.GetComponent<Rigidbody>().velocity) ;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
