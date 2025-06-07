namespace skills_test.Core;

public class ErrorNullException : Exception
{
    public ErrorNullException() : base("В случае, когда IsSuccess true, Error не может быть null") { }
}