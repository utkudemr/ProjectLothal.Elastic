

namespace ProjectLothal.Elastic.Core.Paging;

public class Paginate<T>
{
    public Paginate()
    {
        Items = new List<T>();
    }

    public int PageIndex { get; set; }
    public int Size { get; set; }
    public int Pages { get; set; }
    public IList<T> Items { get; set; }
    public bool HasPrevious => PageIndex > 1;
    public bool HasNext => PageIndex + 1 < Pages;
}