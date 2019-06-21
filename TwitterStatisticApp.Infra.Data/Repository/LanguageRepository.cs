﻿using Microsoft.Extensions.Configuration;
using System.Linq;
using TwitterStatisticApp.Domain.Entities;
using TwitterStatisticApp.Infra.Data.Repository.Interface;

namespace TwitterStatisticApp.Infra.Data.Repository
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        public LanguageRepository(IConfiguration configuration) : base(configuration)
        { }

        public Language GetByCode(string code)
        {
            var lang = Find(q => q.Code == code);
            return lang != null ? lang.ToList().FirstOrDefault() : null;
        }
    }
}
