using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spending.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class CustomAuthorize : AuthorizeAttribute
    {

        protected void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new NullReferenceException("Context is null");
            }

            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                this.SetCachePolicy(filterContext);
            }
            else if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Se a autenticação falhar, redirecionará para a página de login.
            //    filterContext.Result = new HttpUnauthorizedResult();
            //}
            //else
            //{
                filterContext.Result = new RedirectResult("~/Home/Index");

                return;
            }
        }

        /// <summary>
        /// Indica como será a política de cache para o contexto em questão.
        /// </summary>
        /// <param name="filterContext">Contexto do atributo.</param>
        protected void SetCachePolicy(AuthorizationContext filterContext)
        {
            // ** IMPORTANTE ** 
            // Quando estamos realizando uma autorização à nivel de action, o codigo de autorização
            // roda após o modulo de caching de saída (output caching module). No pior caso, isso
            // pode permitir que um usuário autorizado deixe em cache uma página, deste modo um 
            // usuário não autorizado poderá depois acessar a página em cache. Para poder evitar isso
            // nós informamos aos proxies para não deixar em cache a página.
            HttpCachePolicyBase cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(TimeSpan.Zero);
            cachePolicy.AddValidationCallback(CacheValidateHandler, null /* data */);
        }
    }
}