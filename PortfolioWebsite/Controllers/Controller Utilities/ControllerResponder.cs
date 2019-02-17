using Microsoft.AspNetCore.Http;
using PortfolioWebsite.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioWebsite.Controllers
{
    public class ControllerResponder<T>
    {
        public Func<IModelServiceResponse<T>, IActionResult> this[HttpStatusCode index]
        {
            get
            {
                if (_responses.ContainsKey(index))
                {
                    return _responses[index];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _responses[index] = value;
            }
        }

        private readonly Dictionary<HttpStatusCode, Func<IModelServiceResponse<T>, IActionResult>> _responses = new Dictionary<HttpStatusCode, Func<IModelServiceResponse<T>, IActionResult>>();
        public Func<IModelServiceResponse<T>, IActionResult> this[params HttpStatusCode[] indices]
        {
            set
            {
                foreach(HttpStatusCode index in indices)
                {
                    _responses[index] = value;
                }
            }
        }

        public Func<IModelServiceResponse<T>, IActionResult> Default { get; set; } = null;

        public IActionResult Respond(IModelServiceResponse<T> modelServiceResponse)
        {
            if (_responses.ContainsKey(modelServiceResponse.HttpStatusCode))
            {
                return _responses[modelServiceResponse.HttpStatusCode](modelServiceResponse);
            }
            else
            {
                return Default(modelServiceResponse);
            }
        }
    }
}
