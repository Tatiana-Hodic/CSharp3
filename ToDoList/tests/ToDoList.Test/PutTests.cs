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
    public class PutTests
    {
        [Fact]
        public void Put_Item_ById_ReturnsNoContent()
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
            var toDoItemDto = new ToDoItemUpdateRequestDto("Jmeno", "Popis", false);
            ToDoItemsController.items.AddRange(items);
            var controller = new ToDoItemsController();

            // Act
            var result = controller.UpdateById(2, toDoItemDto);
            var resultResult = result;

            // Assert
            Assert.IsType<NoContentResult>(resultResult);
        }
    }
}
