using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rackette rackette;

    private void Update()
    {
        //transform.eulerAngles = Vector3.up * rackette.transform.localEulerAngles.y;
        Debug.Log(rackette.transform.rotation.eulerAngles);
    }
}
