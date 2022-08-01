using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class PickableSTObject : STObject, IPickable
{
    [SerializeField] private Vector3 _pickedLocalPosition;
    [SerializeField] private Vector3 _pickedLocalRotation;

    private Rigidbody _rigidbody;
    private Transform _parentBeforePicking;
    private int _layerBeforePicking;

    public bool IsPicked { get; private set; }

    #region MonoBehaviour

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    #endregion

    public void Pick(Transform newParent, int newLayer)
    {
        if (IsPicked)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            IsPicked = true;
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _parentBeforePicking = transform.parent;
            transform.SetParent(newParent);
            transform.localPosition = _pickedLocalPosition;
            transform.localEulerAngles = _pickedLocalRotation;

            _layerBeforePicking = gameObject.layer;
            gameObject.layer = newLayer;
        }
    }

    public void Drop(Vector3 direction, float force)
    {
        if (!IsPicked)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            IsPicked = false;
            transform.SetParent(_parentBeforePicking);
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(direction.normalized * force, ForceMode.VelocityChange);

            transform.gameObject.layer = _layerBeforePicking;
        }
    }
}