using System;
using System.Collections.Generic;
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
        public void StudentBusiness_Insert_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            _studentRepository.Setup(s => s.Insert(It.IsAny<Student>()));

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var insertedStudent = business.Insert(student);

            // Assert
            Assert.NotNull(insertedStudent);
            Assert.Equal(student.Id, insertedStudent.Id);
        }

        [Fact]
        public void StudentBusiness_GetAll_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            _studentRepository.Setup(s => s.GetAll()).Returns(new List<Student>() { student });

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var students = business.GetAll();

            // Assert
            Assert.NotNull(students);
            Assert.True(students.Count > 0);
        }

        [Fact]
        public void StudentBusiness_Delete_OK()
        {
            // Arrange
            _studentRepository.Setup(s => s.Delete(It.IsAny<int>()));
            _studentRepository.Setup(s => s.CheckIfInserted(It.IsAny<int>())).Returns(true);

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var isDeleted = business.Delete(1); // Substitua 1 pelo ID do cliente que você deseja deletar

            // Assert
            Assert.True(isDeleted);
        }


        [Fact]
        public void StudentBusiness_Update_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            _studentRepository.Setup(s => s.CheckIfInserted(student.Id)).Returns(true); // Mock para verificar que o estudante existe
            _studentRepository.Setup(s => s.Update(It.IsAny<Student>())).Returns(student); // Mock para atualização

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var isUpdated = business.Update(student);

            // Assert
            Assert.True(isUpdated);
        }

        //[Fact]
        //public void StudentBusiness_Update_OK()
        //{
        //    // Arrange
        //    Student student = GenerateDefaultStudent();
        //    _studentRepository.Setup(s => s.Update(It.IsAny<Student>())).Returns((Student updatedStudent) => updatedStudent);


        //    StudentBusiness business = new(_studentRepository.Object);

        //    // Act
        //    var isUpdated = business.Update(student);

        //    // Assert
        //    Assert.True(isUpdated);
        //}

        [Fact]
        public void StudentBusiness_Get_OK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();

            _studentRepository.Setup(s => s.Get(It.IsAny<int>())).Returns(student);

            StudentBusiness business = new(_studentRepository.Object);

            // Act
            var studentObtained = business.Get(1); // Substitua 1 pelo ID do cliente que você deseja obter

            // Assert
            Assert.NotNull(studentObtained);
            Assert.NotEqual(0, studentObtained.Id); // Verifica se o ID obtido é diferente de 0
        }

        [Fact]
        public void StudentBusiness_Insert_InvalidStudent_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            student.StudentName = "";

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => business.Insert(student));
            Assert.Equal("Student name cannot be empty.", exception.Message); // Verifica a mensagem da exceção
            //Assert.Throws<ValidationException>(() => business.Insert(student));
        }

        [Fact]
        public void StudentBusiness_Get_InvalidStudent_NOK()
        {
            // Arrange
            _studentRepository.Setup(s => s.Get(It.IsAny<int>())).Returns((Student)null);

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => business.Get(1)); // Substitua 1 pelo ID do cliente que você deseja obter
        }

        [Fact]
        public void StudentBusiness_Delete_InvalidId_NOK()
        {
            // Arrange
            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => business.Delete(0)); // Substitua 0 pelo ID inválido do cliente que você deseja deletar
        }

        [Fact]
        public void StudentBusiness_Delete_InvalidStudent_NOK()
        {
            // Arrange
            _studentRepository.Setup(s => s.CheckIfInserted(It.IsAny<int>())).Returns(false);

            StudentBusiness business = new(_studentRepository.Object);


            // Act & Assert
            Assert.Throws<Exception>(() => business.Delete(0)); // Substitua 0 pelo ID inválido do cliente que você deseja deletar
        }

        [Fact]
        public void StudentBusiness_Update_InvalidId_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            student.Id = 0; // Define um ID inválido

            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => business.Update(student));
        }

        [Fact]
        public void StudentBusiness_Update_StudentNotExists_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();

            _studentRepository.Setup(s => s.CheckIfInserted(It.IsAny<int>())).Returns(false);
            StudentBusiness business = new(_studentRepository.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => business.Update(student));
        }
        [Fact]
        public void StudentBusiness_Update_InvalidStudent_NOK()
        {
            // Arrange
            Student student = GenerateDefaultStudent();
            student.StudentName = ""; // Nome vazio deve causar erro

            _studentRepository.Setup(s => s.CheckIfInserted(It.IsAny<int>())).Returns(true); // Mock que o estudante existe
            StudentBusiness business = new StudentBusiness(_studentRepository.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => business.Update(student));
            Assert.Equal("Student name cannot be empty.", exception.Message); // Verifica a mensagem da exceção
            //Assert.Throws<ValidationException>(() => business.Update(student)); // Espera-se que lance ValidationException
        }

        //[Fact]
        //public void StudentBusiness_Update_InvalidStudent_NOK()
        //{
        //    // Arrange
        //    Student student = GenerateDefaultStudent();
        //    student.StudentName = "";

        //    _studentRepository.Setup(s => s.CheckIfInserted(It.IsAny<int>())).Returns(true);
        //    StudentBusiness business = new StudentBusiness(_studentRepository.Object);

        //    // Act & Assert
        //    Assert.Throws<ValidationException>(() => business.Update(student));
        //}


        //[Fact]
        //public void StudentBusiness_PodeTerBolsa_OK()
        //{
        //    var student = GenerateDefaultStudent();
        //    student.Nota = 7; // Estudante com nota 7
        //    _studentRepository.Setup(s => s.Get(student.Id)).Returns(student);

        //    var business = new StudentBusiness(_studentRepository.Object);

        //    var resultado = business.PodeTerBolsa(student.Id);

        //    Assert.True(resultado); // Deve retornar true
        //}

        //[Fact]
        //public void StudentBusiness_PodeTerBolsa_NOK()
        //{
        //    var student = GenerateDefaultStudent();
        //    student.Nota = 6; // Estudante com nota 6
        //    _studentRepository.Setup(s => s.Get(student.Id)).Returns(student);

        //    var business = new StudentBusiness(_studentRepository.Object);

        //    var resultado = business.PodeTerBolsa(student.Id);

        //    Assert.False(resultado); // Deve retornar false
        //}


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
