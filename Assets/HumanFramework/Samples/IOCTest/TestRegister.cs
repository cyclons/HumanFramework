using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanFramework;
using HumanFramework.Event;

public class TestRegister : MonoBehaviour
{
    void Awake()
    {
        //在全局IoC容器中注册一个消息中心，并标记为local
        G_InjectionContainer.RegisterInstance<ISignalContainer>(new SignalContainer(), "local");
    }

}
