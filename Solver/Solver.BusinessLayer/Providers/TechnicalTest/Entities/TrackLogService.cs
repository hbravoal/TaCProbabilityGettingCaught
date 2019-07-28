using Solver.BusinessLayer.Services;
using Solver.Common.Helpers;
using Solver.Common.Models;
using Solver.DataAccessLayer.Contracts.Contracts;
using Solver.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class TrackLogService : ITrackLogService
    {
        private readonly ITrackLogRepository contract;

        public TrackLogService(ITrackLogRepository contract)
        {
            this.contract = contract;
        }
        public Response<TrackLog> Delete(TrackLog entity)
        {
            if (entity == null)
            {
                return ResponseHelper<TrackLog>.ErrorResponse(new System.Exception("Entity is Empty or Null"), entity);
            }
            return contract.Delete(entity);
        }

        public List<TrackLog> GetAll()
        {
            return contract.GetAll().ToList();
        }

        public Response<TrackLog> Post(TrackLog entity)
        {
            if (entity == null)
            {
                return ResponseHelper<TrackLog>.ErrorResponse(new System.Exception("Entity is Empty or Null"), entity);
            }
            return contract.Create(entity);
        }

        public Response<TrackLog> Put(TrackLog entity)
        {
            if (entity == null)
            {
                return ResponseHelper<TrackLog>.ErrorResponse(new System.Exception("Entity is Empty or Null"), entity);
            }
            return contract.Update(entity);
        }
    }
}
