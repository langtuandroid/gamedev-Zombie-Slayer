using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

[RequireComponent(typeof(EdgeCollider2D))]
public class BezierCollider2DZS : MonoBehaviour
{
    [FormerlySerializedAs("firstPoint")] public Vector2 firstPointT;
    [FormerlySerializedAs("secondPoint")] public Vector2 secondPointT;

    [FormerlySerializedAs("handlerFirstPoint")] public Vector2 handlerFirstPointT;
    [FormerlySerializedAs("handlerSecondPoint")] public Vector2 handlerSecondPointT;

    [FormerlySerializedAs("pointsQuantity")] public int pointsQuantityY;

    private Vector3 CalculateBezierPointT(float t, Vector3 p0, Vector3 handlerP0, Vector3 handlerP1, Vector3 p1)
    {
        float u = 1.0f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; //first term
        p += 3f * uu * t * handlerP0; //second term
        p += 3f * u * tt * handlerP1; //third term
        p += ttt * p1; //fourth term

        return p;
    }

    public Vector2[] Calculate2DPoints()
    {
        List<Vector2> points = new List<Vector2> { firstPointT };

        for (int i = 1; i < pointsQuantityY; i++)
        {
            points.Add(CalculateBezierPointT((1f / pointsQuantityY) * i, firstPointT, handlerFirstPointT, handlerSecondPointT, secondPointT));
        }
        points.Add(secondPointT);

        return points.ToArray();
    }

}