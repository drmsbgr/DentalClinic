namespace DCAPP.Models;

public class PaginationDataModel<T>
{
    public List<T>? PaginatedList { get; set; }
    public PaginationModel? PaginationModel { get; set; }
}