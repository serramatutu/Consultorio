using ConsultorioAPI.Models;
using ConsultorioAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsultorioAPI.Database.Repositories
{
    public class ConsultaRepository : IDisposable
    {
        protected ConsultorioDbContext _ctx;

        public ConsultaRepository(ConsultorioDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Utilizado para checar se um agendamento de consulta é conflitante ou inválido
        /// </summary>
        /// <param name="a">Agendamento a ser testado</param>
        /// <returns>se o agendamento é inválido</returns>
        protected ResultadoOperacao AgendamentoInvalido(AgendamentoConsulta a)
        {
            // Seleciona o médico pelo CRM
            Medico medicoResponsavel = _ctx.Medicos.FirstOrDefault(x => x.CRM == a.CRMMedicoResponsavel);
            if (medicoResponsavel == null) // Médico não encontrado
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "O médico especificado não existe"
                };


            // Procura por consulta do mesmo médico responsável no horário especificado
            bool horarioIndisponivel = _ctx.Consultas.Any(delegate (Consulta c)
            {
                double diferenca = (c.DataHora - a.DataHora).TotalMinutes;
                bool achouConsulta = false;

                // A diferença dos horários é menor que a duração da consulta que vem primeiro
                if (Math.Abs(diferenca) < (diferenca > 0 ? a.Duracao : c.Duracao))
                    achouConsulta = true;

                return c.Medico.Id == medicoResponsavel.Id &&
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

        /// <summary>
        /// Agenda uma consulta
        /// </summary>
        /// <param name="a">Dados do agendamento</param>
        /// <param name="p">Paciente da consulta</param>
        /// <returns>Se o agendamento teve sucesso</returns>
        public async Task<ResultadoOperacao> AgendarConsulta(AgendamentoConsulta a, Guid idPaciente)
        {
            Paciente p = _ctx.Pacientes.FirstOrDefault(x => x.Id.Equals(idPaciente));
            if (p == null)
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Paciente inválido"
                };

            ResultadoOperacao r = AgendamentoInvalido(a);
            if (!r.Sucesso)
                return r;

            Consulta c = null;
            try
            {
                c = new Consulta()
                {
                    Id = Guid.NewGuid(),
                    Duracao = a.Duracao,
                    DataHora = a.DataHora,
                    Status = StatusConsulta.Agendada,
                    Medico = _ctx.Medicos.FirstOrDefault(x => x.CRM == a.CRMMedicoResponsavel),
                    Paciente = p
                };
            }
            catch (ArgumentException e)
            {
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = e.Message
                };
            }
            

            _ctx.Consultas.Add(c);
            if (await _ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        /// <summary>
        /// Cancela uma consulta
        /// </summary>
        /// <param name="idConsulta">Id da consulta a ser cancelada</param>
        public async Task<ResultadoOperacao> CancelarConsulta(Guid idConsulta, Guid idPaciente)
        {
            Consulta c = GetConsulta(idConsulta);
            if (c == null || !c.Paciente.Id.Equals(idPaciente))
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Consulta inexistente"
                };

            if (c.Status != StatusConsulta.Agendada)
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Não é possível cancelar consulta não agendada"
                };

            var medico = c.Medico; // Força o LazyLoader pq a implementação do EF é ruim pra isso :( (se tirar dá erro)
            c.Status = StatusConsulta.Cancelada;
            _ctx.Entry(c).Property(x => x.Status).IsModified = true;

            if (await _ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        /// <summary>
        /// Modifica o comentário de um médico de uma consulta
        /// </summary>
        /// <param name="comentario">Comentário novo</param>
        /// <param name="idConsulta">Consulta a ter seu comentário modificado</param>
        public async Task<ResultadoOperacao> AlterarDiagnostico(string diagnostico, Guid idConsulta, Guid idMedico)
        {
            Consulta c = GetConsulta(idConsulta);
            if (c == null || !c.Medico.Id.Equals(idMedico))
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Consulta inexistente"
                };

            var medico = c.Medico; // Força o LazyLoader pq a implementação do EF é ruim pra isso :( (se tirar dá erro)
            c.Diagnostico = diagnostico;
            _ctx.Entry(c).State = System.Data.Entity.EntityState.Modified;

            if (await _ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        /// <summary>
        /// Modifica a avaliação de um paciente de uma consulta
        /// </summary>
        /// <param name="avaliacao">Avaliação nova</param>
        /// <param name="idConsulta">Consulta a ter sua avaliação modificado</param>
        /// /// <param name="idPaciente">Paciente da consulta</param>
        public async Task<ResultadoOperacao> AvaliarConsulta(AvaliacaoConsulta avaliacao, Guid idConsulta, Guid idPaciente)
        {
            Consulta c = GetConsulta(idConsulta);
            if (c == null || !c.Paciente.Id.Equals(idPaciente))
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Consulta inexistente"
                };

            if (!string.IsNullOrEmpty(c.Avaliacao.Comentario) || c.Avaliacao.Nota.HasValue)
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Não pode avaliar uma consulta duas vezes"
                };

            var medico = c.Medico; // Força o LazyLoader pq a implementação do EF é ruim pra isso :( (se tirar dá erro)
            c.Avaliacao = avaliacao;
            _ctx.Entry(c).Property(x => x.Avaliacao).IsModified = true;

            if (await _ctx.SaveChangesAsync() < 1) // Não mexeu em nenhuma linha
                return ResultadoOperacao.ErroBD;

            return ResultadoOperacao.Ok;
        }

        /// <summary>
        /// Obtém todas as consultas do consultório em um período
        /// </summary>
        /// <param name="dias">Período especificado em dias</param>
        public Consulta[] GetConsultasDePeriodo(int dias)
        {
            return _ctx.Consultas.Where(c => c.DataHora < DateTime.Today && 
            DbFunctions.AddDays(c.DataHora, dias).Value > DateTime.Today).ToArray();
        }

        public Consulta[] GetConsultasDeUsuario(Guid idUsuario)
        {
            return _ctx.Consultas.Where(x => x.Paciente.Id.Equals(idUsuario)).ToArray();
        }

        public Consulta[] GetConsultasDeMedico(Guid idMedico)
        {
            return _ctx.Consultas.Where(x => x.Medico.Id.Equals(idMedico)).ToArray();
        }

        public Consulta[] GetConsultasDeMedico(string CRM)
        {
            return _ctx.Consultas.Where(x => x.Medico.CRM == CRM).ToArray();
        }

        protected Consulta GetConsulta(Guid idConsulta)
        {
            return _ctx.Consultas.FirstOrDefault(x => x.Id.Equals(idConsulta));
        }

        protected bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}