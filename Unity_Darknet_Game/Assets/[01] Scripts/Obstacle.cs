using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 desti = Vector3.zero;
    public Vector3 origin = Vector3.zero;
    public float spd = 20f;

    // Update is called once per frame
    protected void Update()
    {
        Vector3 dir = desti - transform.position;
        transform.position += dir.normalized * Time.deltaTime * spd;
        if (Vector3.Distance(transform.position, desti) < 0.5f)
        {
            Reset();
        }
    }
    protected void Reset()
    {
        transform.position = origin;
    }
}
