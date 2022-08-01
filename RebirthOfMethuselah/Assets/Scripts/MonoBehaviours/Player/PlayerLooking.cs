using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLooking : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _handler;
    [SerializeField] private float _rotationSpeed;

    private Camera _camera;
    private Vector2 _lastLookDelta;

    public Vector3 Forward => _camera.transform.forward;

    #region MonoBehaviour

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnValidate()
    {
        _rotationSpeed = _rotationSpeed < 0 ? 0 : _rotationSpeed;
    }

    private void OnEnable()
    {
        _handler.LookDeltaChanged += OnLookDeltaChanged;
    }

    private void OnDisable()
    {
        _handler.LookDeltaChanged -= OnLookDeltaChanged;
    }

    private void LateUpdate()
    {
        transform.Rotate(Vector3.up * _lastLookDelta.x * _rotationSpeed);
        _camera.transform.Rotate(Vector3.right * _lastLookDelta.y * _rotationSpeed);
    }

    #endregion

    private void OnLookDeltaChanged(Vector2 lookDelta)
    {
        _lastLookDelta = lookDelta;
    }
}