# ATS (Applicant Tracking System)

[![CI](https://github.com/RodriguesCesar/Ats/actions/workflows/ci.yml/badge.svg)](https://github.com/RodriguesCesar/Ats/actions/workflows/ci.yml)

Projeto de estudo para demonstrar arquitetura moderna com **.NET + Angular**, aplicando práticas de **Clean Architecture, DDD e CQRS**.

> Este repositório está em evolução para padrão profissional de mercado (ver [Issue #1](https://github.com/RodriguesCesar/Ats/issues/1)).

---

## Visão geral da arquitetura

- **Backend**: .NET 6, API minimal, separação por camadas (Domain/Application/Infrastructure/Api).
- **Frontend**: Angular 17.
- **Testes**: unitários e integração no backend, testes no frontend.
- **Containerização**: Dockerfiles e compose para execução local.

Estrutura atual do repositório:

- `backend/` (backend)
- `frontend/Ats.web/` (frontend)

---

## Pré-requisitos

- .NET SDK 6.x
- Node.js 20+
- npm 10+
- Docker (opcional para execução containerizada)

---

## Como rodar localmente

### 1) Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet test
```

Para executar API:

```bash
cd backend/src/Api
dotnet run
```

### 2) Frontend

```bash
cd frontend/Ats.web
npm ci
npm run build
npm test -- --watch=false --browsers=ChromeHeadless
npm start
```

Aplicação frontend padrão: `http://localhost:4200`

---

## Qualidade e CI

Este projeto possui pipeline no GitHub Actions com:

- Build e testes do backend
- Build e testes do frontend

Workflow: [`.github/workflows/ci.yml`](.github/workflows/ci.yml)

---

## Roadmap curto

- [ ] Fortalecer documentação técnica e diagramas
- [ ] Aumentar cobertura de testes e quality gates
- [ ] Evoluir observabilidade e segurança de borda

---

## Contato

- GitHub: [@RodriguesCesar](https://github.com/RodriguesCesar)
- LinkedIn: [Cesar Rodrigues de Sousa](https://www.linkedin.com/in/cesar-rodrigues-de-sousa-b69849162/)