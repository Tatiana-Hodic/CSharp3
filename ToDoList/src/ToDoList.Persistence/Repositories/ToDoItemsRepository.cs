namespace ToDoList.Persistence.Repositories;

using System.Collections.Generic;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }

    public bool Delete(int id)
    {
        var itemToDelete = context.ToDoItems.Find(id);
        if (itemToDelete is null)
        {
            return false; //404
        }

        context.ToDoItems.Remove(itemToDelete);
        context.SaveChanges();
        return true;
    }
    public List<ToDoItem> Read()
    {
        return context.ToDoItems.ToList();
    }
    public ToDoItem ReadById(int id)
    {
        return context.ToDoItems.Find(id);
    }
    public bool Update(ToDoItem item)
    {
        var itemToUpdate = context.ToDoItems.Find(item.ToDoItemId);
        if (itemToUpdate is null)
        {
            return false; //404
        }

        context.Entry(itemToUpdate).CurrentValues.SetValues(item);
        context.SaveChanges();
        return true;
    }
}
