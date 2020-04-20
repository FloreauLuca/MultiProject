using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
struct Foot
{

    public Vector3 localStandardPos;
    public Vector3 worldPosition;
    [SerializeField] public Transform transform;

    public void Start()
    {
        localStandardPos = transform.localPosition;
        worldPosition = transform.position;
    }

    public void Update()
    {
        transform.position = worldPosition;
    }
}

//[ExecuteInEditMode]
public class FeetScript : MonoBehaviour
{
    [SerializeField] private Foot rightFoot;
    [SerializeField] private Foot leftFoot;

    private Rigidbody rigidbody;
    [SerializeField] private float velocityPredict = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        rightFoot.Start();
        leftFoot.Start();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rightFoot.Update();
        leftFoot.Update();
        float rightFootPos = rightFoot.transform.localPosition.z;
        float leftFootPos = leftFoot.transform.localPosition.z;
        if (rightFootPos > rightFoot.localStandardPos.z && leftFootPos > leftFoot.localStandardPos.z)
        {
            if (rightFootPos > leftFootPos)
            {
                rightFoot.worldPosition = rightFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
            else
            {
                leftFoot.worldPosition = leftFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
        } else if (rightFootPos < rightFoot.localStandardPos.z && leftFootPos < leftFoot.localStandardPos.z)
        {
            if (rightFootPos < leftFootPos)
            {
                rightFoot.worldPosition = rightFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
            else
            {
                leftFoot.worldPosition = leftFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
        }
        rightFootPos = rightFoot.transform.localPosition.x;
        leftFootPos = leftFoot.transform.localPosition.x;
        if (rightFootPos > rightFoot.localStandardPos.x && leftFootPos > leftFoot.localStandardPos.x)
        {
            if (rightFootPos > leftFootPos)
            {
                rightFoot.worldPosition = rightFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
            else
            {
                leftFoot.worldPosition = leftFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
        }
        else if (rightFootPos < rightFoot.localStandardPos.x && leftFootPos < leftFoot.localStandardPos.x)
        {
            if (rightFootPos < leftFootPos)
            {
                rightFoot.worldPosition = rightFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
            else
            {
                leftFoot.worldPosition = leftFoot.localStandardPos + transform.position + rigidbody.velocity * velocityPredict;
            }
        }

    }
}
