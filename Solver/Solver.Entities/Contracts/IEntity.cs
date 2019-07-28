namespace Solver.Entities.Contracts
{
    using System;
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}