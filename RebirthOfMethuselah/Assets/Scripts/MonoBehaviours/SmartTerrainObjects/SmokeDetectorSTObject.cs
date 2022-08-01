using UnityEngine;

public class SmokeDetectorSTObject : SmokableSTObject, ISmokeDetector
{
    public bool CanWet { get; private set; } = false;

    #region MonoBehaviour

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (!CanWet)
        {
            return;
        }
        else if (other.TryGetComponent<IWettable>(out IWettable wettable) && !wettable.IsWetted)
        {
            Wet(wettable);
        }
    }

    #endregion

    public void Wet(IWettable target)
    {
        if (target is null)
        {
            throw new System.ArgumentNullException(nameof(target));
        }
        else if (!CanWet)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            target.Wet();
        }
    }

    public override void Smoke()
    {
        base.Smoke();
        CanWet = true;
    }
}