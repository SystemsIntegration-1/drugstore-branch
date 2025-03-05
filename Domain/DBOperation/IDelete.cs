using System;
using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.DBOperations;

/* 
 * Represents the contract for deleting entities of type T from the database.
 * 
 * @typeparam T The type of the entity that is being deleted. It must implement the IEntity interface.
 * 
 * This interface defines the Delete method that must be implemented by any class 
 * responsible for handling the deletion of entities.
 */
public interface IDelete<T> where T : IEntity
{
    Task<bool> Delete(Guid id);
}
