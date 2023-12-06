namespace ToDoListBlazorFluentUI.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public ToDoProgress Progress { get; set; }
            = ToDoProgress.Completed;
    }
}
