namespace ToDoListBlazorFluentUI.Components;

public partial class ToDoEditModal
{
    [Parameter]
    public ToDoItem Content { get; set; } = default!;

    [CascadingParameter]
    public FluentDialog Dialog { get; set; } = default!;

    private string _title = default!;
    private string _selectedValue = default!;

    private List<Option<string>> _selectItems = default!;

    protected override void OnInitialized()
    {
        _title = Content.Title!;
        _selectedValue = Content.Progress.ToString();

        _selectItems = new()
        {
            { new Option<string>
                {
                    Value = ToDoProgress.Pending.ToString(),
                    Text = ToDoProgress.Pending.GetValue(),
                    Selected = _selectedValue == ToDoProgress.Pending.ToString(),
                }
            },
            { new Option<string>
                {
                    Value = ToDoProgress.InProgress.ToString(),
                    Text = ToDoProgress.InProgress.GetValue(),
                    Selected = _selectedValue == ToDoProgress.InProgress.ToString(),
                }
            },
            { new Option<string>
                {
                    Value = ToDoProgress.Completed.ToString(),
                    Text = ToDoProgress.Completed.GetValue(),
                    Selected = _selectedValue == ToDoProgress.Completed.ToString(),
                }
            },
        };
    }

    private async Task UpdateAsync()
    {
        var list = await _localStorage
                    .GetValueAsync<List<ToDoItem>>(
                        LocalStorageKey.ListItems.GetValue());

        list?.Where(item => item.Id == Content.Id)
            .ToList()
            .ForEach(item =>
            {
                item.Title = _title;
                item.Progress = (ToDoProgress)
                    Enum.Parse(typeof(ToDoProgress), _selectedValue);
            });

        _toDoState.SetValues(list ?? []);

        await _localStorage
            .SetValueAsync(
                LocalStorageKey.ListItems.GetValue(),
                list!);

        await Dialog.CloseAsync();
    }

    private async Task DeleteAsync()
    {
        var list = await _localStorage
                    .GetValueAsync<List<ToDoItem>>(
                        LocalStorageKey.ListItems.GetValue());

        list = list?.Where(item => item.Id != Content.Id).ToList();

        _toDoState.SetValues(list ?? []);

        await _localStorage
            .SetValueAsync<List<ToDoItem>>(
                LocalStorageKey.ListItems.GetValue(),
                list!);

        await Dialog.CloseAsync();
    }

    private async Task CloseAsync()
    {
        await Dialog.CloseAsync();
    }
}