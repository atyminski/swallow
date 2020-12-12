using System;

namespace Gevlee.Swallow.Web.Utils
{
    public interface IApiUrlProvider
    {
        Uri BaseUrl { get; }
    }
}