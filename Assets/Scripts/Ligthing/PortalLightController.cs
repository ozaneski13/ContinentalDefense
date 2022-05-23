using System.Collections;
using UnityEngine;

public class PortalLightController : MonoBehaviour
{
    [Header("Portal Light")]
    [SerializeField] private Light _portalLight = null;

    [Header("Intensity Thresholds")]
    [SerializeField] private float _minIntensity = 10f;
    [SerializeField] private float _maxIntensity = 20f;

    [Header("Intensity Thresholds")]
    [SerializeField] private float _flickerDuration = 0.1f;

    private void Start()
    {
        StartCoroutine(LightFlicker());
    }

    private IEnumerator LightFlicker()
    {
        float increase = -1;

        while(true)
        {
            if (_portalLight.intensity <= _minIntensity)
                increase = 1;
            else if (_portalLight.intensity >= _maxIntensity)
                increase = -1;

            _portalLight.intensity += increase;

            yield return new WaitForSeconds(_flickerDuration);
        }
    }
}