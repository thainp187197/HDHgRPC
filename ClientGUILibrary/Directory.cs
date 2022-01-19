using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientGUILibrary
{
    public class Directory
    {
        public string Path { get; init; }
        public string[] DirectoryNames { get; init; }
        public string[] FileNames { get; init; }

        public Directory(string path, RepeatedField<string> dirNames, RepeatedField<string> fileNames)
        {
            Path = path;
            DirectoryNames = dirNames.ToArray();
            FileNames = fileNames.ToArray();
        }
    }
}
