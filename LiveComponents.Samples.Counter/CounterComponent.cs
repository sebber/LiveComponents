using System;

namespace LiveComponents.Samples.Counter
{
    public class CounterComponent : BaseComponent, IComponent
    {
        public int Count { get; set; }

        public void Add()
        {
            Count++;
        }

        public void Subtract()
        {
            Count--;
        }

        public override string Render()
        {
            return $@"
                <h1>Counter</h1>
                <p>{Count}</p>
                <button live-component-click=""Add"">Add</button>
                <button live-component-click=""Subtract"">Subtract</button>
            ";
        }
    }
}
