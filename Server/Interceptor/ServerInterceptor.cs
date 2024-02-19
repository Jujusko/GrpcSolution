using Grpc.Core;

namespace Server.Interceptor;

public class ServerInterceptor : Grpc.Core.Interceptors.Interceptor
{
    private readonly ILogger<ServerInterceptor> _logger;

    public ServerInterceptor(ILogger<ServerInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation("Starting receiving call. Type/Method: {Type} / {Method}",
            MethodType.Unary, context.Method);
        try
        {
            return await continuation(request, context);
        }
        catch (Exception e)
        {
            _logger.LogError(
                "Ошибка | Exception {Exception} [{ExceptionType}] | InnerException {InnerException}",
                e.Message, typeof(Exception), e.InnerException?.Message);
            throw;
        }
    }
}