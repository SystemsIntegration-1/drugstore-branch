using System;

namespace drugstore_branch.Domain.Model;

public interface IEntity
{
    Guid Id { get; set; }
}
