using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class JointPosition : MonoBehaviour 
{
    public Windows.Kinect.JointType _jointType;
    public GameObject _bodySourceManager;
	public int step = 5;
	public float maxSpeed = 60f;

	public bool moveActive;
    
	private BodySourceManager _bodyManager;
	private Rigidbody _rigidbody;

	private Vector3 lastPos;
	private int tick;
	private float time;

	public Vector3 [] speed;
	public int index;
	public int maxIndex = 3;

	// Use this for initialization
	void Start () 
    {
		_rigidbody = GetComponent<Rigidbody> ();
		lastPos = transform.position;
		tick = 0;
		time = 0;

		speed = new Vector3[maxIndex]; 
		index = 0;

		moveActive = true;
	}
		

	// Update is called once per frame
	void Update () 
    {
		//tick++;
		//time += Time.deltaTime;

        if (_bodySourceManager == null)
        {
            return;
        }

        _bodyManager = _bodySourceManager.GetComponent<BodySourceManager>();
        if (_bodyManager == null)
        {
            return;
        }

        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return;
        }

        // get the first tracked body...
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {

               	var pos = body.Joints[_jointType].Position;
				float k = 10f;


				var headPos = body.Joints [JointType.Head].Position;

				float x = (-pos.X + -1*headPos.X * 2)*k;
				float y = pos.Y * k;
				float z = pos.Z * k;



				// Update speed
				//calculateSpeed();

				if (moveActive) {
					this.gameObject.transform.localPosition= new Vector3 (x, y, z);
				}

				//calculateSpeed ();


				break;
            }
        }
	}

	void calculateSpeed() {
		if (tick % step == 0) {
			Vector3 distance = transform.position - lastPos;
			Vector3 newSpeed = (distance / time)*5;

			print (newSpeed.magnitude);

			if (newSpeed.magnitude < maxSpeed) {
				speed [index] = newSpeed;
				index = (index + 1) % maxIndex;
				lastPos = gameObject.transform.position;
			}

			time = 0;
			tick = 0;
		}

//		if (tick % step == 0) {
//			Vector3 distance = (gameObject.transform.position - lastPos)*10;
//			Vector3 newSpeed = distance / time;
//
//			// Check top speed
//			print (newSpeed.magnitude);
//			if (newSpeed.magnitude < maxSpeed) {
//				speed[index] = newSpeed;
//				index = (index + 1) % maxIndex;
//				lastPos = gameObject.transform.position;
//			}
//
//			time = 0;
//			tick = 0;
//		}
	}
}
