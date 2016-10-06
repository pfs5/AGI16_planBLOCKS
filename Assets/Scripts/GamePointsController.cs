using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePointsController : MonoBehaviour {
	public GameManager gameManager;

	// Health points
	public Text hpText;
	public int healthPoints;
	public int rockDamage = 5;
	public int blockDamage = 10;

	// Score points
	public Text scoreText;
	public int scorePoints;

	// Game over text
	public Text gameOverText;

	// Sounds
	public AudioClip [] grunts;

	private AudioSource _audioSource;

	private int _lastIncrease;

	// Use this for initialization
	void Start () {
		gameOverText.text = "Use the shield to block rocks.\nThrow the axes to destroy blocks.\n\nDon't die.\n\nHave fun!";
		gameOverText.enabled = true;
		_audioSource = GetComponent<AudioSource> ();

		healthPoints = 100;
		hpText.text = "HP: " + healthPoints;

		scorePoints = 0;
		scoreText.text = "Score: " + scorePoints;

		_lastIncrease = 0;
	}

	void Update() {
		// Check game over
		if (healthPoints <= 0) {
			gameOverText.text = "GAME OVER";
			gameOverText.enabled = true;

			Time.timeScale = 0;
			gameManager.pause = true;

		}

		// Speed up
		if (scorePoints % 100 == 0 && _lastIncrease != scorePoints) {
			_lastIncrease = scorePoints;

			int newBlockInt = gameManager.blockInterval - 100;
			int newRockInt = gameManager.rockInterval - 100;
			print (gameManager.blockInterval + " " + newRockInt);
			gameManager.blockInterval = Mathf.Max(newBlockInt, 100);
			gameManager.rockInterval = Mathf.Max(newRockInt, 100);
		}

		if (Input.GetKeyDown("space")) {
			Application.LoadLevel (Application.loadedLevel);
		}

		if (Input.GetKeyDown ("p")) {
			gameManager.pause = !gameManager.pause;

			if (gameManager.pause) {
				Time.timeScale = 0;
				gameOverText.text = "PAUSE";
				gameOverText.enabled = true;
			} else {
				Time.timeScale = 2;
				gameOverText.enabled = false;
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.layer == LayerMask.NameToLayer ("Rock")) {
			healthPoints -= rockDamage;
			hpText.text = "HP: " + healthPoints;
			Destroy (collider.gameObject);
			playSound ();
		} else if (collider.gameObject.layer == LayerMask.NameToLayer ("Block")) {
			healthPoints -= blockDamage;
			hpText.text = "HP: " + healthPoints;
			Destroy (collider.gameObject);
			playSound ();
		}
	}

	private void playSound() {
		System.Random r = new System.Random ();
		int i = r.Next (0, grunts.Length);

		_audioSource.clip = grunts [i];
		_audioSource.Play();
	}

}
