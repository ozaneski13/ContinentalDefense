using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private TriggerController _turretTutorialTrigger = null;

    private BuildManager _buildManager = null;

    private Camera _mainCamera = null;

    private bool _canCheck = true;

    private void Start()
    {
        if (_turretTutorialTrigger != null)
        {
            _canCheck = false;

            _turretTutorialTrigger.triggerActivated += CanCheck;
        }

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

                if (Physics.Raycast(raycast, out raycastHit) && _canCheck)
                {
                    CheckNode(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (_turretTutorialTrigger != null)
        {
            _turretTutorialTrigger.triggerActivated -= CanCheck;
        }
    }

    private void CanCheck(int index)
    {
        _canCheck = true;
    }

    private void CheckNode(GameObject nodeGO)
    {
        Node node = nodeGO.GetComponent<Node>();

        if (node == null)
            return;

        if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            if (node.CurrentSpawnable == null)
                node.CreateNew();

            else if (node.CurrentSpawnable != null)
                _buildManager.SetNode(node);
        }
    }
}