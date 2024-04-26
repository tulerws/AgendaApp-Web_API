using AgendaApp.API.Models;
using AgendaApp2.API.Models;
using AgendaApp2.Data.Entities;
using AgendaApp2.Data.Entities.Enums;
using AgendaApp2.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        //atributo
        private readonly IMapper _mapper;

        public TarefasController(IMapper mapper)
        {
            _mapper = mapper;
        }


        /// <summary>
        /// Serviço da API para cadastro de tarefas
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CriarTarefaResponseModel), 200)]
        public IActionResult Post(CriarTarefaRequestModel model)
        {
            try
            {
                //copiando os dados do objeto 'model' para 'tarefa'
                var tarefa = _mapper.Map<Tarefa>(model);

                tarefa.Id = Guid.NewGuid();
                tarefa.DataHoraCadastro = DateTime.Now;
                tarefa.DataHoraUltimaAtualizacao = DateTime.Now;
                tarefa.Status = 1;

                //gravando no banco de dados
                var tarefaRepository = new TarefaRepository();
                tarefaRepository.Add(tarefa);

                var response = _mapper.Map<CriarTarefaResponseModel>(tarefa);
                
                return StatusCode(201, new { message = "Tarefa cadastrada com sucesso: " , response });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = "FALHA AO CADASTRAR TAREFA: " + e.Message});
            }
        }

        /// <summary>
        /// Serviço da API para edição de tarefas
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(EditarTarefaResponseModel), 200)]
        public IActionResult Put(EditarTarefaRequestModel model)
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(model.Id.Value);

                if(tarefa != null)
                {
                    tarefa.Nome = model.Nome;
                    tarefa.Descricao = model.Descricao;
                    tarefa.DataHora = model.DataHora;
                    tarefa.Prioridade = (PrioridadeTarefa)model.Prioridade;

                    tarefaRepository.Update(tarefa);

                    var response = _mapper.Map<EditarTarefaResponseModel>(tarefa);

                    return StatusCode(200, new { message = "Tarefa atualizada com sucesso: ", response });
                }
                else
                {
                    return StatusCode(400, new { message = "O ID da tarefa é inválido." });
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço da API para exclusão de tarefas
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ExcluirTarefaResponseModel), 200)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                if(tarefa != null)
                {
                    tarefaRepository.Delete(tarefa);

                    var response = _mapper.Map<ExcluirTarefaResponseModel>(tarefa);
                    response.DataHoraExclusao = DateTime.Now;


                    return StatusCode(200, new { message = "Tarefa excluída com sucesso." , response });
                }
                else
                {
                    return StatusCode(400, new { message = "Tarefa não encontrada." });
                }
            }
            catch(Exception e) 
            {
                return StatusCode(500, new { e.Message});
            }
        }

        /// <summary>
        /// Serviço da API para consulta de tarefas
        /// </summary>
        [HttpGet("{dataInicio}/{dataFim}")]
        [ProducesResponseType(typeof(List<ConsultarTarefaResponseModel>), 200)]
        public IActionResult Get(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                //consultar as tarefas no banco de dados através das datas
                var tarefaRepository = new TarefaRepository();
                var tarefas = tarefaRepository.Get(dataInicio, dataFim);

                //copiar os dados das tarefas para uma lista de ConsultarTarefaResponseModel
                var response = _mapper.Map<List<ConsultarTarefaResponseModel>>(tarefas);

                return StatusCode(200, response);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço da API para consultar 1 tarefa baseado no ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ConsultarTarefaResponseModel), 200)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var tarefaRepository = new TarefaRepository();
                var tarefa = tarefaRepository.GetById(id);

                var response = _mapper.Map<ConsultarTarefaResponseModel>(tarefa);

                return StatusCode(200, response);
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}



    