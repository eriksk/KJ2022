using System;
using System.Collections.Generic;
using UnityEngine;

public class CargoContainer : MonoBehaviour
{
    public GameObject LogPrefab;

    private List<Transform> _logs = new List<Transform>();

    public void AddLog()
    {
        AddLogModel();
    }

    private void AddLogModel()
    {
        var log = Instantiate(LogPrefab);
        log.transform.SetParent(transform);
        
        log.transform.localRotation = Quaternion.identity;
        log.transform.localScale = Vector3.one;

        var col = _logs.Count % 4;
        var row = _logs.Count / 4;

        var offset = new Vector3(
            -3f * 0.2f,
            0,
            0f
        ) * 0.5f;

        var position = offset + new Vector3(
            col * 0.2f,
            row * 0.2f,
            0f
        );

        log.transform.localPosition = position;

        _logs.Add(log.transform);
    }
}
