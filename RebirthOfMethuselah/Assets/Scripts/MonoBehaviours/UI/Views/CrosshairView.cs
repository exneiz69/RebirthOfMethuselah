using UnityEngine;

public class CrosshairView : MonoBehaviour
{
    #region MonoBehaviour

    private void Awake()
    {
        Show();
    }

    #endregion

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
