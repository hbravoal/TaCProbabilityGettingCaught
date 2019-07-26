namespace Solver.DataAccessLayer.Contracts.Required
{
    using System;
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}