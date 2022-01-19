using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gRPC.Server
{
    public class CalculatorService : Calculator.CalculatorBase
    {
        public override Task<ResultReply> Add(IntFactorRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultReply
            {
                F = request.X + request.Y,
            });
        }

        public override Task<ResultReply> Subtract(IntFactorRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultReply
            {
                F = request.X - request.Y,
            });
        }

        public override Task<ResultReply> Multiply(IntFactorRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ResultReply
            {
                F = request.X * request.Y,
            });
        }

        public override Task<ResultReply> Divide(IntFactorRequest request, ServerCallContext context)
        {
            if (request.Y == 0) throw new ArgumentException("Divisor must not be zero.");
            return Task.FromResult(new ResultReply
            {
                F = request.X / request.Y,
            });
        }
    }
}
