using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Api.Models;
using RT.Application.Commands.ClienteFeatures;
using RT.Application.Queries;
using RT.Domain.Exceptions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RT.Api.Controllers
{
    /// <summary>
    /// Responsável por receber as requisiçoes para manutenção do Cliente
    /// </summary>
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClienteController> _logger;

        /// <summary>
        /// Construtor padrão da classe ClienteController
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ClienteController(
            IMediator mediator,
            ILogger<ClienteController> logger)
        {
            _mediator = mediator;
            this._logger = logger;
        }

        // GET: api/<ClienteController>
        /// <summary>
        /// Pesquisa por clientes de uma empresa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PesquisarClienteRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);

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
        /// Obtem os dados de um cliente e seus endereços
        /// </summary>
        /// <param name="id">Indentificação do Cliente</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ObterClientePorIdResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var request = new ObterClientePorIdRequest(id);
                var result = await _mediator.Send(request);

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
        /// Cadastra um novo cliente
        /// </summary>
        /// <param name="cadastrarClienteRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] CadastrarClienteRequest cadastrarClienteRequest)
        {
            try
            {
                var cadastrarClienteCommand = cadastrarClienteRequest.Map();

                await _mediator.Send(cadastrarClienteCommand);
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

        /// <summary>
        /// Altera todos os dados de um cliente
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="atualizarClienteRequest">dados que serão atualizados</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AtualizarClienteRequest atualizarClienteRequest)
        {
            try
            {
                var command = atualizarClienteRequest.Map(id);

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

        /// <summary>
        /// Remove um cliente
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new RemoverClienteCommand(id);
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
