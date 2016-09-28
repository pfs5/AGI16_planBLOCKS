using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Blocks reference
	public GameObject [] block;
	public GameObject [] rock;
	public GameObject player;

	public float blockSpeed;

	public int rockInterval = 100;
	public int blockInterval = 100;

	private int tick;
	private System.Random _rand;

	// Use this for initialization
	void Start () {
		tick = 0;
		_rand = new System.Random ();
	}
	
	// Update is called once per frame
	void Update () {
		tick++;

		if (tick % blockInterval == 0) {
			int index = _rand.Next (0, block.Length);
			GameObject newBlock = Instantiate (block[index]);

			// Direction
			Vector3 dir = newBlock.transform.position - player.transform.position;

			newBlock.GetComponent<Rigidbody> ().velocity = blockSpeed*dir.normalized*-1;
			newBlock.transform.rotation = Quaternion.LookRotation (dir);
		}

		if (tick % rockInterval == 0) {
			int index = _rand.Next (0, rock.Length);
			GameObject newRock = Instantiate (rock [index]);

			// Direction and speed
			Vector3 dir = -(newRock.transform.position - player.transform.position).normalized;
			RockController rockController = newRock.GetComponent<RockController> ();
			newRock.GetComponent<Rigidbody> ().velocity = new Vector3 (rockController.speed*dir.x, rockController.angle, rockController.speed*dir.z);
		}
	}
}
