using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListWPF.Models;

namespace ToDoListWPF.Services
{
    public class FileIOService
    {
        private readonly string Path;

        public FileIOService(string path)
        {
            Path = path;
        }

        public BindingList<ToDoModel> LoadData()
        {
            var fileExists = File.Exists(Path);
            if (!fileExists)
            {
                File.CreateText(Path).Dispose();

                return new BindingList<ToDoModel>()
                {
                    new ToDoModel()
                    {
                        IsDone = false,
                        CreateDate = DateTime.Now,
                        Text = "Empty",
                    }
                };
            }
            using (var reader = File.OpenText(Path))
            { 
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
            }
        }

        public void SaveData(object toDoDataList)
        {
            using (StreamWriter writer = File.CreateText(Path))
            {
                string output = JsonConvert.SerializeObject(toDoDataList);
                writer.Write(output);
            }
        }
    }
}
