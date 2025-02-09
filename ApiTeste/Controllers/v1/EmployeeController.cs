using ApiTeste.Domain.DTOs;
using ApiTeste.Models;
using ApiTeste.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/employee")]
    [ApiVersion("1.0")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add([FromForm] EmployeeViewModel employeeViewModel)
        {
            var filePath = Path.Combine("Storage", employeeViewModel.Photo.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                employeeViewModel.Photo.CopyTo(stream);
            }

            var employee = new Employee(employeeViewModel.Name, employeeViewModel.Age, filePath);
            _employeeRepository.Add(employee);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            //_logger.Log(LogLevel.Error, "Teve um Erro");


            //throw new Exception("Erro ao buscar os dados");

            var employees = _employeeRepository.Get(pageNumber, pageQuantity);

            //_logger.LogInformation("test");
            return Ok(employees);
        }

        [HttpPost]
        [Route("{id}/download")]
        [Authorize]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(dataBytes, "image/png");
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {

            var employee = _employeeRepository.Get(id);

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return Ok(employeeDTO);
        }
    }
}
