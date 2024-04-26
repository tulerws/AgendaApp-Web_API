using AgendaApp2.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp2.Data.Mappings
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("TAREFA");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("ID");

            builder.Property(t => t.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.Prioridade)
                .HasColumnName("PRIORIDADE")
                .IsRequired();

            builder.Property(t => t.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            builder.Property(t => t.DataHoraCadastro)
                .HasColumnName("DATAHORACADASTRO")
                .IsRequired();

            builder.Property(t => t.DataHoraUltimaAtualizacao)
                .HasColumnName("DATAHORAULTIMAATUALIZACAO")
                .IsRequired();

            builder.Property(t => t.Status)
                .HasColumnName("STATUS")
                .IsRequired();


            #region Relacionamentos


            #endregion
        }
    }
}
