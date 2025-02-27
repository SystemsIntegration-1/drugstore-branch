using System;
using drugstore_branch.Domain.DBOperations;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Repository;

public interface IOrderRepository : ICreate<Order>, IRead<Order>
{
    
}
