using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HumanFramework.IOC
{

    public interface IInjectionContainer
    {
        /// <summary>
        ///  注册某一类的单例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="instance">注册实例</param>
        /// <param name="nameID">实例ID</param>
        void RegisterInstance<T>(T instance, string nameID = null);
        
        /// <summary>
        /// 取消注册某一类的单例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="nameID">实例ID</param>
        void UnRegisterInstance<T>(string nameID = null);

        /// <summary>
        /// 注册某一类型的构造方式
        /// </summary>
        /// <typeparam name="TBase">类型的基类，通常为接口或虚基类</typeparam>
        /// <typeparam name="TChild">类型的具体实现类</typeparam>
        /// <param name="nameID">实例构造ID</param>
        /// <param name="args">构造函数参数</param>
        void RegisterConstruction<TBase,TChild>(string nameID = null, params object[] args) where TChild : TBase;

        /// <summary>
        /// 为对象注入
        /// </summary>
        /// <param name="obj">被注入的对象</param>
        void Inject(object obj);

        /// <summary>
        /// 主动获取注入池中的单例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameID"></param>
        /// <returns></returns> 
        T ResolveInstance<T>(string nameID = null);

        /// <summary>
        /// 主动获取注入池中的构造对象
        /// </summary>
        /// <typeparam name="TBase">对象基类</typeparam>
        /// <param name="nameID">对象ID</param>
        /// <returns></returns>
        TBase ResolveConstruction<TBase>(string nameID = null);
    }

    public class InjectionContainer : IInjectionContainer
    {

        private TypeInstanceCollection _typeInstanceDict;

        public TypeInstanceCollection TypeInstanceDict
        {
            get
            {
                return _typeInstanceDict ?? (_typeInstanceDict = new TypeInstanceCollection());
            }
            set => _typeInstanceDict = value;
        }

        private TypeConstructionCollection _typeConstructionDict;

        public TypeConstructionCollection TypeConstructionDict { get => _typeConstructionDict ?? (_typeConstructionDict = new TypeConstructionCollection()); set => _typeConstructionDict = value; }


        public void Inject(object obj)
        {
            if (obj == null) return;

            //注入fields部分
            var fieldMembers = obj.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var memberInfo in fieldMembers)
            {
                var memberAttr = memberInfo.GetCustomAttributes(typeof(InjectAttribute), true).FirstOrDefault();
                if (memberAttr != null)
                {
                    if (memberAttr.GetType() == typeof(InjectInstanceAttribute))
                    {
                            var field = memberInfo as FieldInfo;
                            field.SetValue(obj, ResolveInstance(field.FieldType, (memberAttr as InjectInstanceAttribute).NameID));
                    }
                    else if (memberAttr.GetType() == typeof(InjectConstructionAttribute))
                    {
                        var field = memberInfo as FieldInfo;
                        field.SetValue(obj, ResolveConstruction(field.FieldType, (memberAttr as InjectConstructionAttribute).NameID));
                    }
                }
            }

            //注入properties部分
            var propertyMembers = obj.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var memberInfo in propertyMembers)
            {
                var memberAttr = memberInfo.GetCustomAttributes(typeof(InjectAttribute), true).FirstOrDefault();
                if (memberAttr != null)
                {
                    if (memberAttr.GetType() == typeof(InjectInstanceAttribute))
                    {
                        var propertyInfo = memberInfo as PropertyInfo;
                        propertyInfo.SetValue(obj, ResolveInstance(propertyInfo.PropertyType, (memberAttr as InjectInstanceAttribute).NameID));
                    }
                    else if (memberAttr.GetType() == typeof(InjectConstructionAttribute))
                    {
                        var propertyInfo = memberInfo as PropertyInfo;
                        propertyInfo.SetValue(obj, ResolveConstruction(propertyInfo.PropertyType, (memberAttr as InjectConstructionAttribute).NameID));
                    }
                }
            }

        }

        public void RegisterInstance<T>(T instance, string nameID = null)
        {
            TypeInstanceDict[typeof(T), nameID] = instance;
        }

        public void UnRegisterInstance<T>(string nameID = null)
        {
            TypeInstanceDict.Remove(new Tuple<Type, string>(typeof(T), nameID));
        }

        private object ResolveInstance(Type instanceType, string nameID = null)
        {
            return TypeInstanceDict[instanceType, nameID];
        } 

        public T ResolveInstance<T>(string nameID = null)
        {
            return (T)ResolveInstance(typeof(T), nameID);
        }

        public void RegisterConstruction<TBase, TChild>(string nameID = null, params object[] args) where TChild : TBase
        {
            TypeConstructionDict[typeof(TBase), nameID] = new Tuple<Type, object[]>(typeof(TChild), args);
        }

        public TBase ResolveConstruction<TBase>(string nameID = null)
        {
            return (TBase)ResolveConstruction(typeof(TBase), nameID);
        }

        private object ResolveConstruction(Type TBase, string nameID = null)
        {
            Tuple<Type, object[]> constructInfo;
            if (TypeConstructionDict.TryGetValue(new Tuple<Type, string>(TBase, nameID), out constructInfo))
            {
                return CreateInstance(constructInfo.Item1, constructInfo.Item2);
            }
            return default;
        }

        object CreateInstance(Type objType,object[] objArgs)
        {
            return Activator.CreateInstance(objType, objArgs);
        }


    }

    // http://stackoverflow.com/questions/1171812/multi-key-dictionary-in-c
    public class Tuple<T1, T2> //FUCKING Unity: struct is not supported in Mono
    {
        public readonly T1 Item1;
        public readonly T2 Item2;

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(Object obj)
        {
            Tuple<T1, T2> p = obj as Tuple<T1, T2>;
            if (obj == null) return false;

            if (Item1 == null)
            {
                if (p.Item1 != null) return false;
            }
            else
            {
                if (p.Item1 == null || !Item1.Equals(p.Item1)) return false;
            }

            if (Item2 == null)
            {
                if (p.Item2 != null) return false;
            }
            else
            {
                if (p.Item2 == null || !Item2.Equals(p.Item2)) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            if (Item1 != null)
                hash ^= Item1.GetHashCode();
            if (Item2 != null)
                hash ^= Item2.GetHashCode();
            return hash;
        }
    }

    public class TypeInstanceCollection : Dictionary<Tuple<Type, string>, object>
    {

        public object this[Type from, string name = null]
        {
            get
            {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                object mapping = null;
                if (this.TryGetValue(key, out mapping))
                {
                    return mapping;
                }

                return null;
            }
            set
            {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                this[key] = value;
            }
        }
    }

    public class TypeConstructionCollection : Dictionary<Tuple<Type, string>, Tuple<Type, object[]>>
    {
        public Tuple<Type, object[]> this[Type from, string name = null]
        {
            get
            {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                Tuple<Type, object[]> mapping = null;
                if (this.TryGetValue(key, out mapping))
                {
                    return mapping;
                }

                return null;
            }
            set
            {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                this[key] = value;
            }
        }
    }
}
