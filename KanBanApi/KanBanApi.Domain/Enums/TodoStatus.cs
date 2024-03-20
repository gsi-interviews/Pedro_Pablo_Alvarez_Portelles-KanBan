using Ardalis.SmartEnum;

namespace KanBanApi.Domain.Entities;

public class TodoStatus : SmartEnum<TodoStatus>
{
    public static TodoStatus Todo = new("Todo", 0);
    public static TodoStatus Doing = new("Doing", 1);
    public static TodoStatus Review = new("Review", 2);
    public static TodoStatus Done = new("Done", 3);


    public TodoStatus(string name, int value) : base(name, value) { }
}