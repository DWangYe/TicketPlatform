using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketPlatform.Web.Models;

namespace TicketPlatform.Web.Repository
{
    public class TpContext : DbContext
    {
        public TpContext(DbContextOptions<TpContext> options) :base(options)
        {//带参数DbContex可以省略重写OnConfiguring方法
        }

        public DbSet<BaseDictionary> BaseDictionaries { get; set; }
    }
}
