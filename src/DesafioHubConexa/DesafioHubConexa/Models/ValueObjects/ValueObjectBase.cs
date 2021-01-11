using System.Collections.Generic;

namespace DesafioHubConexa.Models.ValueObjects
{
    public class ValueObjectBase
    {
        private bool _isValid { get; set; }

        private List<string> _mensagensErro { get; set; }

        public List<string> MensagensErro => _mensagensErro;

        public ValueObjectBase()
        {
            _mensagensErro = new List<string>();
        }

        public void AddError(string mensagem)
        {
            _mensagensErro.Add(mensagem);

            IsValid();
        }

        public bool IsValid()
        {
            return _mensagensErro.Count == 0;
        }
    }
}