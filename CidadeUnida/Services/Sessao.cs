using CidadeUnida.Models;
using CidadeUnida.ViewModels;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace CidadeUnida.Services
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string _tokenSessao;

        public Sessao(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            _tokenSessao = "login";
        }

        public void AddTokenLogin(LoginViewModel login)
        {
            string loginTokenJson = JsonConvert.SerializeObject(login);
            httpContextAccessor.HttpContext?.Session.SetString(_tokenSessao, loginTokenJson);
        }

        public LoginViewModel GetTokenLogin()
        {
            string? loginTokenJson = httpContextAccessor.HttpContext?.Session.GetString(_tokenSessao);
            return loginTokenJson != null ? JsonConvert.DeserializeObject<LoginViewModel>(loginTokenJson) : null;
        }

        public void DeleteTokenLogin()
        {
            httpContextAccessor.HttpContext?.Session.Remove(_tokenSessao);
        }
    }
}
