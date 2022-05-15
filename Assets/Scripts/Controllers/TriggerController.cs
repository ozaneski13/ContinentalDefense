using System;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] private int _triggerIndex = 0;

    private bool _isActivated = false;

    public Action<int> triggerActivated;
    
    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy == null || _isActivated)
            return;

        _isActivated = true;

        triggerActivated?.Invoke(_triggerIndex);
    }
}