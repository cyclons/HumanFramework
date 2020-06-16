using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFramework;

public class ObjPoolTest : MonoBehaviour
{
    public GameObject TestPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Pool.Spawn(TestPrefab);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Pool.Clear();
        }

    }
}
