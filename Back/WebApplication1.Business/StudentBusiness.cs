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

        // Construtor que recebe o repositório de estudantes
        public StudentBusiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // Método para inserir um estudante
        public async Task<Student> InsertAsync(Student student)
        {
            ValidateStudent(student); // Valida o estudante
            await _studentRepository.InsertAsync(student); // Insere no repositório
            return student; // Retorna o estudante inserido
        }

        // Método para obter todos os estudantes
        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync(); // Retorna a lista de estudantes
        }

        // Método para obter um estudante pelo ID
        public async Task<Student> GetAsync(int id)
        {
            var student = await _studentRepository.GetAsync(id); // Busca o estudante pelo ID
            if (student == null)
            {
                throw new Exception($"Não existe um estudante com o ID: {id}. Tente novamente."); // Exceção se não encontrado
            }
            return student; // Retorna o estudante
        }

        // Método para atualizar um estudante
        public async Task<bool> UpdateAsync(Student student)
        {
            // Verifica se o estudante existe
            if (student.Id == 0 || !await _studentRepository.CheckIfInsertedAsync(student.Id))
            {
                throw new Exception("Por favor, informe um estudante válido."); // Exceção se inválido
            }

            ValidateStudent(student); // Valida o estudante
            await _studentRepository.UpdateAsync(student); // Atualiza no repositório
            return true; // Retorna verdadeiro se a atualização for bem-sucedida
        }

        // Método para excluir um estudante
        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Por favor, informe um ID válido."); // Exceção se ID inválido
            }

            // Verifica se o estudante existe
            if (!await _studentRepository.CheckIfInsertedAsync(id))
            {
                throw new Exception($"O estudante com o ID {id} não foi encontrado. Por favor, tente novamente."); // Exceção se não encontrado
            }

            await _studentRepository.DeleteAsync(id); // Exclui do repositório
            return true; // Retorna verdadeiro se a exclusão for bem-sucedida
        }

        // Método para calcular a situação do estudante
        public async Task<int> CalcularSituacaoAsync(int id)
        {
            var student = await _studentRepository.GetAsync(id); // Busca o estudante
            if (student == null)
            {
                throw new ArgumentException($"Estudante com ID {id} não encontrado."); // Exceção se não encontrado
            }

            return student.Nota >= 60 ? 1 : 0; // Retorna 1 se aprovado, 0 se reprovado
        }

        // Método privado para validar os dados do estudante
        private void ValidateStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.StudentName))
            {
                throw new Exception("O nome do estudante não pode estar vazio."); // Exceção se o nome estiver vazio
            }
        }
    }
}
