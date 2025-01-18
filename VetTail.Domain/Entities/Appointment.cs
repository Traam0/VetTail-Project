using System;
using VetTail.Domain.Common.Abstractions;
using VetTail.Domain.Enums;

namespace VetTail.Domain.Entities;

public class Appointment : AuditableEntity
{
    public ulong Id { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    
    public Guid ClientId { get; set; }
    public Client Client { get; set; }

}
