using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake (float duration, float magnitude)
    {
            Vector3 originalPos = transform.localPosition;
        Debug.Log(originalPos);

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = originalPos.x + Random.Range(-0.1f, 0.1f) * magnitude;
            float z = originalPos.z + Random.Range(-0.1f, 0.1f) * magnitude;

            transform.localPosition = new Vector3(x, originalPos.y, z);

            elapsed += Time.deltaTime;

            yield return null;
            Debug.Log(originalPos);
        }

        transform.localPosition = originalPos;
        Debug.Log(originalPos);
    }
}
