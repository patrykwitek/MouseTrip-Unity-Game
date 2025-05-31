using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float normalSize = 5f;
    [SerializeField] private float zoomedOutSize = 7f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private KeyCode zoomKey = KeyCode.F;

    private Camera mainCamera;
    private bool isZoomedOut = false;
    private float targetSize;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        targetSize = normalSize;
        mainCamera.orthographicSize = normalSize;
    }

    private void Update()
    {
        HandleZoomInput();
        SmoothZoom();
    }

    private void HandleZoomInput()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            isZoomedOut = true;
            targetSize = zoomedOutSize;
        }
        else if (Input.GetKeyUp(zoomKey))
        {
            isZoomedOut = false;
            targetSize = normalSize;
        }
    }

    private void SmoothZoom()
    {
        if (Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.01f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(
                mainCamera.orthographicSize,
                targetSize,
                Time.deltaTime * zoomSpeed
            );
        }
        else
        {
            mainCamera.orthographicSize = targetSize;
        }
    }
    
    private void OnDisable()
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = normalSize;
        }
    }
}