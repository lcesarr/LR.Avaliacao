using LR.Avaliacao.Domain.Core;
using System;

namespace LR.Avaliacao.Domain.EntitiesData
{
    /// <summary>
    /// 
    /// </summary>
    public class ClienteData : IdEntityData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cpf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Aniversario { get; set; }
    }
}
