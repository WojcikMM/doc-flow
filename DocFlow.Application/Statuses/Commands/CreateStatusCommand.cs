using System;

namespace DocFlow.Application.Statuses.Commands
{
    public class CreateStatusCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
