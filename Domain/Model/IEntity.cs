using System;

namespace drugstore_branch.Domain.Model;

/* 
 * Defines a base entity with a unique identifier (Id). 
 * All domain models implementing this interface will have an Id property.
 */
public interface IEntity
{
    Guid Id { get; set; }
}
