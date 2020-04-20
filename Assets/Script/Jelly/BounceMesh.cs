using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source : @MajorMcDoom
public class BounceMesh : MonoBehaviour
{
    [SerializeField] Transform controlPoint;
    [SerializeField] Transform skewPivotA;
    [SerializeField] Transform skewPivotB;
    [SerializeField] Transform skewPivotC;
    Vector3 origControlPointRelPos;
    // Start is called before the first frame update
    void Start()
    {
        origControlPointRelPos = controlPoint.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 controlPointRelPos = controlPoint.position;
        Vector3 skewedUp = new Vector3(
            controlPointRelPos.x - origControlPointRelPos.x,
            controlPointRelPos.y,
            controlPointRelPos.z - origControlPointRelPos.z) / origControlPointRelPos.y;
        Debug.Log(skewedUp);
        Vector3 axis = Vector3.Cross(Vector3.up, skewedUp).normalized;
        float theta = Vector3.Angle(Vector3.up, skewedUp);
        float alpha = Vector3.SignedAngle(Vector3.forward, axis, Vector3.up);

        skewPivotA.localRotation = Quaternion.Euler(0, alpha, -(90 - theta) / 2);
        skewPivotB.localRotation = Quaternion.Euler(0, 0, 45);
        skewPivotC.localRotation = Quaternion.Euler(0, -alpha, 0);

        float p = Mathf.Tan(((theta + 90) / 2) * Mathf.Deg2Rad);
        float w = Mathf.Sqrt((1 + p * p) / 2);

        skewPivotA.localScale = new Vector3(p, 1, 1);
        skewPivotB.localScale = new Vector3(1/w, 1*w/p*skewedUp.y, 1);
        StartCoroutine(Move(controlPoint.position));
    }

    IEnumerator Move(Vector3 position)
    {
        yield return new WaitForSeconds(0.1f);
        origControlPointRelPos = position;
    }
}
