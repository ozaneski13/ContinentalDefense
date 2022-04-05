using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera = null;

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

        if (node.CurrentTurret == null)
        {
            node.CreateNewTurret();
        }

        else
        {

        }

        /*Dolu mu? Doluysa renk değiştir.
          Geliştirme yapılacak mı? Yapılacaksa geliştirme ekranı aç.
          Doluysa sat*/
    }
}