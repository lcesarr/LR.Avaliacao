using LR.Avaliacao.Domain.Core;

namespace LR.Avaliacao.Domain.EntitiesData
{
    /// <summary>
    /// 
    /// </summary>
    public class UsuarioData : IdEntityData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Senha { get; set; }
    }
}
