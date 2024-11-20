namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class DeleteUnitTests
{
    [Fact]

    public void Delete_ValidItemId_ReturnsNoContent()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        //repositoryMock.ReadAll().When().Do();
        //repositoryMock.ReadAll().Returns();
        //repositoryMock.ReadAll().Throws();
        //repositoryMock.Received().ReadAll();
        repositoryMock.ReadById(Arg.Any<int>()).Returns(
            new ToDoItem
            {
                Name = "testName",
                Description = "testDescription",
                IsCompleted = false
            }
        );

        //Act
        var result = controller.DeleteById(1);

        //Assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).ReadAll();
    }
}
