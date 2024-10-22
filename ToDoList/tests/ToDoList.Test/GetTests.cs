namespace ToDoList.Test;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        // Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };
        ToDoItemsController.items.Add(toDoItem);
        var controller = new ToDoItemsController();

        // Act
        var result = controller.Read();
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);
    }

    [Fact]
    public void Get_Item_ById_ReturnsItem()
    {
        // Arrange
        var items = new List<ToDoItem>()
        {
            new() {
                ToDoItemId = 1,
                Name = "Jmeno1",
                Description = "Popis1",
                IsCompleted = false
            },
            new() {
                ToDoItemId = 2,
                Name = "Jmeno2",
                Description = "Popis2",
                IsCompleted = true
            }
        };
        ToDoItemsController.items.AddRange(items);
        var controller = new ToDoItemsController();

        // Act
        var result = controller.ReadById(2);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var secondItem = value;
        Assert.Equal(2, secondItem.Id);
        Assert.Equal("Popis2", secondItem.Description);
        Assert.True(secondItem.IsCompleted);
        Assert.Equal("Jmeno2", secondItem.Name);
    }
}
