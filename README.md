# Kafka Playground – Producer / Consumer (.NET 8)

## Objetivo

Este repositório demonstra um **fluxo completo de mensageria com Apache Kafka**, utilizando **.NET 8**, **Worker Service**, **ASP.NET Core**, **Entity Framework Core** e **SQL Server**, com separação clara de responsabilidades seguindo princípios de **Clean Architecture**.

O foco do projeto é **entender, testar e validar**:

- Publicação de eventos via API (Producer)
- Consumo assíncrono via Worker (Consumer)
- Persistência de dados em bancos distintos
- Comunicação entre serviços exclusivamente via Kafka

---

## Escopo

✔ Comunicação assíncrona
✔ Event-driven architecture
✔ Producer desacoplado do Consumer
✔ Contratos compartilhados e imutáveis
✔ Persistência independente por serviço

❌ Não é um projeto de produção
❌ Não possui autenticação
❌ Não possui testes automatizados

---

## Estrutura do Projeto

├── src
│   ├── ConsumerService
│   │   ├── Consumer.Application
│   │   ├── Consumer.Domain
│   │   ├── Consumer.Infra
│   │   └── Consumer.Worker
│   ├── ProducerService
│   │   ├── Producer.Api
│   │   ├── Producer.Application
│   │   ├── Producer.Domain
│   │   └── Producer.Infra
│   └── Shared
│       └── Shared.Contracts
├── tests
├── KafkaPlayground.sln
└── README.md

---

## Responsabilidade de Cada Camada

### Shared.Contracts

Contém **somente contratos de comunicação** (eventos Kafka).

- Não depende de nenhuma outra camada
- Usado tanto pelo Producer quanto pelo Consumer
- Eventos devem ser **imutáveis**

Exemplo:

- `UserRegisteredEvent`

---

### ProducerService

Responsável por **receber requisições HTTP** e **publicar eventos no Kafka**.

#### Producer.Api

- Exposição de endpoints HTTP
- Não contém regra de negócio
- Apenas delega para Application

Executável: **SIM**

---

#### Producer.Application

- Casos de uso
- Orquestra domínio, persistência e mensageria
- Define portas (`IEventPublisher`, `IUnitOfWork`, etc.)

Executável: **NÃO**

---

#### Producer.Domain

- Entidades
- Enums
- Regras de negócio puras

Executável: **NÃO**

---

#### Producer.Infra

- Implementações concretas
- EF Core
- Kafka Producer
- Repositórios

Executável: **NÃO**

---

### ConsumerService

Responsável por **consumir eventos do Kafka** e **processá-los de forma assíncrona**.

---

#### Consumer.Worker

- Worker Service (.NET)
- Mantém o processo vivo
- Inicializa o consumo Kafka

Executável: **SIM**

---

#### Consumer.Application

- Handlers de eventos
- Casos de uso disparados pelo Kafka
- Coordena persistência e regras

Executável: **NÃO**

---

#### Consumer.Domain

- Entidades de processamento
- Estados e resultados
- Regras de domínio

Executável: **NÃO**

---

#### Consumer.Infra

- Kafka Consumer
- EF Core
- Repositórios
- Unit of Work

Executável: **NÃO**

---

## Fluxo de Funcionamento

### 1. Producer

1. Cliente faz requisição HTTP
2. Controller recebe a requisição
3. Application executa o caso de uso
4. Dados são persistidos
5. Evento é publicado no Kafka

---

### 2. Kafka

- Evento `UserRegisteredEvent` é enviado para o tópico configurado
- Kafka atua como intermediário desacoplado

---

### 3. Consumer

1. Worker inicia
2. KafkaConsumer se inscreve no tópico
3. Mensagem é consumida
4. Handler processa o evento
5. Resultado é persistido no banco do Consumer
6. Offset é commitado manualmente

---

## Execução do Projeto

### Pré-requisitos

- Docker
- .NET SDK 8
- SQL Server
- Kafka (via Docker)

---

### Subir Infraestrutura

```bash
docker compose up -d
```

---

### Executar Consumer

```bash
dotnet run --project src/ConsumerService/Consumer.Worker
```

---

### Executar Producer

```bash
dotnet run --project src/ProducerService/Producer.Api
```

---

## Ordem Correta de Execução

1. Kafka (Docker)
2. Consumer.Worker
3. Producer.Api
4. Enviar requisição HTTP

---

## Observações Técnicas Importantes

- Worker Service é **Singleton**
- KafkaConsumer é resolvido via **IServiceScope**
- DbContext é **Scoped**
- Offset é commitado **após sucesso no processamento**
- Consumer **não conhece Producer**
- Comunicação é **exclusivamente via Kafka**

---

## Motivações Arquiteturais

- Evitar acoplamento direto entre serviços
- Permitir escalabilidade independente
- Simular cenário real de mensageria
- Facilitar entendimento de Kafka + .NET

---

## Estado do Projeto

✔ Kafka funcionando
✔ Producer publicando eventos
✔ Consumer consumindo eventos
✔ Persistência validada
✔ Arquitetura consistente

---

## Considerações Finais

Este projeto serve como **base de estudo e experimentação** para:

- Kafka
- Worker Services
- Arquitetura orientada a eventos
- Clean Architecture em .NET

Não contém otimizações ou hardening de produção.


## Diagrama de Arquitetura (Fluxo Real)

```mermaid
flowchart LR
    %% CLIENTE
    Client[Cliente HTTP]

    %% PRODUCER
    subgraph ProducerService
        API[Producer.Api]
        APP[Producer.Application]
        DOMAIN[Producer.Domain]
        INFRA_P[Producer.Infra]
        DB_P[(SQL Server - Producer)]
    end

    %% KAFKA
    subgraph Kafka
        TOPIC[(UserRegisteredEvent)]
    end

    %% CONSUMER
    subgraph ConsumerService
        WORKER[Consumer.Worker]
        KAFKA_C[KafkaConsumerWorker]
        APP_C[Consumer.Application]
        DOMAIN_C[Consumer.Domain]
        INFRA_C[Consumer.Infra]
        DB_C[(SQL Server - Consumer)]
    end

    %% FLUXO PRODUCER
    Client --> API
    API --> APP
    APP --> DOMAIN
    APP --> INFRA_P
    INFRA_P --> DB_P
    INFRA_P --> TOPIC

    %% FLUXO CONSUMER
    WORKER --> KAFKA_C
    KAFKA_C --> TOPIC
    KAFKA_C --> APP_C
    APP_C --> DOMAIN_C
    APP_C --> INFRA_C
    INFRA_C --> DB_C
````

---

## Diagrama de Sequência (Execução)

```mermaid
sequenceDiagram
    participant C as Cliente
    participant API as Producer.Api
    participant APP as Producer.Application
    participant KP as Kafka Producer
    participant K as Kafka
    participant KC as Kafka Consumer
    participant H as UserRegisteredEventHandler
    participant DB as SQL Consumer

    C->>API: POST /users
    API->>APP: RegisterUser
    APP->>KP: Publish UserRegisteredEvent
    KP->>K: Evento no tópico

    KC->>K: Consume
    K-->>KC: UserRegisteredEvent
    KC->>H: Handle(event)
    H->>DB: Persist UserProcessingResult
```
