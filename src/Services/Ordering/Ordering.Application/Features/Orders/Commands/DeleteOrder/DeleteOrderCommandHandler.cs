﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteOrderCommandHandler>  _logger;

    public DeleteOrderCommandHandler
        (
        IOrderRepository orderRepository, 
        IMapper mapper, 
        ILogger<DeleteOrderCommandHandler> logger
        )
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

        if (orderToDelete == null)
        {
            _logger.LogError($"Order Not Found To Delete With Id: {request.Id}.");
            throw new NotFoundException(nameof(DeleteOrderCommand), request.Id);
        }
        await _orderRepository.DeleteAsync(orderToDelete);
    }

    //public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    //{
    //    var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);

    //    if (orderToDelete == null)
    //    {
    //        _logger.LogError($"Order Not Found To Delete With Id: {request.Id}.");
    //        throw new NotFoundException(nameof(DeleteOrderCommand), request.Id);
    //    }
    //    await _orderRepository.DeleteAsync(orderToDelete);
    //    return Unit.Value;
    //}
}
