using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header ("Stats")]
    public float speed = 10f;

    private Transform target;
    private int wavePointsIndex = 0;

    private void Start()
    {
        target = WayPoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) <= 0.2f)
        {
            GetNextWave();
        }
    }

    void GetNextWave()
    {
        if (wavePointsIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavePointsIndex++;
        target = WayPoints.points[wavePointsIndex];
    }
}
