using System.Security.Cryptography.X509Certificates;
using TaskDeskLite.Core;

namespace TaskDeskLite.Tests
{
    public class Exception
    {
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
            // Primeiro, criamos uma tarefa válida e a marcamos como concluída
            var tarefaConcluida = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Tarefa Concluída",
                Description = "Descrição da tarefa concluída",
                DueDate = DateTime.Now.AddDays(5),
                Status = Core.TaskStatus.Done // Marcando como concluída
            };
            // Adiciona a tarefa concluída ao serviço (simulando que já existe)
            service.Create(tarefaConcluida);
            //Agora tentamos atualizar a tarefa concluída
            var tarefaAtualizada = new TaskItem
            {
                Id = tarefaConcluida.Id,
                Title = "Tarefa Concluída Atualizada",
                Description = "Descrição atualizada",
                DueDate = DateTime.Now.AddDays(10),
            };
            //Act & Assert (Verifica se o nosso validator de teste lança a exceção esperada)
            Assert.Throws<TaskDeskLite.Core.BusinessRuleException>(() => service.Update(tarefaAtualizada));
        }
    }
}