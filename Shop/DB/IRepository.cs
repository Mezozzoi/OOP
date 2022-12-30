using System.Collections;
using MongoDB.Bson;

public interface IRepository<T>
{
    T GetById(ObjectId id);
    IEnumerable List();
    // IEnumerable<T> List(Expression<Func<T, bool>> predicate);
    T Add(T entity);
    void Delete(T entity);
    T Update(T entity);
}