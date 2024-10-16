using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Model;
using WebApplication1.Model.Interfaces;

namespace WebApplication1.Business
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;

        public StudentBusiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student Insert(Student student)
        {
            // Validação do nome do estudante
            if (string.IsNullOrWhiteSpace(student.StudentName))
            {
                throw new Exception("Student name cannot be empty."); // Lançando uma exceção personalizada
            }
            _studentRepository.Insert(student);
            return student;
        }

        public List<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public Student Get(int id)
        {
            Student student = _studentRepository.Get(id);
            if (student == null)
            {
                throw new Exception($"There is not a student with the id: {id}. Try again.");
            }
            return student;
        }

        public bool Update(Student student)
        {
            if (student.Id == 0 || !_studentRepository.CheckIfInserted(student.Id))

            //if (student.Id.Equals(Guid.Empty) || !_studentRepository.CheckIfInserted(student.Id))
            {
                throw new Exception("Please, inform a valid student.");
            }

            // Validação do nome do estudante
            if (string.IsNullOrWhiteSpace(student.StudentName))
            {
                throw new Exception("Student name cannot be empty."); // Lançando uma exceção personalizada
            }

            _studentRepository.Update(student);
            return true;
        }

        public bool Delete(int id)
        {
            if (id.Equals(Guid.Empty))
            {
                throw new Exception("Please, inform a valid Id.");
            }

            if (!_studentRepository.CheckIfInserted(id))
            {
                throw new Exception($"The student with id {id} was not found. Please, try again.");
            }

            _studentRepository.Delete(id);
            return true;
        }



        public int CalcularSituacao(int id)
        {
            var student = _studentRepository.Get(id);

            if (student == null)
            {
                throw new ArgumentException($"Estudante com ID {id} não encontrado.");
            }

            // Defina a nota mínima de aprovação
            int notaMinimaAprovacao = 60;

            // Verifique se a nota do estudante é maior ou igual à nota mínima de aprovação
            if (student.Nota >= notaMinimaAprovacao)
            {
                // Se a nota for maior ou igual à nota mínima, o estudante está aprovado
                return 1; // 1 representa "Aprovado"
            }
            else
            {
                // Caso contrário, o estudante está reprovado
                return 0; // 0 representa "Reprovado"
            }
        }

        //public bool PodeTerBolsa(int id)
        //{
        //    var student = _studentRepository.Get(id); // Obtemos o estudante pelo ID
        //    if (student == null) // Verificamos se o estudante existe
        //    {
        //        throw new ArgumentException($"student why ID {id} not search.");
        //    }

        //    return student.Nota >= 7; // Retorna true se a média for >= 7, caso contrário, false
        //}


    }
}
