namespace ToDoListBlazorFluentUI.States;

public class ToDoListState
{
    public List<ToDoItem> ToDoItems { get; set; } = [];

    public event Action OnStateChange = default!;

    public void SetValues(List<ToDoItem> toDoItems)
    {
        ToDoItems = toDoItems;
        NotifyStateChanged();
    }

    public void SetValue(ToDoItem item)
    {
        ToDoItems.Add(item);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}
