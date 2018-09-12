using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudMvc.Models
{
    public class PagingParameterModel
    {
        const int maxPagingSize = 20;

        public int pageNumber { get; set; } = 1;
        public int _pageSize { get; set; } = 10;
        public int pageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPagingSize) ? maxPagingSize : value;
            }
        }

    }
}