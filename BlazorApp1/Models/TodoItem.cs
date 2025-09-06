namespace BlazorApp1.Models
{
    public class TodoItem
    {
        public int Id { get; set; }  // Уникальный идентификатор, авто-генерируется БД.
        public string Title { get; set; }  // Название задачи.
        public bool IsCompleted { get; set; }  // Статус завершения.
    }
}