import React, { useState, useEffect } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import logoCadastro from './assets/e.png';
import moment from 'moment';
import { Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

function App() {
  const baseUrl = "https://localhost:7184/Students"; // URL base para alunos
  const [data, setData] = useState([]); // Estado para armazenar alunos
  const [loading, setLoading] = useState(true); // Estado para controle de loading
  const [error, setError] = useState(null); // Estado para armazenar erros
  const [modal, setModal] = useState(false); // Estado para controle do modal de alunos
  const [aluno, setAluno] = useState({ id: 0, studentName: '', dateOfBirth: '', height: '', weight: '', nota: '', subjects: [] }); // Estado para aluno
  const [subjectModal, setSubjectModal] = useState(false); // Estado para controle do modal de matérias
  const [subject, setSubject] = useState({ id: 0, subjectName: '' }); // Estado para matéria
  const [allSubjects, setAllSubjects] = useState([]); // Estado para armazenar todas as matérias

  // Função para alternar a exibição do modal de aluno
  const toggleModal = () => setModal(!modal);
  
  // Função para alternar a exibição do modal de matéria
  const toggleSubjectModal = () => setSubjectModal(!subjectModal);

  // Função para buscar alunos
  const pedidoGet = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await axios.get(baseUrl);
      setData(response.data);
    } catch (error) {
      console.error(error);
      setError("Erro ao carregar os dados.");
    } finally {
      setLoading(false);
    }
  };

  // Função para buscar matérias
  const getAllSubjects = async () => {
    try {
      const response = await axios.get("https://localhost:7184/Subjects");
      console.log("Matérias recebidas:", response.data); // Log da resposta da API
      setAllSubjects(response.data);
    } catch (error) {
      console.error("Erro ao buscar as matérias:", error);
    }
  };

  useEffect(() => {
    pedidoGet();
    getAllSubjects(); // Carregar as matérias disponíveis
  }, []);

  // Handle para submit do aluno
  const handleSubmit = async () => {
    try {
      if (aluno.id) {
        await axios.put(`${baseUrl}/${aluno.id}`, aluno);
      } else {
        await axios.post(baseUrl, aluno);
      }
      toggleModal();
      pedidoGet();
    } catch (error) {
      console.error("Erro ao salvar aluno:", error);
    }
  };

  // Handle para submit da matéria
  const handleSubmitSubject = async () => {
    try {
      if (subject.id) {
        await axios.put(`https://localhost:7184/Subjects/${subject.id}`, subject);
      } else {
        await axios.post("https://localhost:7184/Subjects", subject);
      }
      toggleSubjectModal();
      getAllSubjects(); // Atualizar lista de matérias após criar/editar
    } catch (error) {
      console.error("Erro ao salvar matéria:", error);
    }
  };

  const handleEdit = (aluno) => {
    setAluno(aluno);
    toggleModal();
  };

  const handleDelete = async (id) => {
    try {
      await axios.delete(`${baseUrl}/${id}`);
      pedidoGet();
    } catch (error) {
      console.error("Erro ao excluir aluno:", error);
    }
  };

  const handleDeleteSubject = async (id) => {
    try {
      await axios.delete(`https://localhost:7184/Subjects/${id}`);
      getAllSubjects();
    } catch (error) {
      console.error("Erro ao excluir matéria:", error);
    }
  };

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div className="App">
      <br />
      <h3>Cadastro de Alunos</h3>
      <header>
        <img src={logoCadastro} alt="e" />
        <button className="btn btn-success" onClick={() => { setAluno({ id: 0, studentName: '', dateOfBirth: '', height: '', weight: '', nota: '', subjects: [] }); toggleModal(); }}>Incluir novo Aluno</button>
        <button className="btn btn-info" onClick={toggleSubjectModal}>Inserir Nova Matéria</button>
      </header>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Id</th>
            <th>StudentName</th>
            <th>DateOfBirth</th>
            <th>Height</th>
            <th>Weight</th>
            <th>Nota</th>
            <th>Matérias</th> {/* Nova coluna de matérias */}
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {data.length > 0 ? (
            data.map(aluno => (
              <tr key={aluno.id}>
                <td>{aluno.id}</td>
                <td>{aluno.studentName}</td>
                <td>{moment(aluno.dateOfBirth).format('DD/MM/YYYY')}</td>
                <td>{aluno.height} cm</td>
                <td>{aluno.weight} kg</td>
                <td>{aluno.nota}</td>
                <td>
                  {aluno.subjects && aluno.subjects.length > 0
                    ? aluno.subjects.map(subj => subj.subjectName).join(', ')
                    : 'Sem matérias'}
                </td> {/* Exibe as matérias associadas ao aluno */}
                <td>
                  <button className="btn btn-primary" onClick={() => handleEdit(aluno)}>Editar</button>
                  <button className="btn btn-danger" onClick={() => handleDelete(aluno.id)}>Excluir</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="8">Nenhum aluno encontrado.</td>
            </tr>
          )}
        </tbody>
      </table>

      {/* Modal para criar/editar aluno */}
      <Modal isOpen={modal} toggle={toggleModal}>
        <ModalHeader toggle={toggleModal}> {aluno.id ? 'Editar Aluno' : 'Cadastrar Aluno'} </ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome:</label>
            <input type="text" className="form-control" value={aluno.studentName} onChange={(e) => setAluno({ ...aluno, studentName: e.target.value })} />
          </div>
          <div className="form-group">
            <label>Data de Nascimento:</label>
            <input type="date" className="form-control" value={aluno.dateOfBirth} onChange={(e) => setAluno({ ...aluno, dateOfBirth: e.target.value })} />
          </div>
          <div className="form-group">
            <label>Altura (cm):</label>
            <input type="number" className="form-control" value={aluno.height} onChange={(e) => setAluno({ ...aluno, height: e.target.value })} />
          </div>
          <div className="form-group">
            <label>Peso (kg):</label>
            <input type="number" className="form-control" value={aluno.weight} onChange={(e) => setAluno({ ...aluno, weight: e.target.value })} />
          </div>
          <div className="form-group">
            <label>Nota:</label>
            <input type="number" className="form-control" value={aluno.nota} onChange={(e) => setAluno({ ...aluno, nota: e.target.value })} />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={handleSubmit}>Salvar</button>
          <button className="btn btn-secondary" onClick={toggleModal}>Cancelar</button>
        </ModalFooter>
      </Modal>

      {/* Modal para criar/editar matéria */}
      <Modal isOpen={subjectModal} toggle={toggleSubjectModal}>
        <ModalHeader toggle={toggleSubjectModal}> {subject.id ? 'Editar Matéria' : 'Cadastrar Matéria'} </ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome da Matéria:</label>
            <input 
              type="text" 
              className="form-control" 
              value={subject.subjectName} 
              onChange={(e) => setSubject({ ...subject, subjectName: e.target.value })} 
            />
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={handleSubmitSubject}>Salvar</button>
          <button className="btn btn-secondary" onClick={toggleSubjectModal}>Cancelar</button>
        </ModalFooter>
      </Modal>

      {/* Tabela de Matérias */}
      <h3>Lista de Matérias</h3>
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Id</th>
            <th>Nome da Matéria</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {allSubjects.length > 0 ? (
            allSubjects.map(subject => (
              <tr key={subject.id}>
                <td>{subject.id}</td>
                <td>{subject.subjectName}</td>
                <td>
                  <button className="btn btn-primary" onClick={() => { setSubject(subject); toggleSubjectModal(); }}>Editar</button>
                  <button className="btn btn-danger" onClick={() => handleDeleteSubject(subject.id)}>Excluir</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="3">Nenhuma matéria encontrada.</td>
            </tr>
          )}
        </tbody>
      </table>
    </div>
  );
}

export default App;
