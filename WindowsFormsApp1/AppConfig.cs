using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WindowsFormsApp1
{
    /// <summary>
    /// 持久化配置：复制模板、上次筛选状态、列宽。
    /// 存储于 exe 旁 ui_config.ini，以 key=value 方式读写。
    /// </summary>
    public class AppConfig
    {
        private readonly string _path;
        private readonly Dictionary<string, string> _kv = new Dictionary<string, string>();

        public AppConfig(string path)
        {
            _path = path;
            Load();
        }

        public string Template { get => Get("template", ""); set => _kv["template"] = value; }
        public string Category { get => Get("category", BoltEngine.AllCat); set => _kv["category"] = value; }
        public string Cls { get => Get("class", BoltEngine.AllCls); set => _kv["class"] = value; }
        public string Face { get => Get("face", "全部"); set => _kv["face"] = value; }
        public string Nps { get => Get("nps", BoltEngine.AllNps); set => _kv["nps"] = value; }
        public string ColWidths { get => Get("colwidths", ""); set => _kv["colwidths"] = value; }

        public string Get(string key, string def = "")
            => _kv.TryGetValue(key, out var v) ? v : def;

        public void Load()
        {
            _kv.Clear();
            try
            {
                if (File.Exists(_path))
                    foreach (var line in File.ReadAllLines(_path))
                    {
                        int eq = line.IndexOf('=');
                        if (eq > 0)
                            _kv[line.Substring(0, eq).Trim()] = line.Substring(eq + 1);
                    }
            }
            catch (Exception) { /* 忽略读失败 */ }
        }

        public void Save()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var kv in _kv)
                    sb.Append(kv.Key).Append('=').Append(kv.Value).AppendLine();
                File.WriteAllText(_path, sb.ToString());
            }
            catch (Exception) { /* 忽略写失败 */ }
        }
    }
}
