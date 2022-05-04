using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using QuoteRepo.Repos;
using QuoteData.Data;

namespace QuoteRepo
{
    public interface IUnitOfWork : IDisposable
    {
        QuoteRepository Quote  { get; }

        //UserRepository User { get; }
        int Complete();
    }
}
