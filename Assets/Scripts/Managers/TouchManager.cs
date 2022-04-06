using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;

    private bool _sellClicked = false;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray raycast = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit) && raycastHit.transform.gameObject.tag == "Node")
                {
                    CheckNode(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private void CheckNode(GameObject nodeGO)
    {
        Node node = nodeGO.GetComponent<Node>();

        if (node.CurrentTurret == null && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            node.CreateNewTurret();
        }

        else if(node.CurrentTurret != null)// && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
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

            if (turret.Upgradable)
            {
                //Upgrade();

                return;
            }
        }

        /*Geliştirme yapılacak mı? Yapılacaksa geliştirme ekranı aç.
          Doluysa sat*/
    }

    public void SellClicked()
    {
        _sellClicked = true;
    }
}