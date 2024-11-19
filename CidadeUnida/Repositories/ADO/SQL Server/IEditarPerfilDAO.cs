using CidadeUnida.ViewModels;

namespace CidadeUnida.Repositories.ADO.SQL_Server
{
    public interface IEditarPerfilDAO
    {
        public bool AtualizarPerfil(PerfilViewModel perfil);

        public PerfilViewModel ObterPerfil(int idUsuario);
    }
}
