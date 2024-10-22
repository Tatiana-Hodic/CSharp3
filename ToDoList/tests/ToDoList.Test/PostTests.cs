using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

namespace ToDoList.Test
{
    public class PostTests
    {
        [Fact]
        public void Post_Item_ReturnsItem()
        {
            // Arrange
            var toDoItem = new ToDoItem
            {
                ToDoItemId = 1,
                Name = "Jmeno",
                Description = "Popis",
                IsCompleted = false
            };
            var toDoItemDto = new ToDoItemCreateRequestDto("Jmeno", "Popis", false);
            var controller = new ToDoItemsController();

            // Act
            var result = controller.Create(toDoItemDto);
            var resultResult = result.Result;
            var value = result.GetValue();

            // Assert
            Assert.IsType<CreatedAtActionResult>(resultResult);
            Assert.NotNull(value);

            var firstItem = value;
            Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
            Assert.Equal(toDoItem.Description, firstItem.Description);
            Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
            Assert.Equal(toDoItem.Name, firstItem.Name);
        }
    }
}
