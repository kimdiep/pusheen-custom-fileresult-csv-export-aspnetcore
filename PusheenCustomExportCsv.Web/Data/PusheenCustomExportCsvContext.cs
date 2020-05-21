using Microsoft.EntityFrameworkCore;
using PusheenCustomExportCsv.Web.Models;

namespace PusheenCustomExportCsv.Web.Data
{
    public class PusheenCustomExportCsvContext : DbContext
    {
        public PusheenCustomExportCsvContext (DbContextOptions<PusheenCustomExportCsvContext> options)
            : base(options)
        {
        }

        public DbSet<Pusheen> Pusheens { get; set; }
    }
}