using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

public interface IUpdate<T> where T : IEntity
{
    Task<T> Update(T entity);
}
