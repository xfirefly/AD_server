using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Common
{
    public abstract class CustomIniFile
    {
        public CustomIniFile(string AFileName)
        {
            FFileName = AFileName;
        }
        private string FFileName;
        public string FileName
        {
            get { return FFileName; }
        }
        public virtual bool SectionExists(string Section)
        {
            List<string> vStrings = new List<string>();
            ReadSections(vStrings);
            return vStrings.Contains(Section);
        }
        public virtual bool ValueExists(string Section, string Ident)
        {
            List<string> vStrings = new List<string>();
            ReadSection(Section, vStrings);
            return vStrings.Contains(Ident);
        }
        public abstract string ReadString(string Section, string Ident, string Default);
        public abstract bool WriteString(string Section, string Ident, string Value);
        public abstract bool ReadSectionValues(string Section, List<string> Strings);
        public abstract bool ReadSection(string Section, List<string> Strings);
        public abstract bool ReadSections(List<string> Strings);
        public abstract bool EraseSection(string Section);
        public abstract bool DeleteKey(string Section, string Ident);
        public abstract bool UpdateFile();
    }
    /// <summary>
    /// 存储本地INI文件的类。
    /// </summary>
    public class IniFile : CustomIniFile
    {
        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileString(
            string lpAppName,    // points to section name 
            string lpKeyName,    // points to key name 
            string lpDefault,    // points to default string 
            byte[] lpReturnedString,    // points to destination buffer 
            uint nSize,    // size of destination buffer 
            string lpFileName     // points to initialization filename 
        );

        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(
            string lpAppName,    // pointer to section name 
            string lpKeyName,    // pointer to key name 
            string lpString,    // pointer to string to add 
            string lpFileName     // pointer to initialization filename 
        );

        /// <summary>
        /// 构造IniFile实例。
        /// <param name="AFileName">指定文件名</param>
        /// </summary>
        public IniFile(string AFileName)
            : base(AFileName)
        {
        }

        /// <summary>
        /// 析够IniFile实例。
        /// </summary>
        ~IniFile()
        {
            UpdateFile();
        }

        /// <summary>
        /// 读取字符串值。
        /// <param name="Ident">指定变量标识。</param>
        /// <param name="Section">指定所在区域。</param>
        /// <param name="Default">指定默认值。</param>
        /// <returns>返回读取的字符串。如果读取失败则返回该值。</returns>
        /// </summary>
        public override string ReadString(string Section, string Ident, string Default)
        {
            byte[] vBuffer = new byte[2048];
            uint vCount = GetPrivateProfileString(Section,
                Ident, Default, vBuffer, (uint)vBuffer.Length, FileName);
            return Encoding.Default.GetString(vBuffer, 0, (int)vCount);
        }
        /// <summary>
        /// 写入字符串值。
        /// </summary>
        /// <param name="Section">指定所在区域。</param>
        /// <param name="Ident">指定变量标识。</param>
        /// <param name="Value">所要写入的变量值。</param>
        /// <returns>返回写入是否成功。</returns>
        public override bool WriteString(string Section, string Ident, string Value)
        {
            return WritePrivateProfileString(Section, Ident, Value, FileName);
        }

        /// <summary>
        /// 获得区域的完整文本。(变量名=值格式)。
        /// </summary>
        /// <param name="Section">指定区域标识。</param>
        /// <param name="Strings">输出处理结果。</param>
        /// <returns>返回读取是否成功。</returns>
        public override bool ReadSectionValues(string Section, List<string> Strings)
        {
            List<string> vIdentList = new List<string>();
            if (!ReadSection(Section, vIdentList)) return false;
            foreach (string vIdent in vIdentList)
                Strings.Add(string.Format("{0}={1}", vIdent, ReadString(Section, vIdent, "")));
            return true;
        }

        /// <summary>
        /// 读取区域变量名列表。
        /// </summary>
        /// <param name="Section">指定区域名。</param>
        /// <param name="Strings">指定输出列表。</param>
        /// <returns>返回获取是否成功。</returns>
        public override bool ReadSection(string Section, List<string> Strings)
        {
            byte[] vBuffer = new byte[16384];
            uint vLength = GetPrivateProfileString(Section, null, null, vBuffer,
                (uint)vBuffer.Length, FileName);
            int j = 0;
            for (int i = 0; i < vLength; i++)
                if (vBuffer[i] == 0)
                {
                    Strings.Add(Encoding.Default.GetString(vBuffer, j, i - j));
                    j = i + 1;
                }
            return true;
        }

        /// <summary>
        /// 读取区域名列表。
        /// </summary>
        /// <param name="Strings">指定输出列表。</param>
        /// <returns></returns>
        public override bool ReadSections(List<string> Strings)
        {
            byte[] vBuffer = new byte[16384];
            uint vLength = GetPrivateProfileString(null, null, null, vBuffer,
                (uint)vBuffer.Length, FileName);
            int j = 0;
            for (int i = 0; i < vLength; i++)
                if (vBuffer[i] == 0)
                {
                    Strings.Add(Encoding.Default.GetString(vBuffer, j, i - j));
                    j = i + 1;
                }
            return true;
        }

        /// <summary>
        /// 删除指定区域。
        /// </summary>
        /// <param name="Section">指定区域名。</param>
        /// <returns>返回删除是否成功。</returns>
        public override bool EraseSection(string Section)
        {
            return WritePrivateProfileString(Section, null, null, FileName);
        }

        /// <summary>
        /// 删除指定变量。
        /// </summary>
        /// <param name="Section">变量所在区域。</param>
        /// <param name="Ident">变量标识。</param>
        /// <returns>返回删除是否成功。</returns>
        public override bool DeleteKey(string Section, string Ident)
        {
            return WritePrivateProfileString(Section, Ident, null, FileName);
        }

        /// <summary>
        /// 更新文件。
        /// </summary>
        /// <returns>返回更新是否成功。</returns>
        public override bool UpdateFile()
        {
            return WritePrivateProfileString(null, null, null, FileName);
        }
    }
}
