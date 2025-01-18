using System.Collections.Generic;
using System.Linq;

namespace VetTail.Application.Common.DTOs.Generics;

public sealed class Result<TType>
{
    public bool Succeeded { get; init; }
    public TType? Value { get; set; }
    public string? Error { get; set; }


    internal Result()
    {
        Succeeded = false;
        Value = default;
        Error = null;
    }

    internal Result(bool succeeded) : this()
    {
        Succeeded = succeeded;
    }

    internal Result(TType result) : this(true)
    {
        Value = result;
    }


    internal Result(string error) : this(false)
    {
        Error = error;
    }



    public static Result<TType> Success()
    {
        return new Result<TType>(true);
    }


    public static Result<TType> Failure()
    {
        return new Result<TType>(false);
    }


    public static Result<TType> Success(TType result)
    {
        return new Result<TType>(result);
    }


    public static Result<TType> Failure(string error)
    {
        return new Result<TType>(error);
    }
}

public class Result
{
    public bool Succeeded { get; init; }

    public string? Errors { get; init; }

    internal Result(bool succeeded)
    {
        this.Succeeded = succeeded;
    }

    internal Result(bool succeeded, string error) : this(succeeded)
    {
        this.Errors = error;
    }


    public static Result Success()
    {
        return new Result(true);
    }

    public static Result Failure()
    {
        return new Result(false);
    }

    public static Result Failure(string errors)
    {
        return new Result(false, errors);
    }
}
