using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SmokableSTObject : STObject, ISmokable
{
    [SerializeField] private float _detectionTime;

    [SerializeField] private UnityEvent _smoked;

    private Coroutine _detection;

    public bool IsSmoked { get; private set; } = false;

    #region MonoBehaviour

    protected virtual void OnValidate()
    {
        _detectionTime = _detectionTime < 0 ? 0 : _detectionTime;
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (IsSmoked)
        {
            return;
        }
        else if (other.TryGetComponent<ISmoker>(out ISmoker smoker) && smoker.CanSmoke)
        {
            if (_detection is null)
            {
                _detection = StartCoroutine(DoDetection(_detectionTime));
            }
        }
    }

    #endregion

    public virtual void Smoke()
    {
        IsSmoked = true;
        _smoked?.Invoke();
    }

    private IEnumerator DoDetection(float detectionTime)
    {
        var waitForSeconds = new WaitForSeconds(detectionTime);
        yield return waitForSeconds;
        Smoke();
    }
}