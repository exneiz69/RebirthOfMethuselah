using UnityEngine;

[CreateAssetMenu(fileName = "CausalityChainsGeneratorProvider", menuName = "CausalityChainsGeneratorProvider", order = 153)]
public class CausalityChainsGeneratorProvider : ScriptableObject
{
    [SerializeField] private STObjectsInteractions _stObjectsInteractions;
    [SerializeField] private STObjectEffect _initialEffect;
    [SerializeField] private STObjectEffect _objectiveEffect;

    private CausalityChainsGenerator _causalityChainsGenerator;

    public CausalityChain Generate()
    {
        if (_causalityChainsGenerator is null)
        {
            _causalityChainsGenerator = new CausalityChainsGenerator(_stObjectsInteractions, _initialEffect, _objectiveEffect);
        }
        if (_causalityChainsGenerator.Generate() is CausalityChain chain)
        {
            return chain;
        }
        else
        {
            throw new System.InvalidOperationException();
        }
    }
}