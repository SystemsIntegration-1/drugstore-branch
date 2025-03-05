using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

/* 
 * Represents the contract for reading entities of type T from the database.
 * 
 * @typeparam T The type of the entity that is being read. It must implement the IEntity interface.
 * 
 * This interface defines methods for reading all entities, reading entities based on specific parameters, 
 * and internal methods for validation and searching in the database.
 */
public interface IRead<T> where T : IEntity
{
    Task<IEnumerable<T>> ReadAllAsync();
    async Task<IEnumerable<T>> ReadAsync(string parameter, object search)
    {
        var allowedParams = SetParams();
        if (!allowedParams.Contains(parameter))
        {
            throw new ArgumentException($"'{parameter}' is not valid.");
        }
        return await SearchDbAsync(parameter, search);
    }
    List<string> SetParams();
    Task<IEnumerable<T>> SearchDbAsync(string parameter, object search);
}
