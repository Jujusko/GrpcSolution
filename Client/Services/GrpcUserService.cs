using Client.Configurations;
using Client.Models;

namespace Client.Services;

public class GrpcService
{
    private readonly ILogger<GrpcService> _logger;
    private readonly GrpcConfiguration _configuration;
    
    public GrpcService(ILogger<GrpcService> logger, GrpcConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    //public async Task<List<ProductModel>> GetAllProducts(int from, int amount)
    //{
        
    //}
    

  
}