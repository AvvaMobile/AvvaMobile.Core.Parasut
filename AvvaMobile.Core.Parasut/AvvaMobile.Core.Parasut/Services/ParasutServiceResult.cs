namespace AvvaMobile.Core.Parasut;

/// <summary>
/// Paraşüt API'ının döndürdüğü hata kaydı.
/// </summary>
public class ParasutError
{
    public string? title { get; set; }
    public string? detail { get; set; }
}

public class ParasutServiceResult
{
    public bool IsSuccess { get; set; } = true;
    public string? Message { get; set; }

    /// <summary>
    /// Paraşüt yapılandırılmış hata döndürdüyse ({"errors":[{"title","detail"}]}) doldurulur.
    /// </summary>
    public List<ParasutError>? Errors { get; set; }

    public void SetError(string? message)
    {
        IsSuccess = false;
        Message = message;
    }
}

public class ParasutServiceResult<T> : ParasutServiceResult
{
    public T? Data { get; set; }
}
