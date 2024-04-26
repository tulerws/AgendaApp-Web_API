using AgendaApp.API.Models;
using AgendaApp2.API.Models;
using AgendaApp2.Data.Entities;
using AutoMapper;

namespace AgendaApp2.API.Mappings
{
    /// <summary>
    /// Classe para configurar os mapeamentos do automapper
    /// </summary>
    public class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<CriarTarefaRequestModel, Tarefa>();

            CreateMap<Tarefa, CriarTarefaResponseModel>();

            CreateMap<Tarefa, EditarTarefaResponseModel>();

            CreateMap<Tarefa, ExcluirTarefaResponseModel>();

            CreateMap<Tarefa, ConsultarTarefaResponseModel>();
        }
    }
}
