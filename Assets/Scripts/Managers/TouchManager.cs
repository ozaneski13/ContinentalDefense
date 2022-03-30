using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;
    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit) && raycastHit.transform.gameObject.tag == "Node")
                {

                }
            }
        }
    }
}
