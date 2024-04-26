using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class User
{
    public int Id { get; set; }
    public required string Firstname { get; set; }
    public required string LastName { get; set; }
}
