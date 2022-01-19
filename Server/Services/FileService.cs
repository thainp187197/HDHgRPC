using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace gRPC.Server
{
    public class FileService : File.FileBase
    {
        private const string _rootPath = @"C:\hdh";

        public override Task<PathReply> ExplorePath(PathRequest request, ServerCallContext context)
        {
            PathReply reply = new();

            if (request.DirectoryName == @"\")
            {
                reply.Path = _rootPath;
            }
            else
            {
                request.Path = _rootPath + request.Path;
                string path = Path.GetFullPath(Path.Combine(request.Path, request.DirectoryName));
                if (Directory.Exists(path))
                    reply.Path = path;
                else if (System.IO.File.Exists(path))
                    reply.Path = Path.GetFullPath(request.Path);
                else
                    reply.Path = _rootPath;
            }

            //reply.DirectoryNames.Add(@"\");
            if ((_rootPath != reply.Path) && (_rootPath + @"\" != reply.Path))
                reply.DirectoryNames.Add("..");

            var directoryPaths = Directory.GetDirectories(reply.Path);
            var filePaths = Directory.GetFiles(reply.Path);
            foreach (var path in directoryPaths)
            {
                reply.DirectoryNames.Add(Path.GetFileName(path));
            }
            foreach (var path in filePaths)
            {
                reply.FileNames.Add(Path.GetFileName(path));
            }
            reply.Path = reply.Path.Replace(_rootPath, "");
            if (reply.Path == "") reply.Path = @"\";

            return Task.FromResult(reply);
        }
    }
}
