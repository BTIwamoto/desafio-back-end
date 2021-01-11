using System.Collections.Generic;

namespace DesafioHubConexa.Utils.ValidationError
{
    public class ValidationBase
    {
        public bool Success { get; set; }

        public virtual List<ValidationResult> ValidationResult { get; set; } = new List<ValidationResult>();

        protected ValidationBase() { }

        public static T TratarMensagemErro<T>(List<string> errors) where T : ValidationBase, new()
        {
            var listaErro = new T();

            foreach (var error in errors)
                listaErro.ValidationResult.Add(new ValidationResult(error));

            listaErro.Success = listaErro.ValidationResult.Count == 0;

            return listaErro;
        }
    }
}