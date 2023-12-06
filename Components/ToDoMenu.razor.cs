namespace ToDoListBlazorFluentUI.Components;

public partial class ToDoMenu
{
    [Parameter]
    public Guid ItemId { get; set; } = default!;

    private bool _display = false;

    private void ShowMenu() => _display = !_display;

    private async Task ChangeProgress(ToDoProgress progress)
    {
        var list = await _localStorageService
                    .GetValueAsync<List<ToDoItem>>(
                        LocalStorageKey.ListItems.GetValue());

        list?.Where(item => item.Id == ItemId)
            .ToList()
            .ForEach(item => item.Progress = progress);

        _toDoState.SetValues(list ?? []);

        await _localStorageService
            .SetValueAsync<List<ToDoItem>>(
                LocalStorageKey.ListItems.GetValue(),
                list!);

        _display = false;
    }

    private async Task DeleteToDoItem()
    {
        var list = await _localStorageService
                    .GetValueAsync<List<ToDoItem>>(
                        LocalStorageKey.ListItems.GetValue());

        list = list?.Where(item => item.Id != ItemId).ToList();

        _toDoState.SetValues(list ?? []);

        await _localStorageService
            .SetValueAsync<List<ToDoItem>>(
                LocalStorageKey.ListItems.GetValue(),
                list!);

        _display = false;
    }
}