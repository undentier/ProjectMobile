using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform endPoint;

    private void Awake()
    {
        endPoint = gameObject.transform;
    }
}
