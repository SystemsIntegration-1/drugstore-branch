using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

public interface ICreate<T> where T : IEntity
{
    Task<T> Create(T entity);
}

