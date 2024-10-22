namespace ToDoList.WebApi.Controllers;

using System.Text;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public static readonly List<ToDoItem> items = [];

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            //setting id, usually done by database itself
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            //adding to List by method Add
            items.Add(item);
        }
        catch (Exception ex)
        {
            //error caught using Exception
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item)); //201
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        //defined variable to returned
        List<ToDoItem> itemsToGet;
        try
        {
            //items set as content of return variable
            itemsToGet = items;
        }
        catch (Exception ex)
        {
            //Error handled using Exception
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemsToGet is null)
            ? NotFound() //404
            : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        //try to retrieve the item by id
        ToDoItem? itemToGet;
        try
        {
            //I find by id the content and send to be returned
            itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //map to Domain object as soon as possible
        var updatedItem = request.ToDomain();

        //try to update the item by retrieving it with given id
        try
        {
            //retrieve the item
            var itemIndexToUpdate = items.FindIndex(i => i.ToDoItemId == toDoItemId);
            if (itemIndexToUpdate == -1)
            {
                return NotFound(); //404
            }
            //set id of new object to given id
            updatedItem.ToDoItemId = toDoItemId;
            //replace selected content by the changed one
            items[itemIndexToUpdate] = updatedItem;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        //try to delete the item
        try
        {
            //I find the item
            var itemToDelete = items.Find(i => i.ToDoItemId == toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound(); //404
            }
            //If I find it, it is removed
            items.Remove(itemToDelete);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client
        return NoContent(); //204
    }
}
