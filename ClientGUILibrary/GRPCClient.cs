using gRPC.Server;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientGUILibrary
{
    public class GRPCClient
    {
        public static async Task<Directory> ExplorePathAsync(string path, string dirName)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var fileClient = new File.FileClient(channel);
            var fileRequest = new PathRequest() { Path = path, DirectoryName = dirName };
            var response = await fileClient.ExplorePathAsync(fileRequest);
            return new Directory(response.Path, response.DirectoryNames, response.FileNames);
        }
    }
}
