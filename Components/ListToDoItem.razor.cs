namespace ToDoListBlazorFluentUI.Components;

public partial class ListToDoItem : ComponentBase
{
    protected override async Task OnInitializedAsync()
    {
        var items = await _localStorage
                        .GetValueAsync<List<ToDoItem>>(
                            LocalStorageKey.ListItems.GetValue())
                    ?? [];

        _todoState.SetValues(items);

        _todoState.OnStateChange += StateHasChanged;
    }

    private async Task ShowModal(Guid id)
    {
        var currentItem = _todoState.ToDoItems
            .Find(item => item.Id == id);

        if (currentItem is null)
            return;

        var parameters = new DialogParameters();

        await _modal.ShowDialogAsync<ToDoEditModal>(currentItem, parameters);
    }
}