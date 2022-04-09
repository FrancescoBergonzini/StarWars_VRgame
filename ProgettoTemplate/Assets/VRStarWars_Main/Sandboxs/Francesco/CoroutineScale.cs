using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineScale : MonoBehaviour
{

    [SerializeField] Transform obj;
    float timeForDissolve = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _DissolveEffect(obj);
    }

    void _DissolveEffect(Transform scale)
    {
        IEnumerator _dissolveEffect()
        {
            Debug.Log("Rimpicciolisco");

            var random = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(timeForDissolve + random);

            Vector3 originalScale = scale.localScale;
            Vector3 destinationScale = new Vector3(0.3f, 0.3f, 0.3f);

            float currentTime = 0.0f;

            do
            {
                scale.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / timeForDissolve);
                currentTime += Time.deltaTime;
                yield return null;
            } 
            while (currentTime <= timeForDissolve);

            yield return new WaitForSeconds(1);
            Destroy(gameObject);

        }

        StartCoroutine(_dissolveEffect());
    }
}
