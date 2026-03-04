using System;
using System.Collections.Generic;

namespace Nómina.Models;

public partial class Employee
{
    public int EmpNo { get; set; }

    public string Ci { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly HireDate { get; set; }

    public string Correo { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<DeptEmp> DeptEmps { get; set; } = new List<DeptEmp>();

    public virtual ICollection<DeptManager> DeptManagers { get; set; } = new List<DeptManager>();

    public virtual ICollection<LogAuditoriaSalario> LogAuditoriaSalarios { get; set; } = new List<LogAuditoriaSalario>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
