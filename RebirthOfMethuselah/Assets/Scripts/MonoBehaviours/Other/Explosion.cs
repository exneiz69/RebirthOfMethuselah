using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private AudioSource _audioSource;

    private bool _isExploded = false;

    public void Explode()
    {
        if (!_isExploded)
        {
            _isExploded = true;
            _effect.Play();
            _audioSource.Play();
            StartCoroutine(DoExplosion(_effect.main.duration));
        }
    }

    private IEnumerator DoExplosion(float time)
    {
        var waitForSeconds = new WaitForSeconds(time);
        yield return waitForSeconds;
        Destroy(gameObject);
    }
}
