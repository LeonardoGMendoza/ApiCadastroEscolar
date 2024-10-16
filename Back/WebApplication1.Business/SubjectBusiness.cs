using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Model.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Business
{
    public class SubjectBusiness : ISubjectBusiness
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectBusiness(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public Subject Insert(Subject subject)
        {
            if (string.IsNullOrWhiteSpace(subject.SubjectName))
            {
                throw new ArgumentException("The subject name cannot be empty.");
            }
            return _subjectRepository.Insert(subject);
        }

        public List<Subject> GetAll()
        {
            return _subjectRepository.GetAll();
        }

        public Subject Get(int id)
        {
            return _subjectRepository.Get(id);
        }

        public void Delete(int id)
        {
            _subjectRepository.Delete(id);
        }

        public Subject Update(Subject subject)
        {
            return _subjectRepository.Update(subject);
        }
    }
}

