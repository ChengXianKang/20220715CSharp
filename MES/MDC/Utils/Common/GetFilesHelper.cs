using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Utils
{
    /// <summary>
    /// 获取不良图片列表帮助类
    /// </summary>
    public class GetFilesHelper
    {
        static List<string> files = new List<string>();
        /// <summary>
        /// 获取目录下图片文件列表（bmp&jpg）
        /// </summary>
        /// <param name="directoryInfo">目录</param>
        /// <returns></returns>
        public List<string> GetFiles(DirectoryInfo directoryInfo)
        {
            files.Clear();
            GetFileList(directoryInfo);
            return files;
        }

        private void GetFileList(DirectoryInfo _dir)
        {
            //获取指定文件夹下的所有文件
            FileInfo[] fis = _dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                string ext = Path.GetExtension(fis[i].FullName);
                if (ext == ".bmp" || ext == ".BMP" || ext == ".jpg" || ext == ".JPG")
                {
                    files.Add(fis[i].FullName);
                }
            }
            DirectoryInfo[] dis = _dir.GetDirectories();
            for (int j = 0; j < dis.Length; j++)
            {
                GetFileList(dis[j]);
            }
        }

        /// <summary>
        /// 文件列表（文件名不含扩展名， 文件完整路径）
        /// </summary>
        public static Dictionary<string, string> DicFiles = new Dictionary<string, string>();

        /// <summary>
        /// 获取目录下图片文件列表（bmp/jpg）
        /// </summary>
        /// <param name="directoryInfo">图片文件夹路径</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetImageDic(DirectoryInfo directoryInfo)
        {
            DicFiles.Clear();
            GetFileDic(directoryInfo);
            return DicFiles;
        }

        private static void GetFileDic(DirectoryInfo _dir)
        {
            //获取指定文件夹下的所有文件
            FileInfo[] fis = _dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                string ext = Path.GetExtension(fis[i].FullName);
                string key = Path.GetFileNameWithoutExtension(fis[i].FullName);
                if (ext == ".bmp" || ext == ".BMP" || ext == ".jpg" || ext == ".JPG")
                {
                    if (!DicFiles.ContainsKey(key))
                    {
                        DicFiles.Add(key, fis[i].FullName);
                    }
                }
            }
            DirectoryInfo[] dis = _dir.GetDirectories();
            for (int j = 0; j < dis.Length; j++)
            {
                GetFileDic(dis[j]);
            }
        }

    }
}
