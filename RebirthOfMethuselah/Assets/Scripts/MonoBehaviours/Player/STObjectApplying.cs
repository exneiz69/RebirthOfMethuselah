using System.Collections;
using UnityEngine;

public class STObjectApplying : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _handler;
    [SerializeField] private PlayerPicking _picking;
    [SerializeField] private STObjectActionsView _stObjectActionsView;
    [SerializeField] private STObjectView _stObjectView;

    private bool _isActionsRendered = false;

    #region MonoBehaviour

    private void OnEnable()
    {
        _handler.SmartTerrainObjectClicked += OnSmartTerrainObjectClicked;
        _handler.Dropped += OnDroped;
        _stObjectActionsView.CloseActionPerformed += OnCloseActionPerformed;
        _stObjectActionsView.PickUpActionPerformed += OnPickUpActionPerformed;
    }

    private void OnDisable()
    {
        _handler.SmartTerrainObjectClicked -= OnSmartTerrainObjectClicked;
        _handler.Dropped -= OnDroped;
        _stObjectActionsView.CloseActionPerformed -= OnCloseActionPerformed;
        _stObjectActionsView.PickUpActionPerformed -= OnPickUpActionPerformed;
    }

    #endregion
    
    private void OnSmartTerrainObjectClicked(STObject passive)
    {
        _handler.SetApplyEnabled(false);
        _handler.SetLookEnabled(false);
        _handler.SetCursorEnabled(true);
        if (_picking.HasPickedItem && _picking.PickedItem is STObject active)
        {
            _stObjectActionsView.Render(active, passive);
        }
        else
        {
            _stObjectActionsView.Render(passive);
        }
        _isActionsRendered = true;
    }

    private void OnDroped()
    {
        if (_picking.HasPickedItem && !_isActionsRendered)
        {
            _picking.Drop();
            _stObjectView.Clear();
        }
    }

    private void OnCloseActionPerformed()
    {
        _handler.SetApplyEnabled(true);
        _handler.SetLookEnabled(true);
        _handler.SetCursorEnabled(false);
        _isActionsRendered = false;
    }

    private void OnPickUpActionPerformed(IPickable pickable)
    {
        if (!_picking.HasPickedItem)
        {
            _picking.Pick(pickable);
            if (pickable is STObject stObject)
            {
                _stObjectView.Render(stObject);
            }
        }
    }
}