using UnityEngine;

public class TeleportSpawner : MonoBehaviour
{
    [SerializeField] private Teleport _tamplate;
    [SerializeField] private Vector3 _spawnPoint;
    [SerializeField] private MovementTargetPoint _teleportDestination;

    public void Spawn()
    {
        var teleport = Instantiate(_tamplate, _spawnPoint, Quaternion.identity);
        teleport.Init(_teleportDestination);
    }
}
