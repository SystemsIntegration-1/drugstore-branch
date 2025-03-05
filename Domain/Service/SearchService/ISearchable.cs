using drugstore_branch.Domain.Model;

namespace drugstore_branch.Domain.Service.SearchService;

/* 
 * Defines a contract for searchable entities. The Search method 
 * allows for searching entities of type T.
 */
public interface ISearchable<T> where T : class
{
    T Search()
    {
        return null!;
    }
}
