using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveComponents.Samples.ChatRoom
{
    public class ChatComponent : IComponent
    {
        public List<string> Messages { get; set; }

        public ChatComponent()
        {
            Messages = new List<string>
            {
                "Welcome to chatty"
            };
        }

        public void SendMessage(string message)
        {
            Messages.Add(message);
        }

        public string Render()
        {
            var messages = string.Join("", Messages.Select(message => $"<li>{message}</li>").ToList());

            return $@"
                <h1>Chatt app</h1>
                <ul>{messages}</ul>
                <p>
                    <input type=""text"" live-component-model=""message"" live-component-enter=""SendMessage(message)"">
                </p>
            ";
        }
    }
}
