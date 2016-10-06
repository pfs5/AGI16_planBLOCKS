using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
	public AudioClip [] shieldHitClips;

	public GameObject position1;
	public GameObject position2;
	public GameObject reference;

	public float turnAngle = 20f;
	private Vector3 _initialRotation;
	// Use this for initialization
	void Start () {
		_initialRotation = transform.eulerAngles;
	}

	void OnTriggerEnter(Collider collider) {

		if (collider.gameObject.layer == LayerMask.NameToLayer ("Rock")) {
			// Push the rock with the shield
			JointPosition pos1 = position1.GetComponent<JointPosition> ();
			JointPosition pos2 = position2.GetComponent<JointPosition> ();

			Vector3 speed = (pos1.speed [pos1.index] + pos2.speed [pos2.index]) / 2;

			collider.gameObject.GetComponent<Rigidbody> ().velocity = speed;
		
			// Destroy object after some time
			Destroy(collider.gameObject, 2);

			// Play sound
			System.Random r = new System.Random();
			int index = r.Next (0, shieldHitClips.Length);
			AudioSource.PlayClipAtPoint(shieldHitClips[index], collider.transform.position, 1f);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.position = (position1.transform.position + position2.transform.position)/2;

		// Rotate if changes sides
		if (transform.position.z > (reference.transform.position.z+0.5f)) {
			transform.eulerAngles = _initialRotation + new Vector3 (0f, 0f, turnAngle);
		} else {
			transform.eulerAngles = _initialRotation;
		}
	}
}
