using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioAPI.Database.Repositories
{
    public class ConsultaRepository
    {
        /// <summary>
        /// Utilizado para checar se um agendamento de consulta é conflitante ou inválido
        /// </summary>
        /// <param name="a">Agendamento a ser testado</param>
        /// <returns>se o agendamento é inválido</returns>
        protected ResultadoOperacao AgendamentoInvalido(AgendamentoConsulta a)
        {
            using (var ctx = new ConsultorioDbContext())
            {
                // Seleciona o médico pelo CRM
                Medico medicoResponsavel = ctx.Medicos.FirstOrDefault(x => x.CRM == a.CRMMedicoResponsavel);
                if (medicoResponsavel == null) // Médico não encontrado
                    return new ResultadoOperacao()
                    {
                        Sucesso = false,
                        Mensagem = "O médico especificado não existe"
                    };


                // Procura por consulta do mesmo médico responsável no horário especificado
                bool horarioIndisponivel = ctx.Consultas.Any(delegate (Consulta c)
                {
                    double diferenca = (c.DataHora - a.DataHora).TotalMinutes;
                    bool achouConsulta = false;

                    // A diferença dos horários é menor que a duração da consulta que vem primeiro
                    if (Math.Abs(diferenca) < (diferenca > 0 ? a.Duracao : c.Duracao))
                        achouConsulta = true;

                    return c.MedicoResponsavel.Id == medicoResponsavel.Id &&
                           c.Status != StatusConsulta.Cancelada && // Se a consulta está cancelada, o horário dela está disponível
                           achouConsulta;
                });

                if (horarioIndisponivel)
                    return new ResultadoOperacao()
                    {
                        Sucesso = false,
                        Mensagem = "O horário da consulta especificada é conflitante"
                    };

                return ResultadoOperacao.Ok;
            }
        }

        /// <summary>
        /// Agenda uma consulta
        /// </summary>
        /// <param name="a">Dados do agendamento</param>
        /// <param name="p">Paciente da consulta</param>
        /// <returns>Se o agendamento teve sucesso</returns>
        public async Task<ResultadoOperacao> AgendarConsulta(AgendamentoConsulta a, Paciente p)
        {
            ResultadoOperacao r = AgendamentoInvalido(a);
            if (!r.Sucesso)
                return r;

            using (var ctx = new ConsultorioDbContext())
            {
                Consulta c = new Consulta()
                {
                    Id = Guid.NewGuid(),
                    DataHora = a.DataHora,
                    Duracao = a.Duracao,
                    Status = StatusConsulta.Agendada,
                    MedicoResponsavel = ctx.Medicos.FirstOrDefault(x => x.CRM == a.CRMMedicoResponsavel),
                    Paciente = p
                };

                ctx.Consultas.Add(c);
                if (await ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                    return ResultadoOperacao.ErroBD;

                return ResultadoOperacao.Ok;
            }
        }

        /// <summary>
        /// Cancela uma consulta
        /// </summary>
        /// <param name="idConsulta">Id da consulta a ser cancelada</param>
        public async Task<ResultadoOperacao> CancelarConsulta(Guid idConsulta)
        {
            Consulta c = GetConsulta(idConsulta);
            if (c == null)
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Consulta inexistente"
                };

            using (var ctx = new ConsultorioDbContext())
            {
                c.Status = StatusConsulta.Cancelada;
                ctx.Entry(c).State = System.Data.Entity.EntityState.Modified;

                if (await ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                    return ResultadoOperacao.ErroBD;

                return ResultadoOperacao.Ok;
            }
        }

        /// <summary>
        /// Modifica o comentário de uma consulta
        /// </summary>
        /// <param name="comentario">Comentário novo</param>
        /// <param name="idConsulta">Consulta a ter seu comentário modificado</param>
        public async Task<ResultadoOperacao> ComentarConsulta(string comentario, Guid idConsulta)
        {
            Consulta c = GetConsulta(idConsulta);
            if (c == null)
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Consulta inexistente"
                };

            using (var ctx = new ConsultorioDbContext())
            {
                c.Comentario = comentario;
                ctx.Entry(c).State = System.Data.Entity.EntityState.Modified;

                if (await ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                    return ResultadoOperacao.ErroBD;

                return ResultadoOperacao.Ok;
            }
        }

        protected Consulta GetConsulta(Guid idConsulta)
        {
            using (var ctx = new ConsultorioDbContext())
            {
                return ctx.Consultas.FirstOrDefault(x => x.Id.Equals(idConsulta));
            }
        }
    }
}