using HospitalAppointmentBooking.Domain.Entities;
using HospitalAppointmentBooking.Application.Interfaces;

namespace HospitalAppointmentBooking.Application.Services
{
    public class AppointmentService : IServices<Appointment>
    {
        private readonly IRepository<Appointment> _repository;

        public AppointmentService(IRepository<Appointment> repository)
        {
            _repository = repository;
        }

        public void Add(Appointment entity) => _repository.Add(entity);

        public void Delete(int id) => _repository.Delete(id);

        public List<Appointment> GetAll() => _repository.GetAll();

        public Appointment GetById(int id) => _repository.GetById(id);

        public void Update(Appointment entity) => _repository.Update(entity);
    }
}
