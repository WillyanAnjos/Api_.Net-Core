using System;
using ApiTeste.Domain.DTOs;

namespace ApiTeste.Models;

public interface IEmployeeRepository
{
    void Add(Employee employee);

    List<EmployeeDTO> Get(int pageNumber, int pageQuantity);

    Employee? Get(int id);
}
