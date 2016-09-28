using UnityEngine;
using System.Collections;

public class LineDraw : MonoBehaviour {
	public GameObject start;
	public GameObject end;

	private LineRenderer _line;

	// Use this for initialization
	void Start () {
		_line = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		_line.SetPosition (0, start.transform.position);
		_line.SetPosition (1, end.transform.position);
	}
		

}
