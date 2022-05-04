using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuoteServices;
using QuoteServices.DTO;
using QuoteWebAPI;
using Microsoft.AspNet.Identity;

namespace QuoteWebAPI.Controllers
{
    public class QuoteController : ApiController
    {
        MapperConfiguration config;
        IMapper mapper;
        QuoteService quoteService;

        public QuoteController()
        {
            quoteService = new QuoteService();
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<QuoteDTO, QuoteService>();
                cfg.CreateMap<QuoteService, QuoteDTO>();
            });
            mapper = config.CreateMapper();
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<QuoteDTO> Get()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            return quoteService.GetAllQuotes();
        }

        //GET by ID
        [HttpGet]
        public IEnumerable<QuoteDTO> GetQuoteByID(int id)
        {
            return quoteService.GetQuoteByID(id);
        }

        //POST
        [HttpPost]
        public HttpResponseMessage PostQuote([FromBody] QuoteDTO qoute)
        {
            QuoteDTO qouteDTO = mapper.Map<QuoteDTO>(qoute);
            quoteService.PostQuote(qouteDTO);

        }

        //PUT
        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] QuoteDTO qoute)
        {
            try
            {
                if (qoute == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Quote with ID = " + id.ToString() + " not found to update");
                }
                else
                {
                    quoteService.PutQuote(id, qoute);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //PATCH
        [HttpPatch]
        public IHttpActionResult PatchTask([FromUri] int id, [FromBody] QuoteService qoute)
        {
            if (qoute == null)
            {
                return BadRequest("Nothing To Patch");
            }
            else
            {
                QuoteDTO taskDTO = mapper.Map<QuoteDTO>(qoute);
                quoteService.PatchQuote(id, taskDTO);
                return Ok(quoteService.GetQuoteByID(id));
            }
        }

        //DELETE
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var target = quoteService.GetQuoteByID(id);
            try
            {
                if (target != null)
                {
                    quoteService.DeleteQuote(id);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Quote with ID = " + id.ToString() + " not found to delete");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
