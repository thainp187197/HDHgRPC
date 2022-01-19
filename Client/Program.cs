using Grpc.Net.Client;
using gRPC.Server;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new Greeter.GreeterClient(channel);
            var greeterReply = await greeterClient.SayHelloAsync(new HelloRequest { Name = "Myself" });
            Console.WriteLine("Greeting: " + greeterReply.Message);

            var calculatorClient = new Calculator.CalculatorClient(channel);
            var calculatorRequest = new IntFactorRequest() { X = 10, Y = 5 };
            var addResponse = await calculatorClient.AddAsync(calculatorRequest);
            var subtractResponse = await calculatorClient.SubtractAsync(calculatorRequest);
            var multiplyResponse = await calculatorClient.MultiplyAsync(calculatorRequest);
            var divideResponse = await calculatorClient.DivideAsync(calculatorRequest);
            Console.WriteLine($"Calculator:\n" +
                $"Addition: {calculatorRequest.X} + {calculatorRequest.Y} = {addResponse.F}\n" +
                $"Subtraction: {calculatorRequest.X} - {calculatorRequest.Y} = {subtractResponse.F}\n" +
                $"Multiplication: {calculatorRequest.X} * {calculatorRequest.Y} = {multiplyResponse.F}\n" +
                $"Divisor: {calculatorRequest.X} / {calculatorRequest.Y} = {divideResponse.F}\n");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
