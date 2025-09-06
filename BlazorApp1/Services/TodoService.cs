using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

public class TodoService
{
    private readonly AppDbContext _context;
    public TodoService(AppDbContext context) { _context = context; }  // DI.

    public async Task<List<TodoItem>> GetTodos() => await _context.TodoItems.ToListAsync();
    
   
   
}