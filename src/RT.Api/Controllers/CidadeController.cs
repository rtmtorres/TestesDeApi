using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Api.Models;
using RT.Application.Queries;
using RT.Domain.Exceptions;
using System.Threading.Tasks;

namespace RT.Api.Controllers
{
    /// <summary>
    /// Responsável por receber as requisiçoes para manutenção da Cidade
    /// </summary>
    [Route("api/cidades")]
    [ApiController]
    public class CidadeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CidadeController> _logger;

        /// <summary>
        /// Construtor padrão da classe ClienteController
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public CidadeController(IMediator mediator, ILogger<CidadeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        /// <summary>
        /// Retorna uma lista de cidades cadastradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _mediator.Send(new ListarCidadesRequest());

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                if (ex is BaseApplicationException)
                {
                    return BadRequest(ex.Message);
                }
                _logger.LogError(ex, "Erro ao criar um novo cliente");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Cadastra uma nova cidade
        /// </summary>
        /// <param name="cadastrarCidadeRequest">Dados necessários para cadastro de uma cidade</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] CadastrarCidadeRequest cadastrarCidadeRequest)
        {
            try
            {
                var command = cadastrarCidadeRequest.Map();

                await _mediator.Send(command);
                return Ok();
            }
            catch (System.Exception ex)
            {
                if (ex is BaseApplicationException)
                {
                    return BadRequest(ex.Message);
                }
                _logger.LogError(ex, "Erro ao criar um novo cliente");
                return StatusCode(500);
            }
        }

    }
}
