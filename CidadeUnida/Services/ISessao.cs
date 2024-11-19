using CidadeUnida.Models;
using CidadeUnida.ViewModels;
using NuGet.Protocol.Plugins;

namespace CidadeUnida.Services
{
    public interface ISessao
    {
        public void AddTokenLogin(LoginViewModel login);

        public LoginViewModel GetTokenLogin();

        public void DeleteTokenLogin();
    }
}
