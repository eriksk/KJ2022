using System;
using System.Collections;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public GameObject ModelPrefab;
    public ConsumableState State;

    void Start()
    {
        State = ConsumableState.Spawning;
        var model = Instantiate(ModelPrefab);
        model.layer = gameObject.layer;

        model.transform.SetParent(transform);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0, 360), 0f);
        model.transform.localScale = Vector3.zero;

        var filter = model.GetComponent<MeshFilter>();
        model.AddComponent<MeshCollider>().sharedMesh = filter.sharedMesh;

        StartCoroutine(FadeIn(model));
    }

    public bool Consume()
    {
        if(State != ConsumableState.Consumable) return false;

        State = ConsumableState.Consumed;
        StartCoroutine(FadeOutAndDestroy());
        return true;
    }

    private IEnumerator FadeOutAndDestroy()
    {
        var current = 0f;
        var duration = 0.3f;

        while (current < duration)
        {
            current += Time.deltaTime;
            var progress = Mathf.Clamp01(Mathf.SmoothStep(0f, 1f, current / duration));
            transform.localScale = Vector3.one * (1f - progress);
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator FadeIn(GameObject model)
    {
        var current = 0f;
        var duration = 1f;

        while (current < duration)
        {
            current += Time.deltaTime;
            var progress = Mathf.Clamp01(Mathf.SmoothStep(0f, 1f, current / duration));
            model.transform.localScale = Vector3.one * progress;
            yield return null;
        }

        State = ConsumableState.Consumable;
    }
}

public enum ConsumableState
{
    Spawning,
    Consumable,
    Consumed
}