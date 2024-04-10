using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCollider2DZS))]
public class BezierCollider2DEditor : Editor
{
    BezierCollider2DZS bezierCollider;
    EdgeCollider2D edgeCollider;

    int lastPointsQuantity = 0;
    Vector2 lastFirstPoint = Vector2.zero;
    Vector2 lastHandlerFirstPoint = Vector2.zero;
    Vector2 lastSecondPoint = Vector2.zero;
    Vector2 lastHandlerSecondPoint = Vector2.zero;

    public override void OnInspectorGUI()
    {
        bezierCollider = (BezierCollider2DZS)target;

        edgeCollider = bezierCollider.GetComponent<EdgeCollider2D>();

        if (edgeCollider.hideFlags != HideFlags.HideInInspector)
        {
            edgeCollider.hideFlags = HideFlags.HideInInspector;
        }

        if (edgeCollider != null)
        {
            bezierCollider.pointsQuantityY = EditorGUILayout.IntField("curve points", bezierCollider.pointsQuantityY, GUILayout.MinWidth(100));
            bezierCollider.firstPointT = EditorGUILayout.Vector2Field("first point", bezierCollider.firstPointT, GUILayout.MinWidth(100));
            bezierCollider.handlerFirstPointT = EditorGUILayout.Vector2Field("handler first Point", bezierCollider.handlerFirstPointT, GUILayout.MinWidth(100));
            bezierCollider.secondPointT = EditorGUILayout.Vector2Field("second point", bezierCollider.secondPointT, GUILayout.MinWidth(100));
            bezierCollider.handlerSecondPointT = EditorGUILayout.Vector2Field("handler secondPoint", bezierCollider.handlerSecondPointT, GUILayout.MinWidth(100));

            EditorUtility.SetDirty(bezierCollider);

            if (bezierCollider.pointsQuantityY > 0 && !bezierCollider.firstPointT.Equals(bezierCollider.secondPointT) &&
                (
                    lastPointsQuantity != bezierCollider.pointsQuantityY ||
                    lastFirstPoint != bezierCollider.firstPointT ||
                    lastHandlerFirstPoint != bezierCollider.handlerFirstPointT ||
                    lastSecondPoint != bezierCollider.secondPointT ||
                    lastHandlerSecondPoint != bezierCollider.handlerSecondPointT
                ))
            {
                lastPointsQuantity = bezierCollider.pointsQuantityY;
                lastFirstPoint = bezierCollider.firstPointT;
                lastHandlerFirstPoint = bezierCollider.handlerFirstPointT;
                lastSecondPoint = bezierCollider.secondPointT;
                lastHandlerSecondPoint = bezierCollider.handlerSecondPointT;
                edgeCollider.points = bezierCollider.Calculate2DPoints();
            }

        }
    }

    void OnSceneGUI()
    {
        if (bezierCollider != null)
        {
            Handles.color = Color.grey;

            Handles.DrawLine(bezierCollider.transform.position + (Vector3)bezierCollider.handlerFirstPointT, bezierCollider.transform.position + (Vector3)bezierCollider.firstPointT);
            Handles.DrawLine(bezierCollider.transform.position + (Vector3)bezierCollider.handlerSecondPointT, bezierCollider.transform.position + (Vector3)bezierCollider.secondPointT);

            var fmh_66_138_638479559903524969 = Quaternion.identity; bezierCollider.firstPointT = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.firstPointT), 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.firstPointT)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;
            var fmh_67_140_638479559903555057 = Quaternion.identity; bezierCollider.secondPointT = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.secondPointT), 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.secondPointT)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;
            var fmh_68_152_638479559903560252 = Quaternion.identity; bezierCollider.handlerFirstPointT = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.handlerFirstPointT), 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.handlerFirstPointT)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;
            var fmh_69_154_638479559903564892 = Quaternion.identity; bezierCollider.handlerSecondPointT = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.handlerSecondPointT), 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.handlerSecondPointT)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }

}