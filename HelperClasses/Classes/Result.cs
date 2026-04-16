namespace HelperClasses.Classes;

public class Result
{
    #region Properties

    /// <summary>
    /// Gets a value indicating whether the operation was successful or not.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error message if the operation failed. This will be null if the operation was successful.
    /// </summary>
    public string? ErrorMessage { get; }

    #endregion

    internal Result(bool isSuccess, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    #region Public Static Methods

    #region Failure
    /// <summary>
    /// Creates a failure result with the specified error message. The <see cref="ErrorMessage"/> property will be set to the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message for the failure result.</param>
    /// <returns>A failure result with the specified error message.</returns>
    public static Result Failure(string errorMessage) => new(false, errorMessage);
    #endregion

    #region Success*

    #region Success()
    /// <summary>
    /// Creates a success result. The <see cref="ErrorMessage"/> property will be null.
    /// </summary>
    /// <returns>A success result.</returns>
    public static Result Success() => new(true, null);
    #endregion

    #region Success(T value)
    /// <summary>
    /// Creates a success result with the specified value. The <see cref="ErrorMessage"/> property will be null.
    /// </summary>
    /// <param name="value">The value for the success result.</param>
    /// <returns>A success result with the specified value.</returns>
    public static Result<T> Success<T>(T value) => new(true, value, null);
    #endregion

    #endregion

    #endregion
}

public class Result<T>
{
    #region Properties

    /// <summary>
    /// Gets a value indicating whether the operation was successful or not.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets the error message if the operation failed. This will be null if the operation was successful.
    /// </summary>
    public string? ErrorMessage { get; }

    /// <summary>
    /// Gets the value of the operation if it was successful. This will be the default value of T if the operation failed.
    /// </summary>
    public T Value { get; }

    #endregion

    internal Result(bool isSuccess, T value, string? errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Value = value;
    }

    #region Public Static Methods

    #region Failure
    /// <summary>
    /// Creates a failure result with the specified error message. The Value property will be the default value of T.
    /// </summary>
    /// <param name="errorMessage">The error message for the failure result.</param>
    /// <returns>A failure result with the specified error message.</returns>
    public static Result<T> Failure(string errorMessage) => new(false, default, errorMessage);
    #endregion

    #region Success
    /// <summary>
    /// Creates a success result with the specified value. The <see cref="ErrorMessage"/> property will be null.
    /// </summary>
    /// <param name="value">The value for the success result.</param>
    /// <returns>A success result with the specified value.</returns>
    public static Result<T> Success(T value) => new(true, value, null);
    #endregion

    #endregion

    #region Operator

    #region Result -> Result<T>
    /// <summary>
    /// Implicitly converts a <see cref="Result"/> to a <see cref="Result{T}"/>.
    /// If the <see cref="Result"/> is successful, the resulting <see cref="Result{T}"/> will be successful with the default value of T.
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator Result<T>(Result result) =>
        result.IsSuccess ? Success(default!) : Failure(result.ErrorMessage!);
    #endregion

    #region Result<T> -> Result
    /// <summary>
    /// Implicitly converts a <see cref="Result{T}"/> to a <see cref="Result"/>.
    /// If the <see cref="Result{T}"/> is successful, the resulting <see cref="Result"/> will be successful.
    /// </summary>
    /// <param name="result"></param>
    public static implicit operator Result(Result<T> result) =>
        result.IsSuccess ? new(true, null) : new(false, result.ErrorMessage);
    #endregion

    #endregion
}
