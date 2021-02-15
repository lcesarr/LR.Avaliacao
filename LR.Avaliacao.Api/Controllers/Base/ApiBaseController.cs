using Flunt.Notifications;
using LR.Avaliacao.Api.Filters;
using LR.Avaliacao.Application.Resultado;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LR.Avaliacao.Api.Controllers.Base
{
    /// <summary>
    /// Base para chamadas de API
    /// </summary>
    [ServiceFilter(typeof(DefaultExceptionFilterAttribute))]
    public abstract class ApiBaseController : ControllerBase
    {
        /// <summary>
        /// Trata retorno de BadRequest.
        /// </summary>
        /// <param name="notifications">Notificações de erro.</param>
        /// <returns></returns>
        protected BadRequestObjectResult BadRequest(IReadOnlyCollection<Notification> notifications)
        {
            return new BadRequestObjectResult(new Erro(notifications));
        }

        /// <summary>
        /// Trata retorno de não encontrado.
        /// </summary>
        /// <param name="message">Mensagem a ser retornada.</param>
        /// <returns></returns>
        protected NotFoundObjectResult NotFound(string message)
        {
            return new NotFoundObjectResult(new Erro(message));
        }
    }
}