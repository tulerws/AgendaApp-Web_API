using AgendaApp2.Data.Contexts;
using AgendaApp2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp2.Data.Repositories
{
    public class TarefaRepository
    {
        public void Add(Tarefa tarefa)
        {
            using (var context = new DataContext())
            {
                context.Add(tarefa);
                context.SaveChanges();
            }
        }

        public void Update(Tarefa tarefa)
        {
            using (var context = new DataContext())
            {
                context.Update(tarefa);
                context.SaveChanges();
            }
        }

        public void Delete(Tarefa tarefa)
        {
            using (var context = new DataContext())
            {
                context.Remove(tarefa);
                context.SaveChanges();
            }
        }

        public List<Tarefa> Get(DateTime dataInicio, DateTime DataFim)
        {
            using (var context = new DataContext())
            {
                return context
                    .Set<Tarefa>()
                    .Where(t => t.DataHora >= dataInicio && t.DataHora <= DataFim)
                    .OrderByDescending(t => t.DataHora)
                    .ToList();
            }
        }

        public Tarefa? GetById(Guid id)
        {
            using (var context = new DataContext())
            {
                return context.Set<Tarefa>().Find(id);
            }
        }
    }
}
