namespace SocialMediaAPI.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; set; }
        public ValidationException(IEnumerable<string> errors)
            : base("Validation Failed")
        {
            Errors = errors;
        }
        public override string ToString()
        {
            return $"{base.Message}\nErrors:\n{string.Join("\n", Errors)}";
        }
    }
}
