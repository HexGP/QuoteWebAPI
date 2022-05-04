using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QuoteData.Data;

namespace QuoteRepo.Repos
{
    public class QuoteRepository : GenericRepository<tblQuote> IQuoteRepository
    {
        //private readonly QuoteDBEntities context;
        public QuoteRepository(QuoteDBEntities quoteDB) : base(quoteDB)
        {
            
        }
         
        //---------------------------------------------------------------------------------------------------

        public void PutQuote(int quoteID, tblQuote quote)
        {
            using (var db = new QuoteDBEntities())
            {
                var result = db.tblQuotes.SingleOrDefault(t => t.QuoteID == quoteID);
                if (result != null)
                {
                    result.QuoteType = quote.QuoteType;
                    result.TaskType = quote.TaskType;
                }
                db.SaveChanges();
            }
        }
                
        public void PatchQuote(int quoteID, tblQuote quote)
        {
            using (QuoteDBEntities db = new QuoteDBEntities())
            {
                var entity = db.tblQuotes.SingleOrDefault(e => e.QuoteID == quoteID);
                if (entity != null)
                {

                    var target = quote.QuoteType;
                    if (target != null)
                        entity.QuoteType = target;

                    target = quote.Contact;
                    if (target != null)
                        entity.Contact = target;

                    target = quote.Task;
                    if (target != null)
                        entity.Task = target;

                    target = quote.DueDate;
                    if (target != null)
                        entity.DueDate = target;

                    target = quote.TaskType;
                    if (target != null)
                        entity.TaskType = target;
                }
                db.SaveChanges();
            }

            using (var db = new QuoteDBEntities())
            {
                var result = db.tblQuotes.SingleOrDefault(t => t.QuoteID == quoteID);

                if (result != null)
                {
                    var target = quote.QuoteType;
                    if (target != null)
                    {
                        result.QuoteType = target;
                    }

                    var target2 = quote.TaskType;
                    if (target2 != null)
                    {
                        result.TaskType = target2;
                    }
                }
                db.SaveChanges();
            }
        }

    }
}
