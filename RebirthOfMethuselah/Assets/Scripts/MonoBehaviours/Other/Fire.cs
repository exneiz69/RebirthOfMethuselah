using UnityEngine;

public class Fire : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.rotation != Quaternion.identity)
        {
            transform.rotation = Quaternion.identity;
        }    
    }
}
