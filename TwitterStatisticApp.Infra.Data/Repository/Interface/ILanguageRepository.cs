using TwitterStatisticApp.Domain.Entities;

namespace TwitterStatisticApp.Infra.Data.Repository.Interface
{
    public interface ILanguageRepository: IRepositoryBase<Language>
    {
        Language GetByCode(string code);
    }
}