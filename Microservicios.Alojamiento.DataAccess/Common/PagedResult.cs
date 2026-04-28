using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicios.Alojamiento.DataAccess.Common
{
    public class PagedResult<T>
    {
        // La lista de elementos de la página actual (ej. 10 hoteles)
        public IEnumerable<T> Items { get; set; } = new List<T>();

        // Metadatos para el Frontend
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        // Cálculo automático del total de páginas
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        // Atajos útiles
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;

        public PagedResult() { }

        public PagedResult(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
        }
    }
}
