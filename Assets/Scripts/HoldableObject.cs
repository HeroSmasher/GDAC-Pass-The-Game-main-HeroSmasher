using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    public static readonly HashSet<HoldableObject> Objects = new HashSet<HoldableObject>();
    void Awake()
    {
        Objects.Add(this);
    }

    void OnDestroy()
    {
        Objects.Remove(this);
    }
}

