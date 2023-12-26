using ErrorOr;

namespace BuberDinner.Domain.Common.Errors;

public static partial class Errors {
    public static class User {
        public static Error DuplicateEmail => Error.Conflict(
            code: "user.duplicate_email",
            description: "Email already exists");
    }
}