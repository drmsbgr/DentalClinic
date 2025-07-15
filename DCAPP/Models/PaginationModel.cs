namespace DCAPP.Models;

public class PaginationModel
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasNext => CurrentPage < PageCount;
    public bool HasPrev => CurrentPage > 1;
}