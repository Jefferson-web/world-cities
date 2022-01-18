using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldCities.Models
{
    public class ApiResult<T>
    {

        #region Properties
        /// <summary>
        /// The data result
        /// </summary>
        public List<T> Data { get; set; }
        /// <summary>
        /// Zero-based index of current page.
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// Tiotal pages count
        /// </summary>
        public int TotalPages { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string FilterColumn { get; set; }
        public string FilterQuery { get; set; }

        /// <summary>
        /// TRUE if the current page has a previus page, FALSE otherwise
        /// </summary>
        public bool HasPreviusPage {
            get 
            {
                return (PageIndex > 0);            
            }
        }

        /// <summary>
        /// TRUE if the current page has a next page, FALSE otherwise
        /// </summary>
        public bool HasNextPage {
            get 
            {
                return ((PageIndex + 1) < TotalPages);
            }
        }

        #endregion

        private ApiResult(
                List<T> data,
                int count,
                int pageIndex,
                int pageSize,
                string sortColumn,
                string sortOrder,
                string filterColumn,
                string filterQuery
            )
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            FilterColumn = filterColumn;
            FilterQuery = filterQuery;
        }

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">An IQueryable source of generic type</param>
        /// <param name="pageIndex">Zero-based current page index</param>
        /// <param name="pageSize">The actual size of each page</param>
        /// <param name="sortColumn">The sorting column name</param>
        /// <param name="sortOrder">The sorting order ("ASC" or "DESC")</param>
        /// <param name="filterColumn">The filtering column</param>
        /// <param name="filterQuery">The filtering query</param>
        /// <returns></returns>
        public static async Task<ApiResult<T>> CreateAsync(
            IQueryable<T> source,
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null,
            string filterColumn = null,
            string filterQuery = null
        )
        {

            int count = await source.CountAsync();

            source = source.Skip(pageIndex * pageSize).Take(pageSize);

            var data = await source.ToListAsync();

            return new ApiResult<T>(
                data,
                count,
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder,
                filterColumn,
                filterQuery
            );
        }

        #endregion

    }
}
