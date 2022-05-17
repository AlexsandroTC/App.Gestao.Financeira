using App.Gestao.Financeira.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App.Gestao.Financeira.Configuration
{
    public class AndroidPlataformData : IPlataformData
    {
        public string FolderPath(string file)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(path, file);
        }
    }
}
