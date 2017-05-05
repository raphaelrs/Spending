using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Spending.Util
{
    public static class PaginacaoAjaxHelper
    {
        public static IHtmlString RodapeAjax(this AjaxHelper html, int paginaAtual, int tamanhoPagina, int totalDados, string actionResultExecutada)
        {
            var stringHtmlTEMP = string.Empty;
            var ultimaPagina = (int)Math.Ceiling(totalDados / (double)tamanhoPagina) - 1;
            var totalPaginasEsquerda = 0;
            var totalPaginasDireita = 0;
            const int QUANTIDADE_MAXIMA_PAGINAS_UM_LADO = 5;

            if (paginaAtual - QUANTIDADE_MAXIMA_PAGINAS_UM_LADO / 2 < 0)
            {
                totalPaginasEsquerda = paginaAtual;
                totalPaginasDireita = QUANTIDADE_MAXIMA_PAGINAS_UM_LADO - totalPaginasEsquerda;

                var possivelUltimaPagina = paginaAtual + totalPaginasDireita;

                if (possivelUltimaPagina > ultimaPagina)
                {
                    totalPaginasDireita = ultimaPagina - paginaAtual;
                }
            }
            else
            {
                if (ultimaPagina - paginaAtual < QUANTIDADE_MAXIMA_PAGINAS_UM_LADO / 2)
                {
                    totalPaginasDireita = ultimaPagina - paginaAtual;
                    totalPaginasEsquerda = QUANTIDADE_MAXIMA_PAGINAS_UM_LADO - totalPaginasDireita;

                    if (paginaAtual - totalPaginasEsquerda < 0)
                    {
                        totalPaginasEsquerda = paginaAtual;
                    }
                }
                else
                {
                    totalPaginasDireita = QUANTIDADE_MAXIMA_PAGINAS_UM_LADO / 2;
                    totalPaginasEsquerda = QUANTIDADE_MAXIMA_PAGINAS_UM_LADO / 2;
                }
            }

            if (paginaAtual - totalPaginasEsquerda > 0)
            {
                stringHtmlTEMP += html.ActionLink("Primeira  ", actionResultExecutada, new { pagina = 0 }, new AjaxOptions { HttpMethod = "POST", UpdateTargetId = "transactionListContainer", InsertionMode = InsertionMode.Replace }).ToHtmlString() + "  ";
            }

            for (int i = paginaAtual - totalPaginasEsquerda; i < paginaAtual; i++)
            {
                stringHtmlTEMP += html.ActionLink(i.ToString(), actionResultExecutada, new { pagina = i }, new AjaxOptions { HttpMethod = "POST", UpdateTargetId = "transactionListContainer", InsertionMode = InsertionMode.Replace }).ToHtmlString() + "  ";
            }

            stringHtmlTEMP += paginaAtual + "  ";

            for (int i = paginaAtual + 1; i <= (paginaAtual + totalPaginasDireita); i++)
            {
                stringHtmlTEMP += html.ActionLink(i.ToString(), actionResultExecutada, new { pagina = i }, new AjaxOptions { HttpMethod = "POST", UpdateTargetId = "transactionListContainer", InsertionMode = InsertionMode.Replace }).ToHtmlString() + "  ";
            }

            if (paginaAtual + totalPaginasDireita < ultimaPagina)
            {
                stringHtmlTEMP += html.ActionLink("Última", actionResultExecutada, new { pagina = ultimaPagina }, new AjaxOptions { HttpMethod = "POST", UpdateTargetId = "transactionListContainer", InsertionMode = InsertionMode.Replace }).ToHtmlString() + "  ";
            }

            return new HtmlString(stringHtmlTEMP);
        }
    }
}