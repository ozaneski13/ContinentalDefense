using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private ToggleStatus _toggleStatus = null;

    [SerializeField] private string _nodeTag = null;

    private Camera _mainCamera = null;

    private bool _canBuy = false;
    private bool _canSell = false;
    private bool _canUpgrade = false;

    private void Awake()
    {
        RegisterToEvents();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                Ray raycast = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit) && raycastHit.transform.gameObject.tag == _nodeTag)
                {
                    CheckNode(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        UnregisterFromEvents();
    }

    private void RegisterToEvents()
    {
        _toggleStatus.ToggleChanged += ToggleChanged;
    }

    private void UnregisterFromEvents()
    {
        _toggleStatus.ToggleChanged -= ToggleChanged;
    }

    private void ToggleChanged(EToggle toggleStatus)
    {
        _canSell = false;
        _canBuy = false;
        _canUpgrade = false;

        switch (toggleStatus)
        {
            case EToggle.Buy:
                _canBuy = true;
                break;

            case EToggle.Sell:
                _canSell = true;
                break;

            case EToggle.Upgrade:
                _canUpgrade = true;
                break;

            case EToggle.Roam:
                break;
        }
    }

    private void CheckNode(GameObject nodeGO)
    {
        Node node = nodeGO.GetComponent<Node>();

        if (node == null)
            return;

        if (node.CurrentTurret == null && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && _canBuy)
        {
            node.CreateNewTurret();
        }

        else if(node.CurrentTurret != null)
        {
            Turret turret = node.CurrentTurret.GetComponent<Turret>();

            if (turret == null)
                return;

            if (_canSell)
            {
                SellTurret(node);

                return;
            }

            if (!turret.Upgradable)
            {
                node.NodeOccupied();

                return;
            }

            if (turret.Upgradable && _canUpgrade)
            {
                UpgradeTurret(node);

                return;
            }
        }
    }

    private void SellTurret(Node node)
    {
        node.SellCurrentTurret();
    }

    private void UpgradeTurret(Node node)
    {
        node.UpgradeCurrentTurret();
    }
}