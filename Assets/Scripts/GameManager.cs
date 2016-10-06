using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public AudioClip rockThrowClip;

	// Points
	public GamePointsController gamePointsController;

	// Blocks reference
	public GameObject axe;
	public GameObject [] block;
	public GameObject [] rock;
	public GameObject player;

	// Axe objects
	public GameObject hand;
	public GameObject elbow;
	public GameObject hip;

	public float blockSpeed;

	public int rockInterval = 100;
	public int blockInterval = 100;
	public int axeSpawnInterval = 10;

	public bool pause;

	private int tick;
	private System.Random _rand;
	private AxeController _axeController;

	// Use this for initialization
	void Start () {
		tick = 0;
		_rand = new System.Random ();
		gamePointsController.gameManager = this;

		// Set pause
		pause = true;
	}
	
	// Update is called once per frame
	void Update () {
		// Pausing
		if (pause) {
			return;
		}

		// Check if game over
		tick++;

		// Block spawning
		if (tick % blockInterval == 0) {
			int index = _rand.Next (0, block.Length);
			GameObject newBlock = Instantiate (block[index]);

			// Direction
			Vector3 dir = newBlock.transform.position - player.transform.position;

			newBlock.GetComponent<Rigidbody> ().velocity = blockSpeed*dir.normalized*-1;
			newBlock.transform.rotation = Quaternion.LookRotation (dir);

			newBlock.GetComponent<BlockController> ().gamePointsController = gamePointsController;
		}

		// Rock spawning
		if (tick % rockInterval == 0) {
			int index = _rand.Next (0, rock.Length);
			GameObject newRock = Instantiate (rock [index]);
			Destroy (newRock.gameObject, 15);

			// Play sound
			AudioSource.PlayClipAtPoint(rockThrowClip, newRock.transform.position, 1.5f);

			// Direction and speed
			Vector3 dir = -(newRock.transform.position - player.transform.position).normalized;
			RockController rockController = newRock.GetComponent<RockController> ();
			newRock.GetComponent<Rigidbody> ().velocity = new Vector3 (rockController.speed*dir.x, rockController.angle, rockController.speed*dir.z);
		}

		// Axe spawning
		if (_axeController == null) {
			spawnAxe ();
		} else if (tick % axeSpawnInterval == 0) {
			if (_axeController.thrown) {
				spawnAxe ();
			}
		}

		if (tick % (rockInterval * blockInterval * axeSpawnInterval) == 0) {
			tick = 0;
		}
	}

	private void spawnAxe() {
		_axeController = Instantiate (axe).GetComponent<AxeController> ();
		_axeController.holdingHand = hand;
		_axeController.elbow = elbow;
		_axeController.hip = hip;
	}

}
