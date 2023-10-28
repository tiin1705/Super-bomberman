using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftObject : MonoBehaviour
{
    // Start is called before the first frame update
    public float destructionTime = 1f;
    private void Start()
    {
        Destroy(gameObject,destructionTime);
    }
}
