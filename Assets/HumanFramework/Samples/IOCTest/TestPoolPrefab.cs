using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFramework;

public class TestPoolPrefab : MonoBehaviour
{

    private void OnEnable()
    {
        transform.position = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
    }

    private void OnMouseDown()
    {
        Pool.Recycle(this.gameObject);
    }

}
