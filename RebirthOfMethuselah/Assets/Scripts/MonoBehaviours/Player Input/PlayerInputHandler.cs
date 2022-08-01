using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private CrosshairView _crosshairView;
    [SerializeField] private PausePanel _pausePanel;
    [SerializeField] private float _maxApplyRaycastDistance;

    private Vector2 _lastApplyPosition;
    private bool _applyEnabledBeforePause;
    private bool _lookEnabledBeforePause;
    private bool _dropEnabledBeforePause;
    private bool _cursorEnabledBeforePause;
    private bool _paused = false;

    public event UnityAction<MovementTargetPoint> MovementTargetPointClicked;
    public event UnityAction<STObject> SmartTerrainObjectClicked;
    public event UnityAction<Vector2> LookDeltaChanged;
    public event UnityAction Dropped;

    public bool ApplyEnabled { get; private set; } = true;

    public bool LookEnabled { get; private set; } = true;

    public bool DropEnabled { get; private set; } = true;

    public bool CursorEnabled { get; private set; } = false;

    #region MonoBehaviour

    private void Awake()
    {
        SetCursorEnabled(false);
    }

    private void OnValidate()
    {
        _maxApplyRaycastDistance = _maxApplyRaycastDistance < 0 ? 0 : _maxApplyRaycastDistance;
    }

    #endregion

    public void OnApply(InputAction.CallbackContext callbackContext)
    {
        if (ApplyEnabled && callbackContext.performed)
        {
            Vector3 applyPosition = _lastApplyPosition;
            applyPosition.z = Camera.main.nearClipPlane;
            var ray = Camera.main.ScreenPointToRay(applyPosition);
            if (TryFind(ray, out MovementTargetPoint point))
            {
                MovementTargetPointClicked?.Invoke(point);
            }
            else if (TryFind(ray, out STObject obj))
            {
                SmartTerrainObjectClicked?.Invoke(obj);
            }
        }
    }

    public void OnApplyPosition(InputAction.CallbackContext callbackContext)
    {
        if (ApplyEnabled)
        {
            _lastApplyPosition = callbackContext.ReadValue<Vector2>();
        }
    }

    public void SetApplyEnabled(bool value)
    {
        ApplyEnabled = value;
    }

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        if (LookEnabled)
        {
            LookDeltaChanged?.Invoke(callbackContext.ReadValue<Vector2>());
        }
    }

    public void SetLookEnabled(bool value)
    {
        LookEnabled = value;
        LookDeltaChanged?.Invoke(Vector2.zero);
    }

    public void OnDrop(InputAction.CallbackContext callbackContext)
    {
        if (DropEnabled && callbackContext.performed)
        {
            Dropped?.Invoke();
        }
    }

    public void SetDropEnabled(bool value)
    {
        DropEnabled = value;
    }

    public void OnPause(InputAction.CallbackContext callbackContext)
    {
        if (!_paused && callbackContext.performed)
        {
            _paused = true;
            _pausePanel.Hidden += OnPausePanelHidden;
            _pausePanel.Show();
            _applyEnabledBeforePause = ApplyEnabled;
            _lookEnabledBeforePause = LookEnabled;
            _dropEnabledBeforePause = DropEnabled;
            _cursorEnabledBeforePause = CursorEnabled;
            SetApplyEnabled(false);
            SetLookEnabled(false);
            SetDropEnabled(false);
            SetCursorEnabled(true);
        }
    }

    public void SetCursorEnabled(bool value)
    {
        CursorEnabled = value;
        if (value)
        {
            _crosshairView.Hide();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _crosshairView.Show();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private bool TryFind<T>(Ray ray, out T component) where T : class
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit, _maxApplyRaycastDistance))
        {
            if (raycastHit.collider.TryGetComponent<T>(out component))
            {
                return true;
            }
        }
        component = null;
        return false;
    }

    private void OnPausePanelHidden()
    {
        _paused = false;
        _pausePanel.Hidden -= OnPausePanelHidden;
        SetApplyEnabled(_applyEnabledBeforePause);
        SetLookEnabled(_lookEnabledBeforePause);
        SetDropEnabled(_dropEnabledBeforePause);
        SetCursorEnabled(_cursorEnabledBeforePause);
    }
}