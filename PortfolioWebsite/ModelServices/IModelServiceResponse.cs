using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PortfolioWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PortfolioWebsite.ModelServices
{
    public interface IModelServiceResponse<T>
    {
        T Model { get; }
        HttpStatusCode HttpStatusCode { get; }
        ValidationStateDictionary ValidationState { get; }
    }
}
