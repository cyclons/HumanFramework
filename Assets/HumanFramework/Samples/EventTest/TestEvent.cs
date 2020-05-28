using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFramework;
using HumanFramework.Framework;

public class TestEvent : MonoBehaviour
{

    public ISignalContainer LocalSignalContainer = GlobalModule.GlobalSignalContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LocalSignalContainer.AddListener<SignalA>(GetSignalA);
            LocalSignalContainer.AddListener<SignalB>(GetSignalB);
        }
        if (Input.GetMouseButtonDown(1))
        {
            LocalSignalContainer.RemoveListener<SignalA>(GetSignalA);
            LocalSignalContainer.RemoveListener<SignalB>(GetSignalB);
            
            //error
            //LocalSignalContainer.RemoveListener<SignalC>(GetSignalC);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            LocalSignalContainer.DispatchSignal(new SignalA() { msg = "A msg broadcast" });
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            LocalSignalContainer.DispatchSignal(new SignalB() { msg = "B msg broadcast" });
            
            //error
            //LocalSignalContainer.DispatchSignal(new SignalC() { msg = "C msg broadcast" });
        }
    }

    void GetSignalA(SignalA signal)
    {
        Debug.Log(signal.msg);
    }
    void GetSignalB(SignalB signal)
    {
        Debug.Log(signal.msg);
    }
    void GetSignalC(SignalC signal)
    {
        Debug.Log(signal.msg);
    }
}

public class SignalA : ISignal
{
    public string msg;
}

public class SignalB : ISignal
{
    public string msg;
}

public class SignalC
{
    public string msg;
}
