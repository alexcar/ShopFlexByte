namespace ShopFlexByte.Domain.Common;

// D1: Result encapsula o resultado de uma operação (sucesso/falha + erro) ocultando a construção do estado
//     atrás de factory methods estáticos (Success/Failure) e de um construtor protegido — interface clara e concisa.
// C1: Result<T> herda de Result, estendendo o tipo base com um Value tipado (hierarquia extensível).
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && string.IsNullOrEmpty(error))
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;

        Error = error;
    }

    public static Result Success() => new(true, string.Empty);
    public static Result Failure(string error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(value, true, string.Empty);
    public static Result<T> Failure<T>(string error) => new(default!, false, error);
}

public class Result<T> : Result
{
    public T Value { get; }
    protected internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;
    }
}
