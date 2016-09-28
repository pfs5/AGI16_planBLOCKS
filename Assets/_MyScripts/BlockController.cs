using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization
	void Start () {
	}

	void OnCollisionEnter(Collision collision) {
		// If collided with ball, hit ball
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Ball")) {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		Vector3 pos = transform.position;
		pos.z += speed*Time.deltaTime;
		transform.position = pos;
	}
}
