using System.Collections.Generic;

namespace Trivainia
{
    public class AbilityQueue
    {
        public bool IsEmpty => _commands.Count == 0;
        private readonly Queue<AbilityCommand> _commands = new();
        public void Enqueue(AbilityCommand command) => _commands.Enqueue(command);
        public bool TryDequeue(out AbilityCommand command) => _commands.TryDequeue(out command);
    }
}