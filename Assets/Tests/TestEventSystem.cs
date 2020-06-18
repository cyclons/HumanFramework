using System.Collections;
using System.Collections.Generic;
using HumanFramework;
using HumanFramework.Event;
using HumanFramework.ObjectPool;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestEventSystem
    {
        class TestSignal : ISignal
        {
            public string Msg;
        }

        void OnRevTestSignal(TestSignal signal)
        {
            Debug.Log(signal.Msg);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void TestEventSystemSimplePasses()
        {
            // Use the Assert class to test conditions
            ISignalContainer signalContainer = new SignalContainer();

            signalContainer.AddListener<TestSignal>(OnRevTestSignal);

            signalContainer.DispatchSignal(new TestSignal { Msg = "test" });

            LogAssert.Expect(LogType.Log, "test");

        
        }

        [Test]
        public void TestObjPool()
        {
            GameObject poolPrefab = new GameObject();
            GameObject go = Pool.Spawn(poolPrefab);
            go.GetComponent<IPoolable>().RecycleToPool(go);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestEventSystemWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
