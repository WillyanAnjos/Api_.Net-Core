using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiTeste.Domain.Models.CompanyAggregate;

[Table("company")]
public class Company
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
