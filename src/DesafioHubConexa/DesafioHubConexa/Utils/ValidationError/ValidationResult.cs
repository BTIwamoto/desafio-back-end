namespace DesafioHubConexa.Utils.ValidationError
{
    public class ValidationResult
    {
        public string Error { get; }

        public ValidationResult(string error)
        {
            Error = error;
        }
    }
}