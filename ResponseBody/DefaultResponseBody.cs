namespace RhbkSdk.ResponseBody;

public class DefaultResponseBody<T>
{
    public int StatusCode { get; set; }
    public T? Data { get; set; }
}