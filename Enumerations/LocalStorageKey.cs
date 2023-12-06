namespace ToDoListBlazorFluentUI.Enumerations;

public enum LocalStorageKey
{
    ListItems
}

public static class LocalStorageKeyExtensions
{
    private const string _localItemsKey = "todoListItems";

    public static string GetValue(this LocalStorageKey value)
        => value switch
        {
            LocalStorageKey.ListItems => _localItemsKey,
            _ => ""
        };
}