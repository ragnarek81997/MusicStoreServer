using MusicStoreServer.Domain.Entities.Enums;
using MusicStoreServer.Domain.Entities.Struct;
using MusicStoreServer.Web.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MusicStoreServer.Web.Controllers.ApiControllers
{
    public class CustomApiController : ApiController
    {
        public IHttpActionResult ServiceResult<T>(ServiceResult<T> source) where T : class
        {
            if (source.Success)
            {
                return Ok(source.Result);
            }
            else
            {
                return new ErrorResult(source, Request);
            }
        }

        public IHttpActionResult ServiceResult(ServiceResult source)
        {
            if (source.Success)
            {
                return Ok();
            }
            else
            {
                return new ErrorResult(source, Request);
            }
        }
    }
}
