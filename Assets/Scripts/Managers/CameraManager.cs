using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private float _dragSpeed = 0.01f;

    private GameManager _gameManager = null;

    private Vector3 _mainCameraStartingPosition = Vector3.zero;

    private bool _isGameEnded = false;

    private void Start()
    {
        _gameManager = GameManager.Instance;

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

            _mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(_mainCamera.GetComponent<Camera>().fieldOfView - difference * 0.01f, 40, 80);
        }
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _gameManager.GameEnded += GameEnded;
    }

    private void UnregisterFromEvents()
    {
        _gameManager.GameEnded -= GameEnded;
    }

    private void GameEnded()
    {
        _isGameEnded = true;
    }
}