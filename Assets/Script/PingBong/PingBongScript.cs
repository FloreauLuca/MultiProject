using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingBongScript : MonoBehaviour
{
    [SerializeField] private Material mat;


    private Quaternion myOrientation;
    private Quaternion startOrientation;


    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        mat.color = Color.blue;
        startOrientation = transform.rotation;
    }

    public void SetStartPosition()
    {
        transform.parent.rotation = transform.rotation;
        transform.parent.Rotate(Vector3.back, Quaternion.Angle(transform.rotation, startOrientation));
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = GyroToUnity(Input.gyro.attitude);
        Debug.Log("Initial Rotation " + myOrientation);
        Debug.Log("GyrToUnity : " + GyroToUnity(Input.gyro.attitude));
        Debug.Log("Local Rotation : " + transform.localRotation);
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public IEnumerator Touch()
    {
        mat.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        mat.color = Color.blue;
    }

}
