using UnityEngine;

public class ExplosionSpawner : MonoBehaviour
{
    [SerializeField] private Explosion _template;
    [SerializeField] private Vector3 _position;

    public void Spawn()
    {
        var explosion = Instantiate(_template, _position, Quaternion.identity);
        explosion.Explode();
    }
}
