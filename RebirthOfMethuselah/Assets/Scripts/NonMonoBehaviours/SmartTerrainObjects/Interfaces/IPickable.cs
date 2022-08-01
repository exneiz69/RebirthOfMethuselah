using UnityEngine;

public interface IPickable
{
    public bool IsPicked { get; }

    public void Pick(Transform newParent, int newLayer);

    public void Drop(Vector3 direcion, float force);
}