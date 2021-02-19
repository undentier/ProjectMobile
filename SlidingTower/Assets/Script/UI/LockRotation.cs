using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public Vector3 target;
    void Update()
    {
        transform.eulerAngles = target;
    }
}
