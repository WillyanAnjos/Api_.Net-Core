
using ApiTeste.Domain.DTOs;
using ApiTeste.Models;

namespace ApiTeste.Infra;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ConnectionDbContext _context = new ConnectionDbContext();
    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public Employee? Get(int id)
    {
        return _context.Employees.FirstOrDefault(e => e.id == id);
    }

    public List<EmployeeDTO> Get(int pageNumber, int pageQuantity)
    {
        return _context.Employees.
        Skip(pageNumber * pageQuantity).
        Take(pageQuantity).
        Select( e => 
        new EmployeeDTO()
        {
            Id = e.id,
            Name = e.name,
            Photo = e.photo
        })
        .ToList();
    }
}
