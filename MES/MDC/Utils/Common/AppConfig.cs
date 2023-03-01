using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace Utils
{
    public class AppConfig
    {
        #region 私有变量
        private Configuration config;   // 配置文件类
        //private string _path;   // 配置文件路径
        #endregion

        #region 属性
        /// <summary>
        /// 配置文件是否存在
        /// </summary>
        public bool IsExisted
        {
            get
            {
                // 刷新配置文件
                RefreshConfig();

                if (config.HasFile)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        ///// <summary>
        ///// 获取或设置配置文件路径
        ///// </summary>
        //public string Path
        //{
        //    get
        //    {
        //        return _path;
        //    }
        //    set
        //    {
        //        if (Directory.Exists(value))
        //        {
        //            _path = value;
        //        }
        //    }
        //}
        #endregion 属性

        #region 构造函数
        /// <summary>
        /// 构造函数（默认）
        /// </summary>
        public AppConfig()
        {
            //_path = null;
        }

        ///// <summary>
        ///// 构造函数（指定配置文件路径）
        ///// </summary>
        //public AppConfig(string Path)
        //{
        //    _path = Path;

        //}
        #endregion

        #region 方法
        /// <summary>
        /// 刷新配置文件
        /// </summary>
        private void RefreshConfig()
        {
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //string path = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                //string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "MDC.exe.config";
                //string path = System.Windows.Forms.Application.ExecutablePath + ".config";
                //config = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap() { ExeConfigFilename = path }, ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                LogHelper.Error("", ex);
                // 弹框提示
                new FailMessage("未能读取程序配置文件", 1000).ShowDialog();
            }
        }

        /// <summary>
        /// 初始化程序配置文件
        /// </summary>
        public void InitConfig(KeyValueConfigurationCollection ConfigCollection)
        {
            // 刷新配置文件
            RefreshConfig();

            config.AppSettings.Settings.Clear();
            foreach (KeyValueConfigurationElement item in ConfigCollection)
            {
                config.AppSettings.Settings.Add(item);
            }
            config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 初始化程序配置文件
        /// </summary>
        public void InitConfig(KeyValueConfigurationElement[] array)
        {
            // 刷新配置文件
            RefreshConfig();

            config.AppSettings.Settings.Clear();
            foreach (KeyValueConfigurationElement item in array)
            {
                config.AppSettings.Settings.Add(item);
            }
            config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 初始化程序配置文件
        /// </summary>
        public void InitConfig(Dictionary<string, string> ConfigDictionary)
        {
            // 刷新配置文件
            RefreshConfig();

            config.AppSettings.Settings.Clear();
            foreach (KeyValuePair<string, string> item in ConfigDictionary)
            {
                config.AppSettings.Settings.Add(item.Key, item.Value);
            }
            config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 清空程序配置文件
        /// </summary>
        public void Clear()
        {
            // 刷新配置文件
            RefreshConfig();

            config.AppSettings.Settings.Clear();
            config.Save(ConfigurationSaveMode.Full);
        }

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetConfigString(string key)
        {
            // 刷新配置文件
            RefreshConfig();

            string strReturn = null;
            if (config.AppSettings.Settings[key] != null)
            {
                strReturn = config.AppSettings.Settings[key].Value;
            }
            return strReturn;
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetConfig(string key, string value)
        {
            // 刷新配置文件
            RefreshConfig();

            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }
            config.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        /// 设置多个配置参数
        /// </summary>
        /// <param name="ConfigList">配置参数列表Dictionary</param>
        public void SetConfig(Dictionary<string, string> ConfigList)
        {
            // 刷新配置文件
            RefreshConfig();

            foreach (KeyValuePair<string, string> cfg in ConfigList)
            {
                if (config.AppSettings.Settings[cfg.Key] != null)
                {
                    config.AppSettings.Settings[cfg.Key].Value = cfg.Value;
                }
                else
                {
                    config.AppSettings.Settings.Add(cfg.Key, cfg.Value);
                }
            }
            config.Save(ConfigurationSaveMode.Modified);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">配置参数名</param>
        /// <returns>删除成功</returns>
        public bool Remove(string key)
        {
            // 刷新配置文件
            RefreshConfig();

            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings.Remove(key);
                config.Save(ConfigurationSaveMode.Modified);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否包含配置参数
        /// </summary>
        /// <param name="Key">配置参数名</param>
        /// <returns>包含配置参数</returns>
        public bool ContainsKey(string Key)
        {
            // 刷新配置文件
            RefreshConfig();

            if (config.AppSettings.Settings[Key] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion 方法
    }
}
