namespace RhbkSdk.Exceptions;

public class RhbkSdkDefaultException(int statusCode, string message) : Exception(message)
{
}