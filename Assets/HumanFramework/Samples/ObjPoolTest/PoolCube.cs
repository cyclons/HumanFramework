using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFramework;

public class PoolCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Pool.Recycle(this.gameObject);
        }
    }
}
