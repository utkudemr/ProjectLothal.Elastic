
namespace ProjectLothal.Elastic.Core.Paging;

public static class ElasticPaginateExtension
{
    public static Paginate<T> ToPaginateAsync<T>(
        this IReadOnlyCollection<T> source,
        int page,
        int size,
        long pageCount
        )
    {
        List<T> result = source.ToList();
        Paginate<T> list = new()
        {
            Items = result,
            PageIndex = page,
            Size = result.Count,
            Pages = (int)Math.Ceiling(pageCount / (double)size)
        };
        return list;
    }
}
