﻿using ETicaretAPI.Application.DTOs.Order;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
        Task<ListOrder> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);
    }
}
