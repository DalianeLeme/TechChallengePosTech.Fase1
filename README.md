﻿<h1 align="center"> Tech Challenge - Pós Tech </h1>

![Capa com o nome do curso da pós graduação](./Assets/capa-readme.jpg)

![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)
<br>![GitHub Org's stars](https://img.shields.io/github/stars/DalianeLeme?style=social)</br>

# Índice 

* [Descrição do Projeto](#Descrição-do-projeto)
* [Fase 1](#Fase-1)
* [Fase 2](#Fase-2)
* [Fase 3](#Fase-3)
* [Técnicas e tecnologias utilizadas](#Técnicas-e-tecnologias-utilizadas)

# :pushpin: Descrição do projeto
API em .NET8 feita para entrega dos Tech Chanllenges da Pós Tech FIAP. <br>
Um aplicativo de gerenciamento de contatos regionais. </br>
O projeto é divido em 5 fases, cada fase vai representar uma evolução do projeto, implementando novos comportamentos e funcionalidades.
<br></br>

# 🥚 Fase 1
Fase inicial onde é criado o projeto e suas camadas, junto cim a persistência de dados.

- `Estrutura`: Uso de DDD (Domain Drive Design) contendo as camadas: </br>
:bookmark_tabs:API: controllers </br>
:bookmark_tabs:Application: services e validações </br>
:bookmark_tabs:Domain: Models de requests e responses </br>
:bookmark_tabs:Infrastructure: conexão com o banco, implementção do EF </br>

- `Endpoints`: Foram criados os 4 endpoints para cadastro, consulta, atualização e exclusão de contatos. </br>
:small_red_triangle_down: `POST` /Contacts/Create: Criação do contato </br>
:small_red_triangle_down: `GET` /Contacts/GetAllContacts: Consulta de contatos com filtro por DDD </br>
:small_red_triangle_down: `PUT` /Contacts/Update: Atualização do contato por Id </br>
:small_red_triangle_down: `DELETE` /Contacts/Delete/{id}: Exclusão do contato por Id </br>

- `Banco de dados`: Foi usado o banco SQL Server e para a persistência de dados foi usado o Entity Framework, contando com uma estrutura de tabelas relacionando contatos com seus DDDs e a região de cada DDD. </br>
:file_folder: Tabela Contacts </br>
:file_folder: Tabela DDDs </br>
:file_folder: Tabela Regions </br>

- `Validações`: Usada a lib FluentValidation, validando cada dado da requisição e impedindo a manipulação errada de dados. Foi criado um modelo de validação para cada método http. </br>
:small_orange_diamond: CreateContactRequestValidator </br>
:small_orange_diamond: GetContactRequestValidator </br>
:small_orange_diamond: UpdateContactRequestValidator </br>

- `Testes unitários`: Testes unitários foram feitos em xUnit para cada camada do projeto. </br>
:hammer: TechChallenge.Application.UnitTests </br>
:hammer: TechChallenge.Domain.UnitTests </br>
:hammer: TechChallenge.Infrastructure.UnitTests 
<br></br>

# :hatching_chick: Fase 2
Implementando práticas de Integração contínua (CI), testes de integração e monitoramento de performance. </br>

- `Pipelines`: Foi integrada a pipeline CI no GitHub Actions, com os jobs: </br>
:clock1230: build: compilar o projeto e garantir que não há erros. </br>
:clock1230: unit_tests: executar os testes unitários. </br>
:clock1230: integration_tests: executar os testes de integração. </br>

- `Prometheus`: Integrei o prometheus para coletar as métricas do aplicativo, criado o endpoint /metrics. </br>
:small_red_triangle_down: contacts_request_duration_milliseconds: para calcular a latência das requisições. </br>
:small_red_triangle_down: cpu_usage_percentage: gauge para medir o uso da CPU. </br>
:small_red_triangle_down: memory_usage_bytes: gauge para medir o uso da memória. </br>
:small_red_triangle_down: contacts_http_requests_total: para calcular quantidade de requisíções por status code. </br>

- `Grafana`: Criação do dashboard com os painéis para monitoramento das métricas coletadas pelo prometheus. </br>
:red_circle: Painéis para monitoramento de Tempo total acumulado gasto em todas as requisições / número total de requisições feitas ao endpoint. Exibindo quantas requisições foram feitas naquele intervalo de latência. 
![Descrição da imagem](./Assets/Latencia-por-requisicoes.png)
:red_circle: Painéis para monitoramento da latência ao longo do tempo, em um rate de 1m. E um painel para exibição da latência média de todas as requisições para todos os endpoints.
![Descrição da imagem](./Assets/Latencia-por-tempo.png)
:red_circle: Painéis para monitoramento do uso de recursos do sistema como CPU e Memória, mais um painel que exibe a contagem de requisições por status code.
![Descrição da imagem](./Assets/metricas-sistema.png)

- `Testes de Integração`: Testes para verificar o funcionamento correto da integração com o banco de dados. </br>
:mag_right: TechChallenge.Application.IntegrationTests
<br></br>

# :hatched_chick: Fase 3
Refatorando API em microsserviços com comunicação por eventos via RabbitMQ </br>
As principais alterações foram:

- `Estrutura`: API virou 5 Microsserviços </br>
:bookmark_tabs:Microsserviço CreateContactService - Publisher </br>
:bookmark_tabs:Microsserviço UpdateContactService - Publisher </br>
:bookmark_tabs:Microsserviço GetContactService - Publisher </br>
:bookmark_tabs:Microsserviço CreateContactService - Publisher </br>
:bookmark_tabs:Microsserviço DataPersistenceService - Consumers </br>

- `RabbitMQ`: A comunicação entre os microsserviços é feita pela mensageria RabbitMQ </br>
:small_orange_diamond: Fila create_contact_queue </br>
:small_orange_diamond: Fila update_contact_queue </br>
:small_orange_diamond: Fila get_contacts_queue </br>
:small_orange_diamond: Fila delete_contact_queue </br>

- `Prometheus`: Agora cada microsserviço tem seu endpoint /metrics, ou seja, cada microsserviço possui suas métricas. </br>
:small_red_triangle_down: Métricas Create: </br>
**create_contact_request_duration_milliseconds:** Latência do endpoint de create </br>
**contacts_create_request_total:** Contagem de requisições por statusCode. </br>
**cpu_usage_percentage_create:** Uso da CPU em tempo real em porcentagem </br>
**memory_usage_bytes_create:** Uso de memória em tempo real em bytes </br>

:small_red_triangle_down: Métricas Update: </br>
**update_contact_request_duration_milliseconds:** Latência do endpoint de update </br>
**contacts_update_request_total:** Contagem de requisições por statusCode. </br>
**cpu_usage_percentage_update:** Uso da CPU em tempo real em porcentagem </br>
**memory_usage_bytes_uppdate:** Uso de memória em tempo real em bytes </br>

:small_red_triangle_down: Métricas Get: </br>
**get_contact_request_duration_milliseconds:** Latência do endpoint de get </br>
**contacts_get_request_total:** Contagem de requisições por statusCode. </br>
**cpu_usage_percentage_get:** Uso da CPU em tempo real em porcentagem </br>
**memory_usage_bytes_get:** Uso de memória em tempo real em bytes </br>

:small_red_triangle_down: Métricas Delete: </br>
**delete_contact_request_duration_milliseconds:** Latência do endpoint de get </br>
**contacts_delete_request_total:** Contagem de requisições por statusCode. </br>
**cpu_usage_percentage_delete:** Uso da CPU em tempo real em porcentagem </br>
**memory_usage_bytes_delete:** Uso de memória em tempo real em bytes </br>

- `Grafana`: Reoprganização dos dashboards para exibição por microsserviços. As visualizalizações continuam as mesmas mudando apenas que é por microsserviço agora. </br>
:red_circle:Create:
![Descrição da imagem](./Assets/GrafanaCreate.png)
:red_circle:Update: 
![Descrição da imagem](./Assets/GrafanaUpdate.png)
:red_circle:Get:
![Descrição da imagem](./Assets/GrafanaGet.png)
:red_circle:Delete:
![Descrição da imagem](./Assets/GrafanaDelete.png)
<br></br>

# :chicken: Fase 4
:construction: Em construção :construction:

# :heavy_check_mark: Técnicas e tecnologias utilizadas
`.NET8` `C#` `SQL Server` `GitHub Actions` `Prometheus` `Grafana` `Testes unitários` `Testes de Integração` `xUnit` `EntityFramework`
`FluentValidator` `RabbitMq` `Eventos`
<br></br>

# :busts_in_silhouette: Autores
[<img loading="lazy" src="https://avatars.githubusercontent.com/u/55365144?v=4" width=115><br><sub>Daliane Leme</sub>](https://github.com/DalianeLeme)
<br></br>
