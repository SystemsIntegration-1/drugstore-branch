using System;
using drugstore_branch.Domain.Model;

/* 
 * Represents the contract for creating entities of type T in the database.
 * 
 * @typeparam T The type of the entity that is being created. It must implement the IEntity interface.
 * 
 * This interface provides the definition for the Create method that must be implemented
 * by any class that handles the creation of entities.
 */
namespace drugstore_branch.Domain.DBOperations;

public interface ICreate<T> where T : IEntity
{
    Task<T> Create(T entity);
}
