using LR.Avaliacao.Application.Resultado;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LR.Avaliacao.Api.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<DefaultExceptionFilterAttribute> _logger;

        /// <summary>
        /// Construtor com ILogger
        /// </summary>
        /// <param name="logger"></param>
        public DefaultExceptionFilterAttribute(ILogger<DefaultExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override async void OnException(ExceptionContext context)
        {
            StringBuilder sb = await TratarRetorno(context);
            _logger.LogError(sb.ToString());

            context.Result = new ObjectResult(new Erro(context.Exception.Message))
            {
                StatusCode = HttpStatusCode.InternalServerError.GetHashCode()
            };
        }
        private async Task<StringBuilder> TratarRetorno(ExceptionContext context)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in context.RouteData.Values)
            {
                if (!(item.Value is string))
                    sb.Append($"'Entrada': '{item.Key}': '{JsonSerializer.Serialize(item.Value)}' - ");
                else
                    sb.Append($"'{item.Key}': '{item.Value}' - ");
            }
            sb.Append($"'Resultado': '{context.Exception.Message}'");
            return sb;
        }
    }
}
