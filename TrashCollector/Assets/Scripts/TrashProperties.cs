using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashProperties : MonoBehaviour
{
    public static TrashProperties instance;
    private void Awake()
    {
        instance = this;
    }

    public int worth { get; set; }
}
