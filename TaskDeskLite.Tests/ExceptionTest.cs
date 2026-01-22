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

    }
}
