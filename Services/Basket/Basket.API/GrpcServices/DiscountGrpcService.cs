using Discount.Grpc.Protos;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;
        

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            //var channel = GrpcChannel.ForAddress("http://localhost:5003");
            //var client = new DiscountProtoService.DiscountProtoServiceClient(channel);

            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _discountProtoService.GetDiscountAsync(discountRequest);
            //var reply = client.GetDiscountAsync(discountRequest); 
            //return await client.GetDiscountAsync(discountRequest);
            //return await reply;
        }
    }
}
