using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour 
{
    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Body[] _Data = null;
    
    public Body[] GetData()
    {

		return _Data;
//		// ADDED CODE
//		if (_Data == null || _Data.Length < 2) {
//			return _Data;
//		}
//
//		// Find body on center
//		float minX = Mathf.Abs(_Data[0].Joints[JointType.SpineMid].Position.X);
//		Body[] newData = new Body[1];
//		newData [0] = _Data [0];
//
//		for (int i = 1; i < _Data.Length; i++) {
//			float x = Mathf.Abs (_Data [i].Joints [JointType.SpineMid].Position.X);
//			if (x < minX) {
//				newData [0] = _Data [i];
//				minX = x;
//			}
//		}
//
//		return newData;
    }
    

    void Start () 
    {
        _Sensor = KinectSensor.GetDefault();

        if (_Sensor != null)
        {
            _Reader = _Sensor.BodyFrameSource.OpenReader();
            
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }   
    }
    
    void Update () 
    {
        if (_Reader != null)
        {
            var frame = _Reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                }
                
                frame.GetAndRefreshBodyData(_Data);
                
                frame.Dispose();
                frame = null;
            }
        }    
    }
    
    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }
        
        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }
            
            _Sensor = null;
        }
    }
}
