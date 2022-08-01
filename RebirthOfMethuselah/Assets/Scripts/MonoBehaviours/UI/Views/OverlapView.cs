using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OverlapView : MonoBehaviour
{
    [SerializeField] private Image _overlap;
    [SerializeField] private float _timeToOverlap;

    public event UnityAction Rendered;

    #region MonoBehaviour

    private void OnValidate()
    {
        _timeToOverlap = _timeToOverlap < 0 ? 0 : _timeToOverlap;
    }

    #endregion

    public void Render()
    {
        var target = new Color(_overlap.color.r, _overlap.color.g, _overlap.color.b, 1f);
        StartCoroutine(DoRender(target));
    }

    private IEnumerator DoRender(Color target)
    {
        float elapsedTime = 0f;
        float interpolant = 0f;
        Color current = _overlap.color;
        while (interpolant < 1f)
        {
            interpolant = elapsedTime / _timeToOverlap;
            _overlap.color = Color.Lerp(current, target, interpolant);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Rendered?.Invoke();
    }
}