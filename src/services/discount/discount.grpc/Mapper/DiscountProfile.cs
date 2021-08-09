using AutoMapper;
using discount.grpc.Entities;
using discount.grpc.Protos;

namespace discount.grpc.Mapper
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}