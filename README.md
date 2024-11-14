# Plataforma de Denúncias Urbanas - Cidade Unida ♻

Este repositório contém o projeto backend do sistema de denúncias urbanas desenvolvido como parte do Trabalho de Conclusão de Curso no curso de Desenvolvimento de Software Multiplataforma. Este projeto é desenvolvido por quatro alunos da Faculdade de Tecnologia de Matão "Luiz Marchesan" e tem como objetivo aplicar os conhecimentos adquiridos em um sistema robusto e funcional que visa melhorar a qualidade de vida nas cidades por meio da participação ativa da comunidade.

## Sobre o Projeto 📚

O projeto é uma plataforma web para registro de denúncias urbanas, alinhada ao Objetivo de Desenvolvimento Sustentável (ODS) 11 da ONU, que visa tornar as cidades e comunidades mais inclusivas, seguras, resilientes e sustentáveis. A plataforma permite que cidadãos reportem problemas urbanos, como buracos nas vias, iluminação deficiente, coleta de lixo inadequada e outros, facilitando a comunicação entre a população e as autoridades competentes.

## Tecnologias Utilizadas 💻

- **Backend:** ASP.NET Core MVC 6.0, C#
- **Banco de Dados:** SQL Server, DAO.NET para operações de acesso a dados
- **Gerenciamento de Versão:** Git e GitHub
- **Ferramentas de Documentação e Colaboração:** Figma, Notion

## Objetivo do Projeto 🎯

Desenvolver um sistema backend funcional que permita o gerenciamento de denúncias, oferecendo funcionalidades de autenticação, autorização, controle de dados e integração com o banco de dados, aplicando as melhores práticas de desenvolvimento e arquitetura em ASP.NET MVC.

## Estrutura do Projeto 🧱

- **/Controllers:** Controladores responsáveis por definir a lógica de negócios e a comunicação entre a camada de apresentação e os dados.
- **/Models:** Modelos das entidades do projeto, como `Denuncia`, `Usuario`, e `Contato`, juntamente com suas propriedades e métodos de validação.
- **/Views:** Arquivos de interface gerados em Razor para renderizar as páginas web com os dados necessários.
- **/DataAccess:** Classe de acesso a dados com DAO.NET para realizar operações CRUD no banco de dados SQL Server.
- **/wwwroot:** Arquivos estáticos utilizados pelo backend, como scripts e arquivos de estilo.
- **README.md:** Documentação do projeto.
- **.gitignore:** Arquivo para ignorar arquivos desnecessários no controle de versão.
- **LICENSE:** Arquivo de licença.

## Funcionalidades Implementadas 🚀

- **Autenticação e Autorização:** Controle de acesso com base em permissões de usuário (usuários comuns e administradores).
- **Registro e Gerenciamento de Denúncias:** Funcionalidades CRUD (Create, Read, Update, Delete) para denúncias, categorizadas por tipo e status.
- **Cadastro e Edição de Usuários:** Gerenciamento de usuários com perfis diferenciados e funcionalidades de ativação e desativação de contas.
- **Interface de Administração:** Painel de controle exclusivo para o administrador, com visualização e edição das denúncias submetidas.

## Configuração do Projeto 🛠️

### Pré-requisitos

- [.NET Core SDK](https://dotnet.microsoft.com/download) versão 6.0 ou superior
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) para gerenciar o banco de dados

### Instruções para Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/usuario/cidade-unida-backend.git
   cd cidade-unida-backend
   ```

2. Configure a string de conexão para o SQL Server no arquivo `appsettings.json`.

3. Execute as migrações para configurar o banco de dados:

   ```bash
   dotnet ef database update
   ```

4. Inicie o servidor:

   ```bash
   dotnet run
   ```

O projeto estará acessível em `https://localhost:5001`.

## Documentação de Cores 🎨

| Cor                 | Hexadecimal |
| ------------------- | ----------- |
| Azul-paleta         | #112B3C     |
| Verde-paleta        | #5BC561     |
| Verde-escuro-paleta | #3B8C4B     |
| Vermelho-paleta     | #A92C2C     |
| Cinza-paleta        | #CED8E2     |

## Contribuintes 👨‍🎓

- Rafael
- Miguel
- Jerônimo
- Pedro

### Como Contribuir 😃

Se desejar contribuir com melhorias ou correções, explore o repositório e abra uma issue ou envie um pull request. Toda sugestão é bem-vinda!

### Licença

Este projeto está licenciado sob a Licença MIT - consulte o arquivo [LICENSE](LICENSE) para mais detalhes.
