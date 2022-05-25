using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private TriggerController _upgradeTurretTutorialTrigger = null;

    private BuildManager _buildManager = null;

    private Camera _mainCamera = null;

    private bool _canCheck = true;

    private void Start()
    {
        if (_upgradeTurretTutorialTrigger != null)
        {
            _canCheck = false;

            _upgradeTurretTutorialTrigger.triggerActivated += CanCheck;
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
        if (_upgradeTurretTutorialTrigger != null)
        {
            _upgradeTurretTutorialTrigger.triggerActivated -= CanCheck;
        }
    }

    private void CanCheck(int index)
    {
        StartCoroutine(CheckRoutine());
    }

    private IEnumerator CheckRoutine()
    {
        yield return new WaitForSeconds(1f);
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