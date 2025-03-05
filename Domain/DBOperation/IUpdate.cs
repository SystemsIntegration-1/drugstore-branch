using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

/* 
 * Represents the contract for updating an entity of type T in the database.
 * 
 * @typeparam T The type of the entity that is being updated. It must implement the IEntity interface.
 * 
 * This interface defines a method for updating an entity of type T in the database.
 */
public interface IUpdate<T> where T : IEntity
{
    Task<T> Update(T entity);
}
