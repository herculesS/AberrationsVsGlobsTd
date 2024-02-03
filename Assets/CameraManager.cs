using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    float edgeValue = 20f;
    float cameraSpeed = 20f;
    Camera mainCamera;
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleEdgeScrolling();

    }
    void HandleEdgeScrolling()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        bool isMouseInRightEdge = mousePos.x > Screen.width - edgeValue;
        bool isMouseInLeftEdge = mousePos.x < edgeValue;
        bool isMouseInTopEdge = mousePos.y > Screen.height - edgeValue;
        bool isMouseInDownEdge = mousePos.y < edgeValue;
        if (isMouseInRightEdge || isMouseInLeftEdge || isMouseInTopEdge || isMouseInDownEdge)
        {
            Vector3 newPosition = mainCamera.transform.position;
            if (isMouseInRightEdge)
            {
                newPosition.x += cameraSpeed * Time.deltaTime;
            }
            if (isMouseInLeftEdge)
            {
                newPosition.x -= cameraSpeed * Time.deltaTime;
            }
            if (isMouseInTopEdge)
            {
                newPosition.y += cameraSpeed * Time.deltaTime;
            }
            if (isMouseInDownEdge)
            {
                newPosition.y -= cameraSpeed * Time.deltaTime;
            }
            Vector3Int tilemapSize = TilemapManager.Instance.getTotalTileMapsSize();
            float halfCameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
            float halfCameraHeight = mainCamera.orthographicSize;
            newPosition.x = Mathf.Clamp(newPosition.x, -tilemapSize.x / 2f + halfCameraWidth, tilemapSize.x / 2f - halfCameraWidth);
            newPosition.y = Mathf.Clamp(newPosition.y, -tilemapSize.y / 2f + halfCameraHeight, tilemapSize.y / 2f - halfCameraHeight);
            mainCamera.transform.position = newPosition;
        }
    }
}
