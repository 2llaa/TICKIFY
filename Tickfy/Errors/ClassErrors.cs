namespace Tickfy.Errors;

public class ClassErrors
{
    public static readonly Error DuplicateClass =
    new Error("Duplicate Class Found", "A flight with the given Id have the same Class.");
    public static readonly Error ClassNotFound =
       new Error("Class.NotFound", "No Class was found with the given ID.");
}
