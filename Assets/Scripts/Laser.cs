using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // FIXME, make these configurable
    [SerializeField] private float _speed = 12.0f; // FIXME, make configurable

    void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));
    }
}
