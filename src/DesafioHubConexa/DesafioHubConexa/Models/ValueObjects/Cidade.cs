namespace DesafioHubConexa.Models.ValueObjects
{
    public class Cidade : ValueObjectBase
    {
        public string Nome { get; private set; }

        public float Temperatura { get; private set; }

        public Coordenadas Coordenadas { get; private set; }

        public Cidade(string nome) : base()
        {
            Nome = nome;

            Validar();
        }

        public Cidade(string nome, float tempreatura, Coordenadas coordenadas) : base()
        {
            Nome = nome;
            Temperatura = tempreatura;
            Coordenadas = coordenadas;

            Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Nome))
                AddError("O nome da cidade deve ser preenchido");
        }
    }
}