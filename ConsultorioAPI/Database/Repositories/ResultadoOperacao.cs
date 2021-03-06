﻿namespace ConsultorioAPI.Database.Repositories
{
    /// <summary>
    /// Diz se uma operação teve sucesso o não e carrega uma mensagem junto
    /// </summary>
    public class ResultadoOperacao
    {
        /// <summary>
        /// Representa uma operação que deu certo, sem mensagem
        /// </summary>
        public static ResultadoOperacao Ok
        {
            get {
                return new ResultadoOperacao()
                {
                    Sucesso = true,
                    ErroInterno = false
                };
            }
        }

        /// <summary>
        /// Quando a operação não encontrou o recurso requerido
        /// </summary>
        public static ResultadoOperacao NotFound
        {
            get
            {
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    ErroInterno = false,
                    Mensagem = "Não encontrado"
                };
            }
        }

        /// <summary>
        /// Representa uma operação na qual ocorreu um erro no banco de dados
        /// </summary>
        public static ResultadoOperacao ErroBD
        {
            get
            {
                return new ResultadoOperacao()
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro no banco de dados"
                };
            }
        }

        public bool Sucesso { get; set; }

        /// <summary>
        /// Caso não tenha sucesso, diz se ocorreu erro interno de servidor
        /// </summary>
        public bool ErroInterno { get; set; }

        public string Mensagem { get; set; }
    }
}