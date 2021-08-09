using System;
using System.Threading.Tasks;
using AutoMapper;
using discount.grpc.Entities;
using discount.grpc.Protos;
using discount.grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace discount.grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(
            IDiscountRepository repository,
            ILogger<DiscountService> logger,
            IMapper mapper
        )
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<CouponModel> GetDiscount(
            GetDiscountRequest request,
            ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(
                    new Status(
                        StatusCode.NotFound,
                        $"Discount with ProductName={request.ProductName} not found"
                    )
                );
            }
            _logger.LogInformation(
                "Discount is retieved for ProductName: {productName}, Amount: {amount}",
                coupon.ProductName,
                coupon.Amount
            );

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(
            CreateDiscountRequest request,
            ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.CreateDiscount(coupon);
            _logger.LogInformation(
                "Discount was successfully created. ProductName: {productName}",
                coupon.ProductName
            );

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(
            UpdateDiscountRequest request,
            ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation(
                "Discount was successfully updated. ProductName: {productName}",
                coupon.ProductName
            );

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(
            DeleteDiscountRequest request,
            ServerCallContext context
        )
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted,
            };
            return response;
        }
    }
}