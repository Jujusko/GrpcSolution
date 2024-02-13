using Grpc.Core;
using GrpcSolution.Test.V1;

namespace Server.Services
{
    public class GrpcTestService : TestService.TestServiceBase
    {
        public override Task<GetRandomNumberResponse> GetRandomNumber(GetRandomNumberRequest request, ServerCallContext context)
        {

            var response = new GetRandomNumberResponse
            {
                Number = 1
            };
            return Task.FromResult(response);
        }
    }
}
