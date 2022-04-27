using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private ToggleStatus _toggleStatus = null;

    [SerializeField] private string _nodeTag = null;

    private Camera _mainCamera = null;

    private bool _sellClicked = false;
    private bool _buyClicked = false;
    private bool _upgradeClicked = false;

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
        _sellClicked = false;
        _buyClicked = false;
        _upgradeClicked = false;

        switch (toggleStatus)
        {
            case EToggle.Buy:
                _buyClicked = true;
                break;

            case EToggle.Sell:
                _sellClicked = true;
                break;

            case EToggle.Upgrade:
                _upgradeClicked = true;
                break;
        }
    }

    private void CheckNode(GameObject nodeGO)
    {
        Node node = nodeGO.GetComponent<Node>();

        if (node == null)
            return;

        if (node.CurrentTurret == null && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && _buyClicked)
        {
            node.CreateNewTurret();
        }

        else if(node.CurrentTurret != null)
        {
            Turret turret = node.CurrentTurret.GetComponent<Turret>();

            if (_sellClicked)
            {
                //Sell();

                return;
            }

            if (!turret.Upgradable)
            {
                node.NodeOccupied();

                return;
            }

            if (turret.Upgradable && _upgradeClicked)
            {
                //Upgrade();

                return;
            }
        }

        /*Geliştirme yapılacak mı? Yapılacaksa geliştirme ekranı aç.
          Doluysa sat*/
    }
}