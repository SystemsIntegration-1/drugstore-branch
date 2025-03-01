using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Service.SearchService;

public interface ISearchable<T> where T : class
{
    T Search()
    {
        return null!;
    }
}