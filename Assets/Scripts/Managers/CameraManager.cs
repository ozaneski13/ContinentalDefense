using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Drag")]
    [SerializeField] private float _dragSpeed = 0.01f;

    private Camera _mainCamera = null;

    private GameManager _gameManager = null;

    private Vector3 _mainCameraStartingPosition = Vector3.zero;

    private bool _isGameEnded = false;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _mainCamera = Camera.main;

        _mainCameraStartingPosition = _mainCamera.transform.position;

        RegisterToEvents();
    }

    private void Update()
    {
        if (_isGameEnded)
            return;

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

                _mainCamera.transform.Translate(-TouchDeltaPosition.x * _dragSpeed, -TouchDeltaPosition.y * _dragSpeed, 0);

                _mainCamera.transform.position = new Vector3(
                    Mathf.Clamp(_mainCamera.transform.position.x, 25, 55),
                    Mathf.Clamp(_mainCamera.transform.position.y, _mainCameraStartingPosition.y, _mainCameraStartingPosition.y),
                    Mathf.Clamp(_mainCamera.transform.position.z, 5, 30));
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            _mainCamera.fieldOfView = Mathf.Clamp(_mainCamera.fieldOfView - difference * 0.01f, 40, 80);
        }
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _gameManager.GameEnded += GameStoped;
    }

    private void UnregisterFromEvents()
    {
        _gameManager.GameEnded -= GameStoped;
    }

    private void GameStoped()
    {
        _isGameEnded = true;
    }

    public void GamePaussed()
    {
        GameStoped();
    }
}