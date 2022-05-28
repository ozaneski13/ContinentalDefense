using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    #region Singleton
    public static TouchManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    #endregion

    [Header("Nothing Selected UI")]
    [SerializeField] private GameObject _nothingSelectedUI = null;

    [Header("Tutorial Only")]
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

        if (nodeGO.GetComponent<LandMine>() != null)
            node = nodeGO.GetComponent<LandMine>().LandMineNode;

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

    public void NothingSelected()
    {
        StartCoroutine(NothingSelectedRoutine());
    }

    private IEnumerator NothingSelectedRoutine()
    {
        _nothingSelectedUI.SetActive(true);

        yield return new WaitForSeconds(2f);

        _nothingSelectedUI.SetActive(false);
    }
}