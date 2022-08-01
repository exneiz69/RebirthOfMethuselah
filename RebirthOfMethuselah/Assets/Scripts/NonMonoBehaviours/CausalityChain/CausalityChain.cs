using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CausalityChain : IReadOnlyCollection<STObjectsInteraction>, System.IEquatable<CausalityChain>
{
    private readonly LinkedList<STObjectsInteraction> _causalityChain = new LinkedList<STObjectsInteraction>();

    public int Count => _causalityChain.Count;

    public override bool Equals(object obj) => Equals(obj as CausalityChain);

    public override int GetHashCode() => _causalityChain.GetHashCode();

    public IEnumerator<STObjectsInteraction> GetEnumerator() => _causalityChain.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool Equals(CausalityChain other)
    {
        if (other is null)
        {
            return false;
        }
        else
        {
            return _causalityChain.SequenceEqual(other._causalityChain);
        }
    }

    public void Add(STObjectsInteraction interaction)
    {
        if (interaction is null)
        {
            throw new System.ArgumentNullException(nameof(interaction));
        }
        else
        {
            _causalityChain.AddLast(interaction);
        }
    }
}