using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PusheenCustomExportCsv.Web.Data;
using PusheenCustomExportCsv.Web.Models;

namespace PusheenCustomExportCsv.Web.Services
{
    public class PusheenService : IPusheenService
    {
        private readonly PusheenCustomExportCsvContext _context;

        public PusheenService(PusheenCustomExportCsvContext context)
        {
            _context = context;
        }

        public IEnumerable<Pusheen> GetAllPusheens()
        {
            var pusheens = _context.Pusheens.ToList();
            return pusheens;
        }

        public async Task<List<Pusheen>> GetAllAsync()
        {
            var pusheens = await _context.Pusheens.ToListAsync();
            return pusheens;
        }

        public async Task<Pusheen> Create(Pusheen pusheen)
        {
            _context.Add(pusheen);
            await _context.SaveChangesAsync();
            return pusheen;
        }

        public async Task<Pusheen> Update(Pusheen pusheen)
        {
            _context.Update(pusheen);
            await _context.SaveChangesAsync();
            return pusheen;
        }

        public async Task<Pusheen> Delete(Pusheen pusheen)
        {
            _context.Pusheens.Remove(pusheen);
            await _context.SaveChangesAsync();
            return pusheen;
        }

        public async Task<Pusheen> FindPusheenAsync(int? id)
        {
            var pusheen = await _context.Pusheens.FindAsync(id);
            return pusheen;
        }

        public async Task<Pusheen> FindPusheenById(int? id)
        {
            var pusheen = await _context.Pusheens
                .FirstOrDefaultAsync(m => m.Id == id);
            return pusheen;
        }

        public bool PusheenExists(int id)
        {
            return _context.Pusheens.Any(e => e.Id == id);
        }

    }
}