using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Server.Interceptor
{
    public class ClientRequestInterceptor : Grpc.Core.Interceptors.Interceptor
    {
        private readonly ILogger<ClientRequestInterceptor> _logger;

        public ClientRequestInterceptor(ILogger<ClientRequestInterceptor> logger)
        {
            _logger = logger;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {

            _logger.LogInformation("Request from client");
            return continuation(request, context);
            //return base.AsyncUnaryCall(request, context, continuation);
        }
    }
}
