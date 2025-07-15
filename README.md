# TaskPilot
TaskPilot é uma API moderna e intuitiva para gerenciamento de tarefas, criada para ajudar equipes a organizar, monitorar e colaborar em suas atividades diárias.

## 📋 Fase 2: Refinamento — Perguntas para o PO

Durante o planejamento das próximas fases e melhorias, é fundamental alinhar as expectativas com o PO. Seguem algumas perguntas que ajudarão a refinar as funcionalidades e definir prioridades futuras:

1. **Gerenciamento de Tarefas e Projetos**
   - Devemos permitir reatribuição de tarefas entre projetos diferentes?
   - Haverá necessidade de definir dependências entre tarefas (por exemplo: tarefa B só inicia após conclusão da tarefa A)?
   - Deveremos implementar um campo de estimativa de esforço (em horas ou pontos) em cada tarefa?

2. **Controle de Prioridade**
   - Futuramente, podemos querer permitir a alteração de prioridade (hoje é bloqueado)? Em quais cenários seria permitido?

3. **Histórico e Auditoria**
   - Precisamos disponibilizar uma interface ou relatório visual para o histórico de alterações? Ou somente manter para auditoria interna?
   - O histórico deve registrar também visualizações ou apenas alterações efetivas?

4. **Relatórios e Métricas**
   - Quais métricas adicionais seriam relevantes no relatório de desempenho (por exemplo: tempo médio para conclusão, tarefas reabertas)?
   - Devemos considerar relatórios customizáveis por período ou filtros avançados (por exemplo: por prioridade, por status)?

5. **Notificações e Alertas**
   - Haverá integração com sistemas de notificação (e-mail, Slack, etc.) para avisos de novas tarefas ou alterações?
   - Desejamos implementar lembretes automáticos para tarefas próximas do vencimento?

6. **Permissões e Acesso**
   - Além do papel de "gerente", teremos outros níveis ou papéis com permissões diferenciadas no futuro?
   - Haverá necessidade de controlar permissões específicas em nível de tarefa ou projeto?

7. **Integrações externas**
   - Há planos de integrar o sistema com outras ferramentas de gestão ou APIs externas (ex.: Jira, Asana, Trello)?
   - Planejamos disponibilizar uma API pública para consulta de dados?

8. **Melhorias de usabilidade**
   - Existe alguma prioridade para funcionalidades offline ou suporte a dispositivos móveis?
   - Devemos oferecer edição em massa (bulk edit) de tarefas ou projetos?
  
   ## 🚀 Fase 3: Final — Pontos de melhoria e visão futura

Na etapa final, e considerando o pouco tempo de desenvolvimento, pensei em algumas coisas que podem ser implementandas:

- Logs e Tratamento adequado de exceção
- Segregação de validações de dominio
- Possibilidade de trabalhar com eventos e jobs (incluir algum cloud)
- Ver a possbilidade de trabalhar com lifetime Transient (DI)
- Usar automapper
- Criar testes funcionais e de integração
- usar ferramentar de observability
- Algumas outras melhorias que podem ser feitas numa possivel evolução.

  ## 🐳 Executando o projeto com Docker

O projeto foi preparado para rodar em ambiente containerizado usando Docker. Assim, você garante a mesma configuração independente do sistema operacional ou ambiente local.

---

### ✅ Pré-requisitos

- Docker instalado ([Instalar Docker](https://docs.docker.com/get-docker/))
- Docker Compose instalado (já incluso no Docker Desktop)

---

### ⚙️ Passo a passo

#### 1️⃣ Clone o repositório

```bash
git clone https://github.com/marcelops88/TaskPilot.git
cd seu-repositorio

#### 2️⃣ Execute o Docker Compose
docker-compose up --build -d

Isso irá criar e iniciar todos os containers definidos no docker-compose.yml (API, banco).

A API ficará disponível em: http://localhost:8080

Swagger: http://localhost:8080/swagger/index.html









