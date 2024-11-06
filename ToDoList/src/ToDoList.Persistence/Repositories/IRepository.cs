namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public interface IRepository<T> where T : class
{
    public void Create(T item);

    public List<ToDoItem> Read();

    public ToDoItem ReadById(int id);

    public bool Update(ToDoItem item);

    public bool Delete(int id);
}
