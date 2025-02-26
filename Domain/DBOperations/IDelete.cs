using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

public interface IDelete<T> where T : IEntity
{
    Task<bool> Delete(Guid id);
}
