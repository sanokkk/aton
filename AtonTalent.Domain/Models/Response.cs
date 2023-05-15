namespace AtonTalent.Domain.Models;

public class Response<T>
{
    public bool Success { get; set; }

    public T? Content { get; set; }

    public Response()
    {
        Success = true;
    }
}


