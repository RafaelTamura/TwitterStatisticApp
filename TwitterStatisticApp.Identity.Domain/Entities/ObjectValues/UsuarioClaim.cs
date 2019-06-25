namespace TwitterStatisticApp.Identity.Domain.Entities.ObjectValues
{
    public class UsuarioClaim
    {
        public UsuarioClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; private set; }
        public string Value { get; private set; }
    }
}