using UnityEngine;

public class PlayerPicking : MonoBehaviour
{
    [SerializeField] private Transform _place;
    [SerializeField] private float _dropForce;
    [SerializeField] private int _pickedItemLayer;
    [SerializeField] private PlayerLooking _playerLooking;

    private IPickable _pickedItem;

    public IPickable PickedItem => _pickedItem;

    public bool HasPickedItem => _pickedItem is IPickable;

    #region MonoBehaviour

    private void OnValidate()
    {
        _dropForce = _dropForce < 0 ? 0 : _dropForce;
        _pickedItemLayer = _pickedItemLayer < 0 || _pickedItemLayer > 31 ? 0 : _pickedItemLayer;
    }

    #endregion

    public void Pick(IPickable item)
    {
        if (HasPickedItem)
        {
            throw new System.InvalidOperationException();
        }
        else if (item is null)
        {
            throw new System.ArgumentNullException(nameof(item));
        }
        else if (item.IsPicked)
        {
            throw new System.ArgumentException(nameof(item));
        }
        else
        {
            item.Pick(_place, _pickedItemLayer);
            _pickedItem = item;
        }
    }

    public void Drop()
    {
        if (!HasPickedItem || !_pickedItem.IsPicked)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            _pickedItem.Drop(_playerLooking.Forward, _dropForce);
            _pickedItem = null;
        }
    }
}