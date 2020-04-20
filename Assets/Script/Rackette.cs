using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rackette : MonoBehaviour
{
    [SerializeField] private Material mat;
    private float initialPos;
    private List<Quaternion> orientationList;
    private List<float> accelerationList;
    private float guiacc;
    private Quaternion guiorien;
    

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            initialPos = Input.gyro.attitude.eulerAngles.x;
            orientationList = new List<Quaternion>();
            accelerationList = new List<float>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(Replay());
        }
        */
        mat.color = Color.Lerp(Color.green, Color.red, Input.gyro.userAcceleration.y / 5);
        transform.localRotation = GyroToUnity(Input.gyro.attitude);

    }

    IEnumerator Replay()
    {
        while (!Input.GetMouseButton(0))
        {
        for(int i = 0; i < orientationList.Count; i++)
        {
            mat.color = Color.Lerp(Color.green, Color.red, accelerationList[i] / 5);
            transform.rotation = GyroToUnity(Input.gyro.attitude);
            guiacc = accelerationList[i];
            guiorien = orientationList[i];
            yield return null;
            }
        }
    }
    
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("input.gyro.attitude.euler: " + Input.gyro.attitude.eulerAngles);
        GUILayout.Label("input.gyro.acceleration: " + Input.gyro.userAcceleration);
        GUILayout.Label("input.acceleration: " + guiacc);
        GUILayout.Label("input.acceleration: " + guiorien);
        GUILayout.Label(" ");
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }

}
