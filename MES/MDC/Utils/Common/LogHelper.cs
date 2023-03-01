using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utils
{
    public class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");

        public static object locker = new object();
        private static string startuppath = AssemblyPath;

        /// <summary>  
        /// 获取Assembly的运行路径  
        /// </summary>  
        ///<returns></returns>  
        private static string AssemblyPath
        {
            get
            {
                string _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);    // 8是file:// 的长度  
                string[] arrSection = _CodeBase.Split(new char[] { '/' });
                string _FolderPath = "";
                for (int i = 0; i < arrSection.Length - 1; i++)
                {
                    _FolderPath += arrSection[i] + "/";
                }
                return _FolderPath;
            }
        }

        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="LogText">日志文本</param>
        /// <param name="path">日志文件存放路径</param>
        private static void WriteLogInfo(string LogText, string path)
        {
            try
            {

                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        sw.WriteLine(LogText);
                    }
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex);
            }
        }

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 添加信息日志
        /// </summary>
        /// <param name="info"></param>
        public static void Info(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        /// <summary>
        /// 添加错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void Error(string info, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info, ex);
            }
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="NoteText"></param>
        public static void Note(string NoteText)
        {
            try
            {
                lock (locker)
                {
                    string path = startuppath + "\\Log\\LogNote";
                    if (!Directory.Exists(path))
                    {
                        //创建日志文件夹
                        Directory.CreateDirectory(path);
                    }
                    //每天都创建一个单独的日志文件[*.log],每天的信息都在这一个文件里。方便查找
                    path += "\\" + DateTime.Now.ToString("yyyyMMdd") + "_Note.log";
                    WriteLogInfo(NoteText, path);
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex);
            }
        }


    }
}
