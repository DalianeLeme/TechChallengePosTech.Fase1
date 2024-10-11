<h1 align="center"> Tech Challenge - P�s Tech </h1>

![descri��o da imagem](https://cdn.discordapp.com/attachments/1206423657648361555/1294012878399209513/Banners_do_Canva_Docs_e_Texto_Magico_no_5.jpg?ex=670976e7&is=67082567&hm=39db7442964b6cab92b1770f47a9b76ac6909c161413d6001f3f005c6fc94771&
)

![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)
<br>![GitHub Org's stars](https://img.shields.io/github/stars/DalianeLeme?style=social)</br>

# �ndice 

* [Descri��o do Projeto](#Descri��o-do-projeto)
* [Fase 1](#Fase-1)
* [Fase 2](#Fase-2)
* [Fase 3](#Fase-3)
* [T�cnicas e tecnologias utilizadas](#T�cnicas-e-tecnologias-utilizadas)

# :pushpin: Descri��o do projeto
API em .NET8 feita para entrega dos Tech Chanllenges da P�s Tech FIAP. <br>
Um aplicativo de gerenciamento de contatos regionais. </br>
O projeto � divido em 5 fases, cada fase vai representar uma evolu��o do projeto, implementando novos comportamentos e funcionalidades.
<br></br>

# Fase 1
Fase inicial onde � criado o projeto e suas camadas, junto cim a persist�ncia de dados.

- `Estrutura`: Uso de DDD (Domain Drive Design) contendo as camadas: </br>
:bookmark_tabs:API: controllers </br>
:bookmark_tabs:Application: services e valida��es </br>
:bookmark_tabs:Domain: Models de requests e responses </br>
:bookmark_tabs:Infrastructure: conex�o com o banco, implement��o do EF </br>

- `Endpoints`: Foram criados os 4 endpoints para cadastro, consulta, atualiza��o e exclus�o de contatos. </br>
:small_red_triangle_down: `POST` /Contacts/Create: Cria��o do contato </br>
:small_red_triangle_down: `GET` /Contacts/GetAllContacts: Consulta de contatos com filtro por DDD </br>
:small_red_triangle_down: `PUT` /Contacts/Update: Atualiza��o do contato por Id </br>
:small_red_triangle_down: `DELETE` /Contacts/Delete/{id}: Exclus�o do contato por Id </br>

- `Banco de dados`: Foi usado o banco SQL Server e para a persist�ncia de dados foi usado o Entity Framework, contando com uma estrutura de tabelas relacionando contatos com seus DDDs e a regi�o de cada DDD. </br>
:file_folder: Tabela Contacts </br>
:file_folder: Tabela DDDs </br>
:file_folder: Tabela Regions </br>

- `Valida��es`: Usada a lib FluentValidation, validando cada dado da requisi��o e impedindo a manipula��o errada de dados. Foi criado um modelo de valida��o para cada m�todo http. </br>
:small_orange_diamond: CreateContactRequestValidator </br>
:small_orange_diamond: GetContactRequestValidator </br>
:small_orange_diamond: UpdateContactRequestValidator </br>

- `Testes unit�rios`: Testes unit�rios foram feitos em xUnit para cada camada do projeto. </br>
:hammer: TechChallenge.Application.UnitTests </br>
:hammer: TechChallenge.Domain.UnitTests </br>
:hammer: TechChallenge.Infrastructure.UnitTests 
<br></br>

# Fase 2
Implementando pr�ticas de Integra��o cont�nua (CI), testes de integra��o e monitoramento de performance. </br>

- `Pipelines`: Foi integrada a pipeline CI no GitHub Actions, com os jobs: </br>
:clock1230: build: compilar o projeto e garantir que n�o h� erros. </br>
:clock1230: unit_tests: executar os testes unit�rios. </br>
:clock1230: integration_tests: executar os testes de integra��o. </br>

- `Prometheus`: Integrei o prometheus para coletar as m�tricas do aplicativo, criado o endpoint /metrics. </br>
:small_red_triangle_down: contacts_request_duration_milliseconds: para calcular a lat�ncia das requisi��es. </br>
:small_red_triangle_down: cpu_usage_percentage: gauge para medir o uso da CPU. </br>
:small_red_triangle_down: memory_usage_bytes: gauge para medir o uso da mem�ria. </br>
:small_red_triangle_down: contacts_http_requests_total: para calcular quantidade de requis���es por status code. </br>

- `Grafana`: Cria��o do dashboard com os pain�is para monitoramento das m�tricas coletadas pelo prometheus. </br>

- `Testes de Integra��o`: Testes para verificar o funcionamento correto da integra��o com o banco de dados. </br>
:mag_right: TechChallenge.Application.IntegrationTests
<br></br>

# Fase 3
:construction:  Projeto em constru��o  :construction:
<br></br>


# :heavy_check_mark: T�cnicas e tecnologias utilizadas
`.NET8` `C#` `SQL Server` `GitHub Actions` `Prometheus` `Grafana` `Testes unit�rios` `Testes de Integra��o` `xUnit` `EntityFramework`
`FluentValidator`