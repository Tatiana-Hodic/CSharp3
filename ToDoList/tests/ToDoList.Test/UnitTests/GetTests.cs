namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetUnitTests
{
    [Fact]

    public void Get_ReadAllAndSomeItemsIsAvailable_ReturnsOk()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        //repositoryMock.ReadAll().When().Do();
        //repositoryMock.ReadAll().Returns();
        //repositoryMock.ReadAll().Throws();
        //repositoryMock.Received().ReadAll();
        repositoryMock.ReadAll().Returns(
        [
            new ToDoItem{
                Name = "testName",
                Description = "testDescription",
                IsCompleted = false
            }
        ]);

        //Act
        var result = controller.Read();
        var resultResult = result.Result;

        //Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
    }

    [Fact]

    public void Get_ReadAllExceptionOccured_ReturnInternalServerError()
    {
        //Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        //Act
        var result = controller.Read();
        var resultResult = result.Result;

        //Assert
        Assert.IsType<ObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}
