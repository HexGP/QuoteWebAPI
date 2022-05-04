using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuoteData.Data;

namespace QuoteRepo.Repos
{
    public interface IQuoteRepository : IGenericRepository<tblQuote>
    {
        void PutQuote(int quoteID, tblQuote quoteType);
        void PatchQuote(int quoteID, tblQuote quoteType);
    }
}
