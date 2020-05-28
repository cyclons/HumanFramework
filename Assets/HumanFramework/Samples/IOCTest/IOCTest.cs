using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using HumanFramework.Event;
using HumanFramework;

public class IOCTest : MonoBehaviour
{
    [InjectInstance("local")]
    private ISignalContainer localsignalContainer;

    public GameObject PoolPrefab;

    private void Awake()
    {
        G_InjectionContainer.RegisterInstance<ISignalContainer>(new SignalContainer(),"local");
    }
    
    void Start()
    {
        G_InjectionContainer.Inject(this);
        localsignalContainer.AddListener<SignalA>(OnReceiveSignalA);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            localsignalContainer.DispatchSignal(new SignalA { msg = "Test A Success!" });
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Pool.Clear();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            localsignalContainer.RemoveListener<SignalA>(OnReceiveSignalA);
        }
    }

    void OnReceiveSignalA(SignalA a)
    {
        Pool.Spawn(PoolPrefab);
    }

}



