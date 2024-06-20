using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests
{
    public abstract class PagedRequest : Request
    {
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
        public int PagaSize { get; set; } = Configuration.DefaultPageSize;
    }
}
