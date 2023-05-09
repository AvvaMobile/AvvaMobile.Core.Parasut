namespace AvvaMobile.Core.Parasut;

public class ParasutServiceResult
{
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; }

    public void SetError(string message)
    {
        IsSuccess = false;
        Message = message;
    }
}

public class ServiceResult<T> : ParasutServiceResult
{
    public T Data { get; set; }
}