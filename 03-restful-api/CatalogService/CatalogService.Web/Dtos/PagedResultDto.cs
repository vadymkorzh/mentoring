using CatalogService.DAL.Models;

namespace CatalogService.Web.Dtos;

public class PagedResultDto<T>
{
    public required IReadOnlyList<T> Items { get; init; }

    public int TotalCount { get; init; }

    public int Page { get; init; }

    public int PageSize { get; init; }
}

public static class PagedResultMappingExtensions
{
    public static PagedResultDto<TDto> ToDto<T, TDto>(this PagedResult<T> result, Func<T, TDto> map) => new()
    {
        Items = result.Items.Select(map).ToList(),
        TotalCount = result.TotalCount,
        Page = result.Page,
        PageSize = result.PageSize
    };
}
