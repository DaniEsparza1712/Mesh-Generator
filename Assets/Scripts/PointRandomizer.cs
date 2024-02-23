using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position +
                             Random.Range(-1.5f, 1.5f) * Vector3.right +
                             Random.Range(-1.5f, 1.5f) * Vector3.up +
                             Random.Range(-1.5f, 1.5f) * Vector3.forward;
    }
}
