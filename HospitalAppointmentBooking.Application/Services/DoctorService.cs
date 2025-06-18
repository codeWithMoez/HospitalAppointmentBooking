using HospitalAppointmentBooking.Domain.Entities;
using HospitalAppointmentBooking.Application.Interfaces;

namespace HospitalAppointmentBooking.Application.Services
{
    public class DoctorService : IServices<Doctor>
    {
        private readonly IRepository<Doctor> _repository;
        public DoctorService(IRepository<Doctor> repository)
        {
            _repository = repository;
        }

        public void Add(Doctor entity)
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Doctor> GetAll()
        {
            return _repository.GetAll();
        }

        public Doctor GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Doctor entity)
        {
            _repository.Update(entity);
        }
    }
}
