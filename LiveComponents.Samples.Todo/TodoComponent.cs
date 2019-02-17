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

        public void AddTodo()
        {
            Todos.Add("New Todo");
        }

        public string Render()
        {
            var todoList = string.Join("", Todos.Select(todo => $"<li>{todo}</li>").ToArray());

            return $@"
                <h1>Todo list</h1>
                <ul>{todoList}</ul>
                <button live-component-click=""AddTodo"">Add</button>
            ";
        }
    }
}