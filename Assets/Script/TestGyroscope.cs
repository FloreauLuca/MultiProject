using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestGyroscope : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private List<Vector3> orientationList;
    private List<Vector3> accelerationList;

    [SerializeField] private GameObject racket;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        Input.gyro.enabled = true;
        accelerationList = new List<Vector3>();
        orientationList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0) && orientationList.Count < 500)
        {
            textMesh.text = textMesh.text + Input.acceleration + Input.gyro.attitude + Input.gyro.attitude.eulerAngles + Input.gyro.userAcceleration + "\n";
            Debug.Log("" + Input.acceleration + Input.gyro.attitude + Input.gyro.userAcceleration);
            orientationList.Add(Input.acceleration);
            accelerationList.Add(Input.gyro.userAcceleration);
        }

        if (Input.GetMouseButtonDown(0))
        {
            textMesh.text = textMesh.text + "====================================\n";
        }
    }
    
    void Show()
    {
        StartCoroutine(Showing());
    }

    IEnumerator Showing()
    {
        foreach (Vector3 acceleration in accelerationList)
        {
            yield return null;
        }
    }
}
