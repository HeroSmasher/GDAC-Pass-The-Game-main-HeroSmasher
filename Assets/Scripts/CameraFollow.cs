using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float LerpSpeed;

    Vector3 _offset;
    void Start()
    {
        if (!Target)
        {
            Debug.LogError("Must set a target transform");
            enabled = false;
            return;
        }
        _offset = transform.position - Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + _offset, LerpSpeed * Time.deltaTime);
    }
}
