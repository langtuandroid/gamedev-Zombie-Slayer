using UnityEngine;
using System.Collections;
using Script;
using UnityEngine.Serialization;

public class CameraFollowZS : MonoBehaviour
{
    public static CameraFollowZS Instance;
    [Tooltip("Limited the camera moving within this box collider")]
    
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = false;
    
    [FormerlySerializedAs("limitLeft")] [SerializeField] private float limitLeftT = -5;
    [FormerlySerializedAs("limitRight")] [SerializeField] private float limitRightT = 50;
    [FormerlySerializedAs("limitDown")] [SerializeField] private float limitDownN = -3;
    [FormerlySerializedAs("limitUp")] [SerializeField] private float limitUpP = 5;
    [FormerlySerializedAs("smooth")] [SerializeField] private float smoothH = 1;
    
    [SerializeField] private  Vector2 offset = Vector2.zero;
    
    [HideInInspector]
    public Vector2 min;

    [HideInInspector]
    public Vector2 max;

    [ReadOnly] public bool manualControl = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetDefaultLimit();
    }

    public void SetDefaultLimit()
    {
        min = new Vector2(limitLeftT, limitDownN);
        max = new Vector2(limitRightT, limitUpP);
    }

    private void LateUpdate()
    {
        if (!manualControl)
            FollowPlayer();
    }

    public void FollowPlayer()
    {
        Vector2 focusPosition = (Vector2)GameManagerZS.Instance.player.transform.position + offset;

        focusPosition.x = Mathf.Clamp(focusPosition.x, min.x + CameraHalfWidth, max.x - CameraHalfWidth);
        focusPosition.y = Mathf.Clamp(focusPosition.y, min.y + Camera.main.orthographicSize, max.y - Camera.main.orthographicSize);

        if (!followX)
            focusPosition.x = transform.position.x;
        if (!followY)
            focusPosition.y = transform.position.y;

        transform.position = Vector3.Lerp(transform.position, (Vector3)focusPosition + Vector3.forward * -10, smoothH);
    }

    private float CameraHalfWidth => (Camera.main.orthographicSize * ((float)Screen.width / Screen.height));

    public void TempLimitCameraA(float minX, float maxX)
    {
        min.x = minX;
        max.x = maxX;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 boxSize = new Vector2(limitRightT - limitLeftT, limitUpP - limitDownN);
        Vector2 center = (new Vector2(limitRightT + limitLeftT, limitUpP + limitDownN)) * 0.5f;
        Gizmos.DrawWireCube(center, boxSize);
    }
}
