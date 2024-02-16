using AutoMapper;
using Client.Mappers;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Client.Interceptors;

public class ClientRequestInterceptor : Grpc.Core.Interceptors.Interceptor
{
    private readonly ILogger<ClientRequestInterceptor> _logger;

    public ClientRequestInterceptor(ILogger<ClientRequestInterceptor> logger)
    {
        _logger = logger;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Отправлен gRPC запрос {request} на сервер", continuation.Method);

        return continuation(request, context);
    }
}