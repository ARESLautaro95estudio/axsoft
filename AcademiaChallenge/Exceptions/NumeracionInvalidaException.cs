namespace AcademiaChallenge.Exceptions
{
    public class NumeracionInvalidaException : ValidacionFacturaException
    {
        public NumeracionInvalidaException() : base("Numeración inválida")
        {
        }
    }
}
