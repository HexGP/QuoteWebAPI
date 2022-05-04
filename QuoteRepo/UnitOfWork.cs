using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteRepo.Repos;
using QuoteData.Data;

namespace QuoteRepo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QuoteDBEntities _context;
        public UnitOfWork(QuoteDBEntities context)
        {
            _context = context;
            Quote = new QuoteRepository(_context);
        }

        public IQuoteRepository Quote { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
