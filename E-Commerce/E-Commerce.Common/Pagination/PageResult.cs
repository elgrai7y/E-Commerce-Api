
namespace E_Commerce.Common
{
    public class PageResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public PaginationMetaData MetaData { get; set; }
    }
}
