namespace ToDoListBlazorFluentUI.Entities
{
    public enum ToDoProgress
    {
        Pending,
        InProgress,
        Completed
    }

    public static class ToDoProgressExtensions
    {
        private const string _pending = "Pending";
        private const string _inProgress = "In Progress";
        private const string _completed = "Completed";

        private const string _warning = "text-warning";
        private const string _danger = "text-danger";
        private const string _success = "text-success";

        public static string GetValue(this ToDoProgress value)
            => value switch
            {
                ToDoProgress.Pending => _pending,
                ToDoProgress.InProgress => _inProgress,
                ToDoProgress.Completed => _completed,
                _ => throw new NotImplementedException()
            };

        public static string GetColorClass(this ToDoProgress value)
            => value switch
            {
                ToDoProgress.Pending => _danger,
                ToDoProgress.InProgress => _warning,
                ToDoProgress.Completed => _success,
                _ => throw new NotImplementedException()
            };
    }
}
