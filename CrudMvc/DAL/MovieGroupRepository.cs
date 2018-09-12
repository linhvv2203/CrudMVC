using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudMvc.Models;
using System.Runtime.Caching;

namespace CrudMvc.DAL
{
    public class MovieGroupRepository : GenericRepository<MovieGroup>
    {

        public MovieGroupRepository(VideoEntities context) : base(context)
        {
        }
    }
}