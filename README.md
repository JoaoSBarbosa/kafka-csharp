# 🚀 Kafka Playground (.NET 8)

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![Kafka](https://img.shields.io/badge/Apache%20Kafka-3.x-black)
![Avro](https://img.shields.io/badge/Avro-Schema-blue)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-green)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-brightgreen)

Projeto para estudo de **Apache Kafka com .NET 8**, utilizando **Avro + Schema Registry**, **EF Core**, **Clean Architecture** e **mensageria desacoplada**.

---

## 🧱 Arquitetura

O projeto segue **Clean Architecture**, garantindo:

- Separação total de responsabilidades
- Domínio independente de infraestrutura
- Infra dependente do domínio (nunca o contrário)
- Comunicação via **Ports (interfaces)**

```

src
├── KafkaPlayground.Api
├── KafkaPlayground.Application
│   ├── Contracts
│   ├── UseCases
│   ├── DTOs
│   └── Ports
├── KafkaPlayground.Domain
│   ├── Entities
│   ├── Events
│   └── Enums
├── KafkaPlayground.Infra
│   ├── Persistence
│   │   ├── Context
│   │   ├── Configurations
│   │   └── Repositories
│   ├── Messaging
│   │   └── Kafka
│   │       ├── Producer
│   │       └── Consumer
│   └── DependencyInjection
└── KafkaPlayground.Worker   👈 Consumer (HostedService)

````

---

## 🧠 Responsabilidades por camada

### **Domain**
- Entidades
- Eventos de domínio (`UserCreatedEvent`, etc.)
- Enums
- **NÃO conhece Kafka, EF, Avro, Infra**

---

### **Application**
- Casos de uso
- DTOs
- Ports (interfaces)
- Orquestra o domínio
- **Depende apenas do Domain**

---

### **Infra**
- Kafka Producer / Consumer
- Serialização Avro
- EF Core / SQL Server
- Implementa interfaces definidas na Application

---

### **Worker**
- `BackgroundService`
- Consumidor Kafka
- Processa eventos
- Chama UseCases da Application

---

### **Api**
- Entrada HTTP
- Publica eventos no Kafka via Application

---

## 📦 Principais Dependências

```xml
<TargetFramework>net8.0</TargetFramework>

Apache.Avro                              1.12.1
Confluent.Kafka                          2.13.1
Confluent.SchemaRegistry.Serdes.Avro     2.13.1
KafkaFlow.SchemaRegistry                4.1.0
Microsoft.EntityFrameworkCore            8.0.24
Microsoft.EntityFrameworkCore.SqlServer  8.0.24
````

---

## 🔄 Fluxo de Mensagens Kafka

```
API
 └─ Application (UseCase)
     └─ Port (IEventProducer)
         └─ Infra.Kafka.Producer
             └─ Kafka Topic (Avro)
                 └─ Worker (Consumer)
                     └─ Application
                         └─ Domain
```

---

## 📌 Convenções Importantes

* ❌ Domain **NUNCA** referencia Infra
* ❌ Application **NUNCA** referencia Infra
* ✅ Infra referencia Application + Domain
* ✅ Worker referencia Application + Infra
* ✅ API referencia Application

---

## 🧪 Objetivos do Projeto

* Kafka Producer / Consumer real
* Avro + Schema Registry
* Event-driven architecture
* .NET 8 moderno
* Código limpo e profissional
* Base sólida para projetos reais

---

## ⚠️ Observação

Este projeto **não é um monólito acoplado**.
Cada camada pode virar **microserviço** futuramente sem refatoração estrutural.

---

## 🧠 Próximos passos sugeridos

* Dead Letter Topic (DLT)
* Retry com backoff
* Outbox Pattern
* Idempotência no Consumer
* Observabilidade (OpenTelemetry)

---

## 👤 Autor

Projeto de estudos para **Kafka + .NET 8**
Arquitetura focada em **qualidade, clareza e evolução**

