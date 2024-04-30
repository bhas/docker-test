using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Infrastructure.Database;
public class DockerTestContext : DbContext
{

    public DockerTestContext(DbContextOptions options)
        : base(options)
    {

    }

}
