using System.Linq;
using UnityEngine;

public class STObjectsSpawner : MonoBehaviour
{
    [SerializeField] private CausalityChainsGeneratorProvider _causalityChainsGeneratorProvider;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _container;

    private void Start()
    {
        var chain = _causalityChainsGeneratorProvider.Generate();
        var random = new System.Random();
        var shuffledSpawnPoints = _spawnPoints.OrderBy(item => random.Next()).ToArray();
        int currentSpawnPointIndex = 0;
        foreach (var interaction in chain)
        {
            var stObject = interaction.Group[random.Next(interaction.Group.Count)];
            if (!stObject.IsPersistent)
            {
                Instantiate(stObject, shuffledSpawnPoints[currentSpawnPointIndex].position, Quaternion.identity, _container);
                currentSpawnPointIndex++;
            }
        }
    }
}