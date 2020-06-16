using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanFramework.IOC;

namespace HumanFramework
{
    public class G_InjectionContainer
    {
        private static IInjectionContainer mContainer = new InjectionContainer();

        /// <summary>
        /// 为对象注入
        /// </summary>
        /// <param name="obj">被注入的对象</param>
        public static void Inject(object obj)
        {
            mContainer.Inject(obj);
        }

        /// <summary>
        ///  注册某一类的单例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="instance">注册实例</param>
        /// <param name="nameID">实例ID</param>
        public static void RegisterInstance<T>(T instance, string nameID = null)
        {
            mContainer.RegisterInstance<T>(instance, nameID);
        }

        /// <summary>
        /// 取消注册某一类的单例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="nameID">实例ID</param>
        public static void UnRegisterInstance<T>(string nameID = null)
        {
            mContainer.UnRegisterInstance<T>(nameID);
        }

        /// <summary>
        /// 注册某一类型的构造方式
        /// </summary>
        /// <typeparam name="TBase">类型的基类，通常为接口或虚基类</typeparam>
        /// <typeparam name="TChild">类型的具体实现类</typeparam>
        /// <param name="nameID">实例构造ID</param>
        /// <param name="args">构造函数参数</param>
        public static void RegisterConstruction<TBase, TChild>(string nameID = null, params object[] args) where TChild : TBase
        {
            mContainer.RegisterConstruction<TBase, TChild>(nameID, args);
        }

        /// <summary>
        /// 主动获取注入池中的单例对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameID"></param>
        /// <returns></returns>
        public static T ResolveInstance<T>(string nameID = null)
        {
            return mContainer.ResolveInstance<T>(nameID);
        }

        /// <summary>
        /// 主动获取注入池中的构造对象
        /// </summary>
        /// <typeparam name="TBase">对象基类</typeparam>
        /// <param name="nameID">对象ID</param>
        /// <returns></returns>
        public static TBase ResolveConstruction<TBase>(string nameID = null)
        {
            return mContainer.ResolveInstance<TBase>(nameID);
        }

    }
}
