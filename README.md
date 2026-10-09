# Gestão de Equipamentos

Aplicação web para organizar fabricantes, equipamentos e chamados de manutenção. O projeto usa ASP.NET Core MVC, com páginas Razor e armazenamento local em arquivo JSON.

## Funcionalidades

- Cadastro, consulta, edição e exclusão de fabricantes.
- Cadastro, consulta, edição e exclusão de equipamentos, associados a um fabricante.
- Cadastro e consulta de chamados, associados a um equipamento, com título, descrição, data de abertura e status.
- Validação de campos nos modelos e formulários.

## Tecnologias

- C# e ASP.NET Core MVC
- .NET 10
- Razor Views
- System.Text.Json para persistência dos dados

## Pré-requisitos

- .NET SDK 10.0 ou superior compatível com o projeto.

Verifique a instalação com:

```powershell
dotnet --version
```

## Como executar

Na pasta raiz do repositório, restaure as dependências e inicie a aplicação:

```powershell
dotnet restore GestaoDeEquipamentoss.Web\GestaoDeEquipamentoss.Web.csproj
dotnet run --project GestaoDeEquipamentoss.Web\GestaoDeEquipamentoss.Web.csproj
```

O perfil HTTP configurado para desenvolvimento usa `http://localhost:5220`. Também há um perfil HTTPS em `https://localhost:7061`.

## Armazenamento dos dados

Os dados são salvos localmente em `dados.json`, dentro da pasta `GestaoDeEquipamentosWebs` do diretório de dados locais do usuário. No Windows, esse local normalmente corresponde a:

```text
%LOCALAPPDATA%\GestaoDeEquipamentosWebs\dados.json
```

O arquivo e a pasta são criados pela aplicação quando necessário. Não é preciso configurar um banco de dados para executá-la.

## Estrutura do projeto

```text
GestaoDeEquipamentoss.Web/
├── Controllers/          # Ações MVC para fabricantes, equipamentos e chamados
├── Models/               # ViewModels usados pelas páginas
├── ModuloChamado/        # Entidade e repositório de chamados
├── ModuloEquipamento/    # Entidade e repositório de equipamentos
├── ModuloFabricante/     # Entidade e repositório de fabricantes
├── Compartilhado/        # Entidades base, contratos e persistência JSON
├── Views/                # Páginas Razor
└── wwwroot/              # Arquivos estáticos, como CSS
```

## Navegação

Ao iniciar a aplicação, use a barra de navegação para acessar os controles de fabricantes, equipamentos e chamados.
