using System.Collections.Generic;
using System.Linq;

public class CausalityChainsGenerator
{
    private const int MaxInteractionsNumberInChain = 8;

    private readonly STObjectsInteractions _interactions;
    private readonly LinkedList<CausalityChain> _builtCausalityChains;
    private readonly STObjectEffect _initial;
    private readonly STObjectEffect _objective;
    private readonly List<STObjectsInteraction> _objectives;
    private int _currentObjectiveIndex;

    public CausalityChainsGenerator(STObjectsInteractions interactions, STObjectEffect initial, STObjectEffect objective)
    {
        if (interactions is null)
        {
            throw new System.ArgumentNullException(nameof(interactions));
        }
        else if (interactions.Count == 0)
        {
            throw new System.ArgumentException(nameof(interactions));
        }
        else if (initial is null)
        {
            throw new System.ArgumentNullException(nameof(initial));
        }
        else if (objective is null)
        {
            throw new System.ArgumentNullException(nameof(objective));
        }
        else
        {
            _interactions = interactions;
            _builtCausalityChains = new LinkedList<CausalityChain>();
            _initial = initial;
            _objective = objective;
            _objectives = FindAllObjectives();
            _currentObjectiveIndex = 0;
        }
    }

    public CausalityChain Generate()
    {
        CausalityChain result;
        if (TryBuildCausalityChain(out result))
        {
            return result;
        }
        else
        {
            Reset();
            if (TryBuildCausalityChain(out result))
            {
                return result;
            }
            else
            {
                throw new System.InvalidOperationException();
            }
        }
    }

    private void Reset()
    {
        _currentObjectiveIndex = 0;
        _builtCausalityChains.Clear();
    }

    private List<STObjectsInteraction> FindAllObjectives()
    {
        var objectives = new List<STObjectsInteraction>();
        for (int i = 0; i < _interactions.Count; i++)
        {
            if (_interactions[i].Effect == _objective)
            {
                objectives.Add(_interactions[i]);
            }
        }
        if (objectives.Count == 0)
        {
            throw new System.InvalidOperationException();
        }
        else
        {
            var random = new System.Random();
            var result = objectives.OrderBy(item => random.Next()).ToList();
            return result;
        }
    }

    private bool TryBuildCausalityChain(out CausalityChain result)
    {
        if (TryBuildCausalityChainRecursively(_objectives[_currentObjectiveIndex], new STObjectsInteraction[MaxInteractionsNumberInChain], out result))
        {
            _builtCausalityChains.AddLast(result);
            return true;
        }
        else if (TryChangeNextObjective())
        {
            return TryBuildCausalityChain(out result);
        }
        else
        {
            result = null;
            return false;
        }
    }

    private bool TryChangeNextObjective()
    {
        if (_currentObjectiveIndex < _objectives.Count - 1)
        {
            _currentObjectiveIndex++;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool TryBuildCausalityChainRecursively(STObjectsInteraction currentInteraction, STObjectsInteraction[] interactionsToBuild, out CausalityChain result, int interactionsNumber = 0)
    {
        result = null;
        if (TryAddInteraction(currentInteraction, interactionsToBuild, ref interactionsNumber))
        {
            if (currentInteraction.Cause == _initial)
            {
                return TryCreateCausalityChain(interactionsToBuild, interactionsNumber, out result);
            }
            for (int i = 0; i < _interactions.Count; i++)
            {
                if (TryFindInteractionIndexWithEffect(currentInteraction.Cause, i, out i))
                {
                    if (TryBuildCausalityChainRecursively(_interactions[i], interactionsToBuild, out result, interactionsNumber))
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        return false;
    }

    private bool TryAddInteraction(STObjectsInteraction interaction, STObjectsInteraction[] interactionsToBuild, ref int interactionsNumber)
    {
        if (interactionsNumber >= MaxInteractionsNumberInChain)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < interactionsNumber; i++)
            {
                if (interactionsToBuild[i] == interaction)
                {
                    return false;
                }
            }
            interactionsToBuild[interactionsNumber] = interaction;
            interactionsNumber++;
            return true;
        }
    }

    private bool TryCreateCausalityChain(STObjectsInteraction[] interactionsToBuild, int interactionsNumber, out CausalityChain result)
    {
        result = null;
        var newCausalityChain = new CausalityChain();
        for (int i = 0; i < interactionsNumber; i++)
        {
            newCausalityChain.Add(interactionsToBuild[i]);
        }
        foreach (CausalityChain causalityChain in _builtCausalityChains)
        {
            if (causalityChain.Equals(newCausalityChain))
            {
                return false;
            }
        }
        result = newCausalityChain;
        return true;
    }

    private bool TryFindInteractionIndexWithEffect(STObjectEffect effect, int fromIndex, out int resultIndex)
    {
        for (; fromIndex < _interactions.Count; fromIndex++)
        {
            if (_interactions[fromIndex].Effect == effect)
            {
                resultIndex = fromIndex;
                return true;
            }
        }
        resultIndex = -1;
        return false;
    }
}