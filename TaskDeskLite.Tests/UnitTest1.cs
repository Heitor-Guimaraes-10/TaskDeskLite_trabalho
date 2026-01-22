using System;
using TaskDeskLite.Core;

namespace TaskDeskLite.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
            var exception = Assert.Throws<TaskDeskLite.Core.DomainValidationException>(() => taskService.Update(task));

            Assert.Equal("Título é obrigatório.", exception.Message);


        }

        [Fact]

        public void Test2()
        {
            // ===== ARRANGE =====
            // Cria uma instância do serviço de tarefas.
            // Aqui a lista interna (_tasks) começa vazia.
            var taskService = new TaskDeskLite.Core.TaskService();

            // Cria um objeto TaskItem com dados de teste.
            // Essa tarefa AINDA NÃO está salva em lugar nenhum.
            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = "Tarefa de teste",
                Description = "Descrição de teste",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            };

            // ===== ACT =====
            // Chama o método Create do serviço.
            // Esse método:
            // - valida a tarefa
            // - gera um Id
            // - define o Status como Pending
            // - adiciona a tarefa na lista interna
            var createdTask = taskService.Create(task);

            // ===== ASSERT =====
            // Verifica se a tarefa criada não é nula
            Assert.NotNull(createdTask);

            // Verifica se o título foi mantido corretamente
            Assert.Equal("Tarefa de teste", createdTask.Title);

            // Verifica se a regra de negócio foi aplicada:
            // toda tarefa criada deve começar com Status = Pending
            Assert.Equal(TaskDeskLite.Core.TaskStatus.Pending, createdTask.Status);
        }

        [Fact]

        public void Test5() 
        {
            var service = new TaskDeskLite.Core.TaskService(); // Serviço de tarefas

            // Cria a tarefa
            var created = service.Create(new TaskDeskLite.Core.TaskItem
            {
                Title = "Tarefa original",
                Description = "Descrição original",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            });

            // Marca como concluída
            service.MarkAsDone(created.Id);

            // Prepara tentativa de atualização
            var updatedTask = new TaskDeskLite.Core.TaskItem // Tarefa com dados alterados
            {
                Id = created.Id, // mantém o mesmo Id
                Title = "Título alterado", // Título alterado
                Description = "Descrição alterada", // Descrição alterada
                Priority = TaskDeskLite.Core.TaskPriority.High // Prioridade alterada
            };

            // ===== ACT & ASSERT =====
            var exception = Assert.Throws<TaskDeskLite.Core.BusinessRuleException>(
                () => service.Update(updatedTask) // Tenta atualizar a tarefa concluída
            );

            Assert.Equal(
                    "Não é permitido alterar uma tarefa concluída.",
                    exception.Message
            );

        }

        [Fact]

        public void Test6() 
        {
            var service = new TaskDeskLite.Core.TaskService(); // Serviço de tarefas

            var task = service.Create(new TaskDeskLite.Core.TaskItem // Cria a tarefa
            {
                Title = "Tarefa para deletar",
                Description = "Descrição original",
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            });

            // ===== ACT =====
            service.Delete(task.Id); // Deleta a tarefa

            // ===== ASSERT =====
            var exception = Assert.Throws<TaskDeskLite.Core.NotFoundException>(
                () => service.GetById(task.Id) // Tenta buscar a tarefa deletada
            );

            Assert.Equal("Tarefa não encontrada.", exception.Message);
        }



        [Fact]

        public void Test3()
        {
            var taskService = new TaskDeskLite.Core.TaskService(); // Serviço de tarefas

            var task = new TaskDeskLite.Core.TaskItem // Tarefa com descrição muito longa
            {
                Title = "Tarefa de teste",
                Description = new string('A', 201), // passa do limite real (200)
                Priority = TaskDeskLite.Core.TaskPriority.Medium
            };

            var exception = Assert.Throws<TaskDeskLite.Core.DomainValidationException>(
                () => taskService.Create(task) // Tenta criar a tarefa inválida
            );

            Assert.Equal(
                "Descrição deve ter no máximo 200 caracteres.",
                exception.Message
            );
        }

        [Fact]

        public void Test4() 
        {
            var taskService = new TaskDeskLite.Core.TaskService(); // Serviço de tarefas

            var task = new TaskDeskLite.Core.TaskItem
            {
                Title = "Tarefa de teste",
                Description = new string('A', 199), // passa do limite real (200)
                Priority = TaskDeskLite.Core.TaskPriority.Medium

            };


            var createdTask = taskService.Create(task); // Cria a tarefa válida

        }
    }
}
