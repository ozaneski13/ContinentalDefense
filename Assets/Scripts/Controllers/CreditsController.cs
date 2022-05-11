using System.Collections;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator _animator = null;
    [SerializeField] private float _waitTimeAfterAnimationEnd = 1f;

    private IEnumerator _animatorRoutine = null;

    private bool _isPressed = false;

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                EndCredits();
        }

        CheckAnimation();
    }

    private void EndCredits()
    {
        if (_isPressed)
            return;

        _isPressed = true;

        FadeUI.Instance.FadeTo(0);
    }

    private void CheckAnimation()
    {
        if (_isPressed)
            return;

        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_animator.IsInTransition(0) && _animatorRoutine == null)
        {
            _animatorRoutine = AnimatorRoutine();
            StartCoroutine(_animatorRoutine);
        }
    }

    private IEnumerator AnimatorRoutine()
    {
        yield return new WaitForSeconds(_waitTimeAfterAnimationEnd);
        EndCredits();
    }
}