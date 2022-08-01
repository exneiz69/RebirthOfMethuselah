using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Finisher : MonoBehaviour
{
    [SerializeField] private MovementTargetPoint _finish;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameCycle _gameCycle;
    [SerializeField] private OverlapView _overlapView;

    #region MonoBehaviour

    private void OnEnable()
    {
        _playerMovement.MovementTargetPointReached += OnMovementTargetPointReached;
    }

    private void OnDisable()
    {
        _playerMovement.MovementTargetPointReached -= OnMovementTargetPointReached;
    }

    #endregion

    private void OnMovementTargetPointReached(MovementTargetPoint movementTargetPoint)
    {
        if (GameObject.ReferenceEquals(movementTargetPoint, _finish))
        {
            _overlapView.Rendered += OnOverlapRendered;
            _overlapView.Render();
        }
    }

    private void OnOverlapRendered()
    {
        _overlapView.Rendered -= OnOverlapRendered;
        _gameCycle.Reload();
    }
}