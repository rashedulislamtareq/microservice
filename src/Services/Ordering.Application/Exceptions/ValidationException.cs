namespace Ordering.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base($"One Or More validation Failures Have Occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(x => x.PropertyName, y => y.ErrorMessage)
            .ToDictionary(x => x.Key, y => y.ToArray());
    }
}
