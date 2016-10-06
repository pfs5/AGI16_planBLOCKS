using UnityEngine;
using System.Collections;


public class BlockController : MonoBehaviour {
	public AudioClip explosionClip;
	public GameObject explosion;
	public GamePointsController gamePointsController;

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.layer == LayerMask.NameToLayer ("Axe")) {
			GameObject expl = Instantiate (explosion);
			expl.transform.SetParent (transform);
			expl.transform.localPosition = Vector3.zero;

			gamePointsController.scorePoints += 10;
			gamePointsController.scoreText.text = "Score: " + gamePointsController.scorePoints;

			// Play sound
			AudioSource.PlayClipAtPoint(explosionClip, transform.position, 1f);
		}

		Destroy (gameObject, 1.5f);
	}
}
