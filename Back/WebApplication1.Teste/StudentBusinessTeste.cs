using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using WebApplication1.Business;
using WebApplication1.Model;
using WebApplication1.Model.Interfaces;
using Xunit;

namespace ProjetoInjecaoDependencia.Teste
{
    public class StudentBusinessTest
    {
        private readonly Mock<IStudentRepository> _studentRepository = new();

        [Fact]
        public async Task StudentBusiness_InsertAsync_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            _studentRepository.Setup(s => s.InsertAsync(It.IsAny<Student>())).Returns(Task.CompletedTask);

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var insertedStudent = await business.InsertAsync(student);

            // Assert
            Assert.NotNull(insertedStudent);
            Assert.Equal(student.Id, insertedStudent.Id);
        }

        [Fact]
        public async Task StudentBusiness_GetAllAsync_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            _studentRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Student> { student });

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var students = await business.GetAllAsync();

            // Assert
            Assert.NotNull(students);
            Assert.True(students.Count > 0);
        }

        [Fact]
        public async Task StudentBusiness_DeleteAsync_OK()
        {
            // Arrange
            _studentRepository.Setup(s => s.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);
            _studentRepository.Setup(s => s.CheckIfInsertedAsync(It.IsAny<int>())).ReturnsAsync(true);

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var isDeleted = await business.DeleteAsync(1); // Substitua 1 pelo ID do cliente que você deseja deletar

            // Assert
            Assert.True(isDeleted);
        }

        [Fact]
        public async Task StudentBusiness_UpdateAsync_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            _studentRepository.Setup(s => s.CheckIfInsertedAsync(student.Id)).ReturnsAsync(true); // Mock para verificar que o estudante existe
            _studentRepository.Setup(s => s.UpdateAsync(It.IsAny<Student>())).ReturnsAsync(student); // Mock para atualização

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var isUpdated = await business.UpdateAsync(student);

            // Assert
            Assert.True(isUpdated);
        }

        [Fact]
        public async Task StudentBusiness_GetAsync_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();

            _studentRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(student);

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var studentObtained = await business.GetAsync(1); // Substitua 1 pelo ID do cliente que você deseja obter

            // Assert
            Assert.NotNull(studentObtained);
            Assert.NotEqual(0, studentObtained.Id); // Verifica se o ID obtido é diferente de 0
        }

        [Fact]
        public async Task StudentBusiness_Insert_InvalidStudent_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            student.StudentName = "";

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await business.InsertAsync(student));
            Assert.Equal("Student name cannot be empty.", exception.Message); // Verifica a mensagem da exceção
        }

        [Fact]
        public async Task StudentBusiness_Get_InvalidStudent_NOK()
        {
            // Arrange
            _studentRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Student)null);

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await business.GetAsync(1)); // Substitua 1 pelo ID do cliente que você deseja obter
        }

        [Fact]
        public async Task StudentBusiness_Delete_InvalidId_NOK()
        {
            // Arrange
            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await business.DeleteAsync(0)); // Substitua 0 pelo ID inválido do cliente que você deseja deletar
        }

        [Fact]
        public async Task StudentBusiness_Delete_InvalidStudent_NOK()
        {
            // Arrange
            _studentRepository.Setup(s => s.CheckIfInsertedAsync(It.IsAny<int>())).ReturnsAsync(false);

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await business.DeleteAsync(0)); // Substitua 0 pelo ID inválido do cliente que você deseja deletar
        }

        [Fact]
        public async Task StudentBusiness_Update_InvalidId_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            student.Id = 0; // Define um ID inválido

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await business.UpdateAsync(student));
        }

        [Fact]
        public async Task StudentBusiness_Update_StudentNotExists_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();

            _studentRepository.Setup(s => s.CheckIfInsertedAsync(It.IsAny<int>())).ReturnsAsync(false);
            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await business.UpdateAsync(student));
        }

        #region Default Values
        private static Student GenerateDefaultStudent()
        {
            return new Student()
            {
                StudentName = "Leonardo",
                Id = 1, // Defina o ID padrão do cliente
                DateOfBirth = new DateTime(1994, 4, 11), // Defina a data de nascimento
                Height = 180, // Defina a altura
                Weight = 75, // Defina o peso
                Nota = 0 // Inicializar a nota
            };
        }
        #endregion
    }
}
