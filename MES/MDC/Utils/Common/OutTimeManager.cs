using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.Common
{
    /// <summary>
    /// 超时类,可设置执行方法超时时间
    /// 目前没有添加取消方法,如有需要,可添加取消功能
    /// </summary>
    public class OutTimeClass
    {
        #region delegate
        /// <summary>
        /// 无参数,无反回值方法
        /// </summary>
        public delegate void NotParamDelegate();
        /// <summary>
        /// 有参数,无反回值方法
        /// </summary>
        /// <param name="Params"></param>
        public delegate void SomeParamsDelegate(object Params);
        /// <summary>
        /// 无参数,带返回值方法
        /// </summary>
        /// <returns></returns>
        public delegate object NotParamReturnDelegate();
        /// <summary>
        /// 有参数,有反回值方法
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        public delegate object SomeParamsReturnDelegate(object Params);
        #endregion

        #region 超时方法
        /// <summary>
        /// 无参数,无反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTimeMilliseconds">超时时间(毫秒)</param>
        public static void OutTimeNotParam(NotParamDelegate Method, int OutTimeMilliseconds)
        {
            OutTimeNotParam(Method, TimeSpan.FromMilliseconds(OutTimeMilliseconds));
        }

        /// <summary>
        /// 无参数,无反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        public static void OutTimeNotParam(NotParamDelegate Method, TimeSpan OutTime)
        {
            OutTimeNotParam(Method, OutTime, null);
        }

        /// <summary>
        /// 无参数,无反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="cancelEvent">取消信号</param>
        public static void OutTimeNotParam(NotParamDelegate Method, TimeSpan OutTime, WaitHandle cancelEvent)
        {
            AutoResetEvent are = new AutoResetEvent(false);
            Thread t = new Thread(delegate() { Method(); are.Set(); });
            t.Start();
            Wait(t, OutTime, are, cancelEvent);
        }

        /// <summary>
        /// 有参数,无反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTimeMilliseconds">超时时间(毫秒)</param>
        /// <param name="Param">参数</param>
        public static void OutTimeSomeParem(SomeParamsDelegate Method, int OutTimeMilliseconds, object Param)
        {
            OutTimeSomeParem(Method, TimeSpan.FromMilliseconds(OutTimeMilliseconds), Param);
        }

        /// <summary>
        /// 有参数,无反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="Param">参数</param>
        public static void OutTimeSomeParem(SomeParamsDelegate Method, TimeSpan OutTime, object Param)
        {
            OutTimeSomeParem(Method, OutTime, Param, null);
        }

        /// <summary>
        /// 有参数,无反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="cancelEvent">取消信号</param>
        /// <param name="Params">参数</param>
        public static void OutTimeSomeParem(SomeParamsDelegate Method, TimeSpan OutTime, object Param, WaitHandle cancelEvent)
        {
            AutoResetEvent are = new AutoResetEvent(false);
            Thread t = new Thread(delegate() { Method(Param); are.Set(); });
            t.Start();
            Wait(t, OutTime, are, cancelEvent);
        }

        /// <summary>
        /// 没参数,有反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTimeMilliseconds">超时时间(毫秒)</param>
        /// <returns>反回object类型对象</returns>
        public static object OutTimeNotParamReturn(NotParamReturnDelegate Method, int OutTimeMilliseconds)
        {
            return OutTimeNotParamReturn(Method, TimeSpan.FromMilliseconds(OutTimeMilliseconds));
        }

        /// <summary>
        /// 没参数,有反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <returns>反回object类型对象</returns>
        public static object OutTimeNotParamReturn(NotParamReturnDelegate Method, TimeSpan OutTime)
        {
            return OutTimeNotParamReturn(Method, OutTime, null);
        }

        /// <summary>
        /// 没参数,有反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="cancelEvent">取消信号</param>
        /// <returns>反回object类型对象</returns>
        public static object OutTimeNotParamReturn(NotParamReturnDelegate Method, TimeSpan OutTime, WaitHandle cancelEvent)
        {
            object obj = null;
            AutoResetEvent are = new AutoResetEvent(false);
            Thread t = new Thread(delegate() { obj = Method(); are.Set(); });
            t.Start();
            Wait(t, OutTime, are, cancelEvent);
            return obj;
        }

        /// <summary>
        /// 有参数,有反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="Params">执行参数</param>
        /// <returns>反回一个object类型方法</returns>
        public static object OutTimeSomeParemReturn(SomeParamsReturnDelegate Method, int OutTimeMilliseconds, object Param)
        {
            return OutTimeSomeParemReturn(Method, TimeSpan.FromMilliseconds(OutTimeMilliseconds), Param);
        }

        /// <summary>
        /// 有参数,有反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="Params">执行参数</param>
        /// <returns>反回一个object类型方法</returns>
        public static object OutTimeSomeParemReturn(SomeParamsReturnDelegate Method, TimeSpan OutTime, object Param)
        {
            return OutTimeSomeParemReturn(Method, OutTime, Param, null);
        }

        /// <summary>
        /// 有参数,有反回值超时方法
        /// </summary>
        /// <param name="Method">执行方法</param>
        /// <param name="OutTime">超时时间</param>
        /// <param name="Params">执行参数</param>
        /// <param name="cancelEvent">取消信号</param>
        /// <returns>反回一个object类型方法</returns>
        public static object OutTimeSomeParemReturn(SomeParamsReturnDelegate Method, TimeSpan OutTime, object Param, WaitHandle cancelEvent)
        {
            object obj = null;
            AutoResetEvent are = new AutoResetEvent(false);
            Thread t = new Thread(delegate() { obj = Method(Param); are.Set(); });
            t.Start();
            Wait(t, OutTime, are, cancelEvent);
            return obj;
        }

        /// <summary>
        /// 等待方法执行完成,或超时
        /// </summary>
        /// <param name="t"></param>
        /// <param name="OutTime"></param>
        /// <param name="ares"></param>
        private static void Wait(Thread t, TimeSpan OutTime, WaitHandle are, WaitHandle cancelEvent)
        {
            WaitHandle[] ares;
            if (cancelEvent == null)
                ares = new WaitHandle[] { are };
            else
                ares = new WaitHandle[] { are, cancelEvent };
            int index = WaitHandle.WaitAny(ares, OutTime);
            if ((index != 0) && t.IsAlive)//如果不是执行完成的信号,并且,线程还在执行,那么,结束这个线程
            {
                t.Abort();
                t = null;
            }
        }
        #endregion
    }
}
