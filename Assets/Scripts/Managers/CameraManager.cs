using UnityEngine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private float _dragSpeed = 0.01f;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

                _mainCamera.transform.Translate(-TouchDeltaPosition.x * _dragSpeed, -TouchDeltaPosition.y * _dragSpeed, 0);

                _mainCamera.transform.position = new Vector3(
                    Mathf.Clamp(_mainCamera.transform.position.x, 5, 65),
                    Mathf.Clamp(_mainCamera.transform.position.y, 25, 25),
                    Mathf.Clamp(_mainCamera.transform.position.z, -25, 45));
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
}