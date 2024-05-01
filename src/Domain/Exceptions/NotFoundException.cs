
namespace Domain.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException()
    {
        
    }

    public NotFoundException(string error) : base(error)
    {
        
    }
}
