using System.Collections;
using MongoDB.Bson;

public interface IRepository<T>
{
    T GetById(ObjectId id);
    IEnumerable List();
    T Add(T entity);
    void Delete(ObjectId entity);
    T Update(T entity);
}