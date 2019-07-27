using Solver.DataAccessLayer.Contracts.Contracts;
using Solver.Entities.Models;

namespace Solver.DataAccessLayer.Repository
{
    public class TrackLogRepository : GenericRepository<TrackLog>, ITrackLogRepository
    {
        private readonly ApplicationDataContext context;

        public TrackLogRepository(ApplicationDataContext context) : base(context)
        {
            this.context = context;
        }
    }
}
