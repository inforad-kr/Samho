﻿using SapBroker.Models;

namespace SapBroker.Services;

interface ISapService
{
    IEnumerable<Order> RetrieveOrders();
}
