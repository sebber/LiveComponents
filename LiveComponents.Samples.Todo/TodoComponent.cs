using System.Collections.Generic;
using System.Linq;

namespace LiveComponents.Samples.Todo
{
    public class TodoComponent : IComponent
    {
        public List<string> Todos { get; set; }

        public TodoComponent()
        {
            Todos = new List<string>();
        }

        public void AddTodo(string title)
        {
            Todos.Add(title);
        }

        public string Render()
        {
            var todoList = string.Join("", Todos.Select(todo => $"<li>{todo}</li>").ToArray());

            return $@"
                <h1>Todo list</h1>
                <input type=""text"" live-component-model=""title"" />
                <button live-component-click=""AddTodo(title)"">Add</button>
                <ul>{todoList}</ul>
            ";
        }
    }
}