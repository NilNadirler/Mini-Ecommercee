﻿
using ETicaretAPI.Insfracture.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Insfracture.Services
{
    public class FileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

     

        async Task<String> FileReNameAsync(string path, string fileName, bool first = true)
        {

            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int indextNo1 = newFileName.IndexOf('-');
                    if (indextNo1 == -1)
                        newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extension}";
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indextNo1;
                            indextNo1 = newFileName.IndexOf("-", indextNo1 + 1);
                            if (indextNo1 == -1)
                            {
                                indextNo1 = lastIndex;
                                break;
                            }
                        }

                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indextNo1 + 1, indexNo2 - indextNo1 - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indextNo1 + 1, indexNo2 - indextNo1 - 1).Insert(indextNo1 + 1, _fileNo.ToString());
                        }
                        else
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(fileName)}-2{extension}";
                        }
                    }

                };
                if (File.Exists($"{path}\\{newFileName}"))
                        return await FileReNameAsync(path, newFileName, false);
                    else
                        return newFileName;

               

              
            });
            return newFileName;
        }


    }
}
