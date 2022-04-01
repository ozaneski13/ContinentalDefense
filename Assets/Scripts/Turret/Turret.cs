using UnityEngine;
public class Turret : MonoBehaviour, ITurret
{
    [SerializeField] private int _price;
    public int Price => _price;
}