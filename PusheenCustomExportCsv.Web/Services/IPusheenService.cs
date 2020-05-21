using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PusheenCustomExportCsv.Web.Models;

namespace PusheenCustomExportCsv.Web.Services
{
    public interface IPusheenService
    {
        IEnumerable<Pusheen> GetAllPusheens();
        Task<List<Pusheen>> GetAllAsync();
        Task<Pusheen> Create(Pusheen pusheen);
        Task<Pusheen> Update(Pusheen pusheen);
        Task<Pusheen> Delete(Pusheen pusheen);
        Task<Pusheen> FindPusheenAsync(int? id);
        Task<Pusheen> FindPusheenById(int? id);
        bool PusheenExists(int id);

    }
}