﻿namespace Discount.Grpc.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CouponModel, Coupon>().ReverseMap();
    }
}
