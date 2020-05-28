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
    
    void Start()
    {
        //使用全局IoC容器对脚本进行注入
        G_InjectionContainer.Inject(this);
        //给消息容器添加事件订阅
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            localsignalContainer.AddListener<SignalA>(OnReceiveSignalA);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            localsignalContainer.RemoveListener<SignalA>(OnReceiveSignalA);
        }
    }

    void OnReceiveSignalA(SignalA a)
    {
        Pool.Spawn(PoolPrefab);
    }

}



