using System;
using drugstore_branch.Domain.DBOperations;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Repository;

/* 
 * Defines the contract for order data operations, supporting creation 
 * and retrieval of order records.
 */                                                     
public interface IOrderRepository : ICreate<Order>, IRead<Order>
{
    
}
