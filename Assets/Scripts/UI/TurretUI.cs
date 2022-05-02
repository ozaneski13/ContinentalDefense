using UnityEngine;

public class TurretUI : MonoBehaviour
{
    [SerializeField] private GameObject _turretCanvas = null;

    private Node _targetNode = null;
    public Node TargetNode => _targetNode;

    public void SetTargetNode(Node node)
    {
        _targetNode = node;

        transform.position = _targetNode.transform.position + _targetNode.PositionOffset;

        _turretCanvas.SetActive(true);
    }

    public void Hide()
    {
        _turretCanvas.SetActive(false);
    }
}