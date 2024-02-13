// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/grpc_solution/product/v1/product.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcSolution.Product.V1 {
  public static partial class ProductService
  {
    static readonly string __ServiceName = "grpc_solution.product.v1.ProductService";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcSolution.Product.V1.GetProductByIdServiceRequest> __Marshaller_grpc_solution_product_v1_GetProductByIdServiceRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcSolution.Product.V1.GetProductByIdServiceRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcSolution.Product.V1.GetProductByIdServiceResponse> __Marshaller_grpc_solution_product_v1_GetProductByIdServiceResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcSolution.Product.V1.GetProductByIdServiceResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcSolution.Product.V1.AddProductServiceRequest> __Marshaller_grpc_solution_product_v1_AddProductServiceRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcSolution.Product.V1.AddProductServiceRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcSolution.Product.V1.AddProductServiceResponse> __Marshaller_grpc_solution_product_v1_AddProductServiceResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcSolution.Product.V1.AddProductServiceResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcSolution.Product.V1.GetAllProductsRequest> __Marshaller_grpc_solution_product_v1_GetAllProductsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcSolution.Product.V1.GetAllProductsRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::GrpcSolution.Product.V1.GetAllProductsResponse> __Marshaller_grpc_solution_product_v1_GetAllProductsResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::GrpcSolution.Product.V1.GetAllProductsResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcSolution.Product.V1.GetProductByIdServiceRequest, global::GrpcSolution.Product.V1.GetProductByIdServiceResponse> __Method_GetProductByIdService = new grpc::Method<global::GrpcSolution.Product.V1.GetProductByIdServiceRequest, global::GrpcSolution.Product.V1.GetProductByIdServiceResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetProductByIdService",
        __Marshaller_grpc_solution_product_v1_GetProductByIdServiceRequest,
        __Marshaller_grpc_solution_product_v1_GetProductByIdServiceResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcSolution.Product.V1.AddProductServiceRequest, global::GrpcSolution.Product.V1.AddProductServiceResponse> __Method_AddProductService = new grpc::Method<global::GrpcSolution.Product.V1.AddProductServiceRequest, global::GrpcSolution.Product.V1.AddProductServiceResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddProductService",
        __Marshaller_grpc_solution_product_v1_AddProductServiceRequest,
        __Marshaller_grpc_solution_product_v1_AddProductServiceResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::GrpcSolution.Product.V1.GetAllProductsRequest, global::GrpcSolution.Product.V1.GetAllProductsResponse> __Method_GetAllProductsService = new grpc::Method<global::GrpcSolution.Product.V1.GetAllProductsRequest, global::GrpcSolution.Product.V1.GetAllProductsResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetAllProductsService",
        __Marshaller_grpc_solution_product_v1_GetAllProductsRequest,
        __Marshaller_grpc_solution_product_v1_GetAllProductsResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcSolution.Product.V1.ProductReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for ProductService</summary>
    public partial class ProductServiceClient : grpc::ClientBase<ProductServiceClient>
    {
      /// <summary>Creates a new client for ProductService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public ProductServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ProductService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public ProductServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected ProductServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected ProductServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcSolution.Product.V1.GetProductByIdServiceResponse GetProductByIdService(global::GrpcSolution.Product.V1.GetProductByIdServiceRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetProductByIdService(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcSolution.Product.V1.GetProductByIdServiceResponse GetProductByIdService(global::GrpcSolution.Product.V1.GetProductByIdServiceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetProductByIdService, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcSolution.Product.V1.GetProductByIdServiceResponse> GetProductByIdServiceAsync(global::GrpcSolution.Product.V1.GetProductByIdServiceRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetProductByIdServiceAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcSolution.Product.V1.GetProductByIdServiceResponse> GetProductByIdServiceAsync(global::GrpcSolution.Product.V1.GetProductByIdServiceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetProductByIdService, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcSolution.Product.V1.AddProductServiceResponse AddProductService(global::GrpcSolution.Product.V1.AddProductServiceRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddProductService(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcSolution.Product.V1.AddProductServiceResponse AddProductService(global::GrpcSolution.Product.V1.AddProductServiceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AddProductService, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcSolution.Product.V1.AddProductServiceResponse> AddProductServiceAsync(global::GrpcSolution.Product.V1.AddProductServiceRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AddProductServiceAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcSolution.Product.V1.AddProductServiceResponse> AddProductServiceAsync(global::GrpcSolution.Product.V1.AddProductServiceRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AddProductService, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcSolution.Product.V1.GetAllProductsResponse GetAllProductsService(global::GrpcSolution.Product.V1.GetAllProductsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllProductsService(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::GrpcSolution.Product.V1.GetAllProductsResponse GetAllProductsService(global::GrpcSolution.Product.V1.GetAllProductsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetAllProductsService, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcSolution.Product.V1.GetAllProductsResponse> GetAllProductsServiceAsync(global::GrpcSolution.Product.V1.GetAllProductsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetAllProductsServiceAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::GrpcSolution.Product.V1.GetAllProductsResponse> GetAllProductsServiceAsync(global::GrpcSolution.Product.V1.GetAllProductsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetAllProductsService, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override ProductServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ProductServiceClient(configuration);
      }
    }

  }
}
#endregion