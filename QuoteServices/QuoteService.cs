using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QuoteData.Data;
using QuoteRepo;
using QuoteServices.DTO;
using AutoMapper;

namespace QuoteServices
{
    public class QuoteService
    {
        public UnitOfWork uow { get; set; }
        public QuoteDBEntities quoteDB;

        MapperConfiguration config;
        IMapper mapper;

        public QuoteService()
        {
            this.quoteDB = new QuoteDBEntities();
            this.uow = new UnitOfWork(quoteDB);

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tblQuote, QuoteDTO>();
                cfg.CreateMap<QuoteDTO, tblQuote>();
            }
            );
            mapper = config.CreateMapper();
        }

        public List<QuoteDTO> GetAllQuotes()
        {
            IEnumerable<tblQuote> quotes = uow.Quote.GetAll();
            List<QuoteDTO> results = new List<QuoteDTO>();

            foreach (tblQuote quote in quotes)
            {
                QuoteDTO taskDTO = new QuoteDTO()
                {
                    QuoteID = quote.QuoteID,
                    QuoteType = quote.QuoteType
                };
                results.Add(taskDTO);
            }
            uow.Complete();

            return results;
        }

        public IList<QuoteDTO> GetQuoteByID(int id)
        {
            var tasks = uow.Quote.GetAll();

            var result = (
                from q in tasks
                where q.QuoteID.Equals(id)
                select new QuoteDTO
                {
                    QuoteID = q.QuoteID,
                    QuoteType = q.QuoteType
                }).ToList();
            uow.Complete();
            return result;
        }



        public QuoteDTO DeleteQuote(int id)
        {
            tblQuote foundTask = uow.Quote.GetById(id);
            if (foundTask != null)
            {
                uow.Quote.Remove(foundTask);
                uow.Complete();
                return mapper.Map<QuoteDTO>(foundTask);
            }
            else
            {
                return null;
            }
        }

        public void PostQuote(QuoteDTO taskDTO)
        {
            tblQuote task = mapper.Map<tblQuote>(taskDTO);
            uow.Quote.Add(task);
            uow.Complete();
        }


        public void PutQuote(int id, QuoteDTO quoteDTO)
        {
            uow.Quote.PutQuote(id, mapper.Map<tblQuote>(quoteDTO));
        }



        public void PatchQuote(int id, QuoteDTO quoteDTO)
        {
            uow.Quote.PatchQuote(id, mapper.Map<tblQuote>(quoteDTO));
        }
    }
}
