**ELLPScore** é uma plataforma web voltada para instituições educacionais, desenvolvida para gerenciar o desempenho dos alunos e informações acadêmicas. O projeto utiliza **ASP.NET Core 8** para o backend, incluindo **ASP.NET Identity** para autenticação, e **Razor Pages** para geração de conteúdo dinâmico na web. A aplicação permite que professores façam login, cadastrem alunos, gerenciem disciplinas e acompanhem o desempenho dos alunos por meio de relatórios gráficos e gráficos dinâmicos.

## Funcionalidades

- **Autenticação e Autorização**: Funcionalidade de login e registro segura para professores utilizando **ASP.NET Identity**.
- **Gestão de Alunos**: Cadastro, atualização e gerenciamento de alunos, incluindo detalhes pessoais e acadêmicos.
- **Gestão de Turmas e Disciplinas**: Operações de CRUD para gerenciar turmas e disciplinas.
- **Acompanhamento de Desempenho**: Gráficos e relatórios dinâmicos para acompanhar o desempenho dos alunos em diversas disciplinas.
- **Atualização Assíncrona**: Uso de AJAX e Partial Views para atualização de dados de desempenho sem recarregar a página.
- **Relatórios Gráficos**: Visualização de desempenho por matéria, comparação entre alunos e suas notas, e monitoramento de aprovações e reprovações.

## Tecnologias Utilizadas

- **ASP.NET Core 8**: Framework principal utilizado para desenvolvimento do backend.
- **Entity Framework Core**: ORM utilizado para interagir com o banco de dados.
- **Dapper**: Utilizado para realizar consultas otimizadas no banco de dados.
- **Razor Pages**: Utilizadas para criar a interface do usuário com conteúdo dinâmico.
- **AJAX**: Para carregamento dinâmico de dados nas páginas, melhorando a experiência do usuário.
- **ASP.NET Identity**: Gerenciamento de autenticação e autorização de usuários.
- **Bootstrap**: Framework CSS utilizado para o design responsivo.

## Instalação e Configuração

### Pré-requisitos

- **.NET Core SDK 8.0** ou superior
- **SQL Server** ou outro banco de dados compatível
- **Visual Studio** ou qualquer IDE compatível com projetos .NET

### Passos para Instalação

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-usuario/ELLPScore.git
   ```

2. Navegue até o diretório do projeto:

   ```bash
   cd ELLPScore
   ```

3. Restaure os pacotes NuGet:

   ```bash
   dotnet restore
   ```

4. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`:

   ```json
   "ConnectionStrings": {
      "DefaultConnection": "Server=SEU_SERVIDOR;Database=ELLPScoreDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

5. Execute as migrações para criar o banco de dados:

   ```bash
   dotnet ef database update
   ```

6. Execute a aplicação:

   ```bash
   dotnet run
   ```

## Estrutura do Projeto

- **ELLPScore.Domain**: Contém as entidades e modelos de domínio, como `Aluno`, `Turma`, `Disciplina`, `Professor`, etc.
- **ELLPScore.Application**: Lógica de aplicação e serviços responsáveis pelas funcionalidades principais.
- **ELLPScore.Web**: Contém as páginas Razor e a interface de usuário.
- **ELLPScore.Infrastructure**: Implementações de acesso a dados, incluindo repositórios e migrações.

## Funcionalidades Futuras

- Integração com API externa para obter dados atualizados de disciplinas e turmas.
- Notificações automáticas para professores sobre desempenho de alunos.
- Painel administrativo com mais funcionalidades para coordenadores.

## Licença

Este projeto é licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.
