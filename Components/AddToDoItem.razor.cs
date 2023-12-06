namespace ToDoListBlazorFluentUI.Components;

public partial class AddToDoItem : ComponentBase
{
    private bool _showError = false;

    private string? _toDoTitle = default!;

    private void ShowErrorMessage()
    {
        _showError = true;
    }

    private void HideErrorMessage()
    {
        _showError = false;
    }

    private async Task AddItem()
    {
        if (string.IsNullOrEmpty(_toDoTitle))
        {
            ShowErrorMessage();
            return;
        }

        var todoItem = new ToDoItem
        {
            Id = Guid.NewGuid(),
            Title = _toDoTitle
        };

        _todoState.SetValue(todoItem);

        var itemsList = await _localStorage
                .GetValueAsync<List<ToDoItem>>(
                    LocalStorageKey.ListItems.GetValue())
            ?? [];

        itemsList.Add(todoItem);

        await _localStorage
            .SetValueAsync<List<ToDoItem>>(
                LocalStorageKey.ListItems.GetValue(),
                itemsList);

        _toDoTitle = null;

        StateHasChanged();
    }
}