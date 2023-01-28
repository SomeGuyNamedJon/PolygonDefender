using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinShape : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100);
        transform.Rotate(Vector3.forward * Time.deltaTime * 100);
    }
}
