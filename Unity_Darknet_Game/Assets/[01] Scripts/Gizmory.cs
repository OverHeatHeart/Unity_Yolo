using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmory : MonoBehaviour
{
    public float rad = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, rad);
    }
}
