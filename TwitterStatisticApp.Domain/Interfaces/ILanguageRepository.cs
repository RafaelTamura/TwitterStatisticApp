using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Domain.Interfaces
{
    public interface ILanguageRepository: IRepositoryBase<Language>
    {
        Language GetByCode(string code);
    }
}