using HospitalAppointmentBooking.Domain.Entities;
using HospitalAppointmentBooking.Application.Interfaces;

namespace HospitalAppointmentBooking.Application.Services
{
    public class PatientService : IServices<Patient>
    {
        private readonly IRepository<Patient> _repository;

        public PatientService(IRepository<Patient> repository)
        {
            _repository = repository;
        }

        public void Add(Patient entity) => _repository.Add(entity);

        public void Delete(int id) => _repository.Delete(id);

        public List<Patient> GetAll() => _repository.GetAll();

        public Patient GetById(int id) => _repository.GetById(id);

        public void Update(Patient entity) => _repository.Update(entity);
    }
}
