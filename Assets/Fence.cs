using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    private void Start()
    {
        Hearth.Instance.SpawnFence(transform);
    }
}
