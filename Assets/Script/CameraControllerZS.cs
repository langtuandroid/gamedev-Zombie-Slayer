using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraControllerZS : MonoBehaviour
{
    [SerializeField] private float limitLeftT = -6;
    [SerializeField] private float limitRightT = 1000;

    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float distanceScale = 1;
    
    private float beginXx;
    private float beginCamreaPosXx;
    private bool isDraggingG = false;
    private Vector3 targetT = new Vector3(-1, 0, 0);
    private bool allowWorkingG = false;

    private IEnumerator Start()
    {
        yield return null;
        beginCamreaPosXx = transform.position.x;
        targetT = transform.position;
        targetT.x = Mathf.Clamp(transform.position.x, limitLeftT + CameraHalfWidth, limitRightT - CameraHalfWidth);
        allowWorkingG = true;
    }
    
    private void Update()
    {
        if (!allowWorkingG)
            return;

        transform.position = Vector3.Lerp(transform.position, targetT, moveSpeed * Time.fixedDeltaTime);
        if (GameManagerZS.Instance.state != GameManagerZS.GameState.Playing)
            return;
        if (!isDraggingG)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDraggingG = true;
                beginXx = Input.mousePosition.x;
                beginCamreaPosXx = transform.position.x;
            }
        }
        else
        {
            if(Input.GetMouseButtonUp(0))
            {
                isDraggingG = false;
            }
            else
            {
                targetT = new Vector3(beginCamreaPosXx + (beginXx - Input.mousePosition.x) * distanceScale * 0.01f, transform.position.y, transform.position.z);

                targetT.x = Mathf.Clamp(targetT.x, limitLeftT + CameraHalfWidth, limitRightT - CameraHalfWidth);
            }
        }
    }

    private float CameraHalfWidth => (Camera.main.orthographicSize * ((float)Screen.width / (float)Screen.height));

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.color = Color.yellow;
        Vector2 boxSize = new Vector2(limitRightT - limitLeftT, Camera.main.orthographicSize * 2);
        Vector2 center = new Vector2((limitRightT + limitLeftT) * 0.5f, transform.position.y);
        Gizmos.DrawWireCube(center, boxSize);
    }
}
