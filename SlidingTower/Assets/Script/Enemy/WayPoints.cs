using UnityEngine;

public class WayPoints : MonoBehaviour
{
    public static Transform points;

    private void Awake()
    {
        points = gameObject.transform;
    }
}
