using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

public interface IRead<T> where T : IEntity
{
    Task<IEnumerable<T>> ReadAllAsync();
    async Task<IEnumerable<T>> ReadAsync(string parameter, object search)
    {
        List<string> allowedParams = SetParams();
        if (!allowedParams.Contains(parameter))
        {
            throw new ArgumentException($"'{parameter}' is not valid.");
        }
        return await SearchDbAsync(parameter, search);
    }

    List<string> SetParams();
    Task<IEnumerable<T>> SearchDbAsync(string parameter, object search);
}
