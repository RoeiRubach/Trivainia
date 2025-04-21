using System.Collections.Generic;

namespace Trivainia
{
    public class AbilityQueue
    {
        private readonly Queue<QueuedAbility> _queuedAbilities = new();
        public bool IsEmpty => _queuedAbilities.Count == 0;

        public void Enqueue(int index, AbilityCommand command) => _queuedAbilities.Enqueue(new QueuedAbility(index, command));

        public bool TryDequeue(out QueuedAbility ability) => _queuedAbilities.TryDequeue(out ability);
    }

    public readonly struct QueuedAbility
    {
        public readonly int Index;
        public readonly AbilityCommand Command;

        public QueuedAbility(int index, AbilityCommand command)
        {
            Index = index;
            Command = command;
        }
    }
}