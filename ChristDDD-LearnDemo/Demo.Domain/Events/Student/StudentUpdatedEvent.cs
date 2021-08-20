using System;
using Demo.Domain.Core.Events;
using Demo.Domain.Models;

namespace Demo.Domain.Events
{
    public class StudentUpdatedEvent : Event
    {
        public StudentUpdatedEvent(Student student)
        {
            Id = student.Id;
            Name = student.Name;
            Email = student.Email;
            BirthDate = student.BirthDate;
            Phone = student.Phone;
            Province = student.Address.Province;
            City = student.Address.City;
            County = student.Address.County;
            Street = student.Address.Street;
            AggregateId = student.Id;
        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Phone { get; private set; }
        public string Province { get; protected set; }
        public string City { get; protected set; }
        public string County { get; protected set; }
        public string Street { get; protected set; }
    }
}