using System;

namespace TwitterStatisticApp.Domain.Entities
{
    public class Language
    {
        public Language(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }

        public void SetId(Guid id)
        {
            Id = id;
        }
    }
}
