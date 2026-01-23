using System.Security.Cryptography.X509Certificates;
using TaskDeskLite.Core;


namespace TaskDeskLite.Tests
{
    public class Exception
    {
        [Fact]
        public void CriarTarefa_ComTituloVazio_DeveLancarErro()
        {
            // Arrange
            var taskService = new TaskDeskLite.Core.TaskService();
            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = "", // Título vazio para forçar o erro
                Description = "Descrição de teste",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            };

            // Act & Assert
            // AJUSTADO: Agora usamos DomainValidationException, que é o nome real no seu Core
            Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => taskService.Create(task));
        }
    
    
        [Fact]
        public void CriarTarefa_comTitulo_QuePassaoLimitedeCaracteres()
        {
            // Arrange
            var taskService = new TaskDeskLite.Core.TaskService();
            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = new string('A', 41), // Título Numero de caracteres maximo
                Description = "Descrição de teste",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            };

            // Act & Assert
            // AJUSTADO: Agora usamos DomainValidationException, que é o nome real no seu Core
            Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => taskService.Create(task));
        }
        [Fact]
        public void CriarTarefa_comTitulo_MenorQueLimitedeCaracteres()
        {
            // Arrange
            var taskService = new TaskDeskLite.Core.TaskService();
            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = new string('A', 2), // Título vazio para forçar o erro
                Description = "Descrição de teste",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            };

            // Act & Assert
            // AJUSTADO: Agora usamos DomainValidationException, que é o nome real no seu Core
            Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => taskService.Create(task));
        }
       
        [Fact]
        public void CriarTarefa_comTitulo_Palavraproibida()
        {
            // Arrange
            var taskService = new TaskDeskLite.Core.TaskService();
            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = "hack o sistema", // Título vazio para forçar o erro
                Description = "Descrição de teste",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            };

            // Act & Assert
            // AJUSTADO: Agora usamos DomainValidationException, que é o nome real no seu Core
            Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => taskService.Create(task));
        }
        [Fact]
        public void Criar_TarefaComDataPassada_DeveLancarErro()
        {
            //Arrange
            var service = new TaskService();
            var tarefaInvalida = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Tarefa Inválida",
                Description = "Descrição da tarefa inválida",
                DueDate = DateTime.Now.AddDays(-5), // Data de 5 dias atras
            };

            //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
            Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => service.Create(tarefaInvalida));

        }
        [Fact]
        public void CriarTarefa_SemPrioridade()
        {
            // Arrange
            var taskService = new TaskDeskLite.Core.TaskService();
            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = new string('A', 4),
                Description = "Descrição de teste",
                // Força um valor fora do intervalo do Enum para testar a validação de integridade.
                Priority = (TaskDeskLite.Core.TaskPriority)99
            };

            // Act & Assert
            // AJUSTADO: Agora usamos DomainValidationException, que é o nome real no seu Core
            Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => taskService.Create(task));
        }
 
        
            [Fact]
            public void Atualizar_TarefaComDataPassada_DeveLancarErro()
            {
                //Arrange
                var service = new TaskService();
                var tarefaInvalida = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Tarefa Inválida",
                    Description = "Descrição da tarefa inválida",
                    DueDate = DateTime.Now.AddDays(-5), // Data de 5 dias atras
                };

                //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
                Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => service.Update(tarefaInvalida));

            }

            [Fact]

            public void Atualizar_TarefaComTituloVazio_DeveLancarErro()
            {
                //Arrange
                var service = new TaskService();
                var tarefaInvalida = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "", // Título vazio
                    Description = "Descrição da tarefa inválida",
                    DueDate = DateTime.Now.AddDays(5),
                };
                //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
                Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => service.Update(tarefaInvalida));
            }

            [Fact]
            public void Atualizar_comTitulo_QuePassaoLimite_Mininimo_de_Caracteres_DeveLancarErro()
            {
                //Arrange
                var service = new TaskService();
                var tarefaInvalida = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "Ti", // Título com 2 caracteres (limite mínimo é 3)
                    Description = "Descrição da tarefa inválida",
                    DueDate = DateTime.Now.AddDays(5),
                };
                //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
                Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => service.Update(tarefaInvalida));
            }
            [Fact]
            public void Atualizar_comTitulo_QuePassaoLimite_Maximo_de_Caracteres_DeveLancarErro()
            {
                //Arrange
                var service = new TaskService();
                var tarefaInvalida = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = new string('A', 41), // Título com 2 caracteres (limite mínimo é 3)
                    Description = "Descrição da tarefa inválida",
                    DueDate = DateTime.Now.AddDays(5),
                };
                //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
                Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => service.Update(tarefaInvalida));
            }

            [Fact]

            public void Atualizar_TarefaComTituloComPalavraProibida_DeveLancarErro()
            {
                //Arrange
                var service = new TaskService();
                var tarefaInvalida = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = "hack nivel max", // Título com palavra proibida
                    Description = "Descrição da tarefa inválida",
                    DueDate = DateTime.Now.AddDays(5),
                };
                //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
                Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => service.Update(tarefaInvalida));
            }

            [Fact]
            public void Atualizar_TarefaConcluida_DeveLancarErro()
            {
                //Arrange
                var service = new TaskService();
                
                //Dados da tarefa
                var tarefaInicial = new TaskItem
                {
                    Title = "Tarefa Original",
                    Priority = TaskPriority.Medium
                };
                
               //Cria a tarefa, só que ela ja vem como pendente
                var criada = service.Create(tarefaInicial);

                // Aqui mudamos o status para Done usando este método
                service.MarkAsDone(criada.Id);

                //Agora tentamos atualizar a tarefa concluída
                var tarefaAtualizada = new TaskItem
                {
                    Id = criada.Id,
                    Title = "Tarefa Concluída Atualizada",
                    Description = "Descrição atualizada",
                    DueDate = DateTime.Now.AddDays(10),
                };
                //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
                Assert.Throws<TaskDeskLite.Core.BusinessRuleException>(() => service.Update(tarefaAtualizada));
            }
        }
    }


