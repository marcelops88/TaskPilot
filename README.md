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

### 📌 Arquitetura e Padrões

- **Validações avançadas no domínio**:  

  Migrar algumas regras de negócio hoje concentradas no Use Case (por exemplo, restrições de prioridade ou limites) para o domínio (entidades ou value objects) garantiria maior consistência e centralização das regras.

### ☁️ Cloud & Escalabilidade

- **Preparar para deploy em nuvem (Cloud-Ready)**:  
  Avaliar containerização (Docker) e provisionamento em Kubernetes para escalar por demanda. Além disso, pensar em usar serviços gerenciados (por exemplo, Azure SQL ou AWS RDS) para banco de dados, e fila (SQS/Azure Service Bus) para processamento assíncrono de eventos.

- **Monitoramento e observabilidade**:  
  Incluir rastreabilidade via Application Insights, Elastic Stack ou Prometheus + Grafana. Isso ajudaria a detectar gargalos ou falhas em tempo real e melhorar a experiência de manutenção.

### 🔥 Logs & Controle de exceção

- **Implementar logging estruturado**:  
  Adotar frameworks como Serilog ou NLog para logs estruturados, armazenando informações ricas (correlação de requests, payloads críticos, erros detalhados).

- **Padronizar tratamento de exceções**:  
  Criar um middleware global de exception handling, retornando respostas amigáveis para o cliente e garantindo rastreabilidade nos logs (ex.: IDs de correlação).

- **Alertas automáticos**:  
  Configurar alertas baseados em logs críticos (ex.: falhas em criação de tarefas, erros de persistência) para acionar o time rapidamente.

### 💡 Clean Code & Evolutividade

- **Camada de Application Services / Orquestração**:  
  Alguns use cases podem ser orquestrados em "Application Services", facilitando a composição de fluxos mais complexos (por exemplo: criação de tarefa + envio de notificação).

- **Refinar injeção de dependência**:  
  Atualmente usamos serviços Scoped; podemos avaliar casos onde Transient seja mais adequado.

- **Automatização de mapeamentos**:  
  Hoje os DTOs são mapeados manualmente. Adotar AutoMapper ou Source Generators pode reduzir repetição, melhorar legibilidade e diminuir erros.

### 🔒 Segurança & Autorização

- **Melhorar a estratégia de autenticação/autorização**:  
  Atualmente temos regras básicas para o papel de gerente. Podemos evoluir para uma política baseada em claims mais granular, permitindo cenários mais ricos como permissões específicas em nível de projeto ou tarefa.

### 🛠️ Testes & Qualidade

- **Cobertura maior de testes unitários e integração**:  
  Cobrir regras de negócio (por exemplo: limites de tarefas, bloqueio de exclusão de projeto com pendências). Também validar fluxo end-to-end, por exemplo com testes usando xUnit + FakeItEasy/Moq.

- **Testes de carga (Load & Stress)**:  
  Simular múltiplas criações e atualizações concorrentes, garantindo consistência e estabilidade do sistema.




