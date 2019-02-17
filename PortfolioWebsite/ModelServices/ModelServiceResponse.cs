using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PortfolioWebsite.ModelServices
{
    public class ModelServiceResponse<T> : IModelServiceResponse<T>
    {
        public T Model { get; private set; }
        public HttpStatusCode HttpStatusCode { get; private set; }
        public ValidationStateDictionary ValidationState { get; private set; }

        public ModelServiceResponse(T model, ValidationStateDictionary validationState, HttpStatusCode statusCode)
        {
            Model = model;
            HttpStatusCode = statusCode;
            ValidationState = validationState;
        }
    }
}
