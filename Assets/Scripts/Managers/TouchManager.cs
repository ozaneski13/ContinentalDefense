using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    private BuildManager _buildManager = null;

    private Camera _mainCamera = null;

    private void Start()
    {
        _buildManager = BuildManager.Instance;

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    CheckNode(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private void CheckNode(GameObject nodeGO)
    {
        Node node = nodeGO.GetComponent<Node>();

        if (node == null)
            return;

        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            if (node.CurrentTurret == null)
                node.CreateNewTurret();

            else if (node.CurrentTurret != null)
                _buildManager.SetNode(node);
        }
    }
}