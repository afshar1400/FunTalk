using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunTalk.Application.Common.Helper
{
    public class PaginationList<T>:List<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }


        public PaginationList(List<T> items,int pageIndex,int pageSize,int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPage = (int)Math.Ceiling((double)count / pageSize);
            TotalCount = count;
            Items = items;
        }

        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => TotalPage > PageIndex ;

        public static async Task<PaginationList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginationList<T>(items,pageIndex,pageSize,count);
        }

    }
}
