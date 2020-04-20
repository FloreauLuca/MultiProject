using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dini Dev - Jelly Mesh Deformation / Soft Body Physics in Unity    :    https://www.youtube.com/watch?v=UxLJ6XewTVs&t=222s
public class JellyVertex
{
    private int verticeIndex;
    private Vector3 initialVertexPosition;
    private Vector3 currentVertexPosition;
    public Vector3 CurrentVertexPosition
    {
        get => currentVertexPosition;
        set => currentVertexPosition = value;
    }

    private Vector3 currentVelocity;
    public Vector3 CurrentVelocity
    {
        get => currentVelocity;
        set => currentVelocity = value;
    }

    public JellyVertex(int newVerticeIndex, Vector3 newInitialVertexPosition, Vector3 newCurrentVertexPosition, Vector3 newCurrentVelocity)
    {
        verticeIndex = newVerticeIndex;
        initialVertexPosition = newInitialVertexPosition;
        currentVertexPosition = newCurrentVertexPosition;
        currentVelocity = newCurrentVelocity;
    }

    public Vector3 GetCurrentDisplacement()
    {
        return currentVertexPosition - initialVertexPosition;
    }

    public void UpdateVelocity(float bounceSpeed)
    {
        currentVelocity = currentVelocity - GetCurrentDisplacement() * bounceSpeed * Time.deltaTime;
    }

    public void Settle(float siffness)
    {
        currentVelocity *= 1f - siffness * Time.deltaTime;
    }

    public void ApplyPressureToVertex(Transform transform, Vector3 position, float pressure)
    {
        Vector3 distanceVerticePoint = currentVertexPosition - transform.InverseTransformPoint(position);
        float adaptedPressure = pressure / (1f + distanceVerticePoint.sqrMagnitude);
        float newVelocity = adaptedPressure * Time.deltaTime;
        currentVelocity += distanceVerticePoint.normalized * newVelocity;
    }

}
