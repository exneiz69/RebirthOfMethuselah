using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _handler;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private float _maxMovementDistance;
    [SerializeField] private float _movementSpeed;

    [SerializeField] private UnityEvent<MovementTargetPoint> _movementTargetPointReached;

    private Rigidbody _rigidbody;
    private bool _isMoving = false;
    private Vector3 _initialPosition;
    private MovementTargetPoint _currentMovementTargetPoint;
    private float _currentMovementInterpolant = 0;
    private float _movementDistanceRatio;

    public event UnityAction<MovementTargetPoint> MovementTargetPointReached
    {
        add => _movementTargetPointReached.AddListener(value);
        remove => _movementTargetPointReached.RemoveListener(value);
    }

    public Vector3 PlayerPosition => transform.position;

    #region MonoBehaviour

    private void OnValidate()
    {
        _maxMovementDistance = _maxMovementDistance < 0 ? 0 : _maxMovementDistance;
        _movementSpeed = _movementSpeed < 0 ? 0 : _movementSpeed;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _handler.MovementTargetPointClicked += OnMovementTargetPointClicked;
    }

    private void OnDisable()
    {
        _handler.MovementTargetPointClicked -= OnMovementTargetPointClicked;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _currentMovementInterpolant = Mathf.MoveTowards(_currentMovementInterpolant, 1, _movementDistanceRatio * Time.deltaTime);
            transform.position = Vector3.Lerp(_initialPosition, _currentMovementTargetPoint.transform.position, _movementCurve.Evaluate(_currentMovementInterpolant));
            if (_currentMovementInterpolant == 1)
            {
                _isMoving = false;
                _movementTargetPointReached?.Invoke(_currentMovementTargetPoint);
                if (_currentMovementTargetPoint is ITeleport teleport)
                {
                    transform.position = teleport.Destination.transform.position;
                    _movementTargetPointReached?.Invoke(teleport.Destination);
                }
            }
        }
    }

    #endregion

    private void OnMovementTargetPointClicked(MovementTargetPoint point)
    {
        if (!_isMoving && CheckAvailability(point))
        {
            _initialPosition = transform.position;
            _currentMovementTargetPoint = point;
            _currentMovementInterpolant = 0;
            _movementDistanceRatio = _movementSpeed / Vector3.Distance(point.transform.position, transform.position);
            _isMoving = true;
        }
    }

    private bool CheckAvailability(MovementTargetPoint movementTargetPoint)
    {
        var distance = Vector3.Distance(transform.position, movementTargetPoint.transform.position);
        if (distance <= _maxMovementDistance)
        {
            return !_rigidbody.SweepTest(
                movementTargetPoint.transform.position - transform.position,
                out _,
                distance,
                QueryTriggerInteraction.Ignore
                );
        }
        else
        {
            return false;
        }
    }
}