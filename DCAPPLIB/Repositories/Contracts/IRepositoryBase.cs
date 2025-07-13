using System.Linq.Expressions;

namespace DCAPPLIB.Repositories.Contracts;

public interface IRepositoryBase<T>
{
    protected IQueryable<T> GetAll(bool trackChanges);
    protected T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    protected void Create(T entity);
    protected void Delete(T entity);
    protected void Update(T entity);
}