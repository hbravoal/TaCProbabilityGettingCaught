using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ProcessInformation : IProcessInformation
    {
        public Response<List<ProcessInformationResponse>> Execute(WorkingDays model)
        {
            Response<List<ProcessInformationResponse>> response = new Response<List<ProcessInformationResponse>>
            {
                IsSuccess = true
            };
            List<ProcessInformationResponse> processInformationResponse = new List<ProcessInformationResponse>();

            try
            {
                for (int i = 0; i < model.DayToWork; i++)
                {
                   
                        processInformationResponse.Add(new ProcessInformationResponse
                        {
                            Case = i,
                        });
                        processInformationResponse.LastOrDefault().Detail = new List<ProcessDetailInformationResponse>();
                        List<WeightLastElement> data = new List<WeightLastElement>();
                        List<WeightLastElement> weightLastElementDesc = new List<WeightLastElement>();

                        weightLastElementDesc = model.Elements[i].WeightLastElements.OrderByDescending(c => c.Weight).ToList();
                        data = model.Elements[i].WeightLastElements.OrderByDescending(c => c.Weight).ToList();
                        int processCount = data.Count;

                        for (int t = 0; t < processCount; t++)
                        {
                            int disponibles = data.Count();
                            int topItemWeight = 0;
                            if (disponibles == 0)
                            {
                                break;
                            }
                            if (data.FirstOrDefault().Weight > 50)
                            {
                                topItemWeight = data.FirstOrDefault().Weight;
                                processInformationResponse.LastOrDefault().Detail.Add(new ProcessDetailInformationResponse { TopItemWeight = topItemWeight, Quantity = 1 });
                                data.Remove((data.FirstOrDefault()));
                                continue;
                            }

                            decimal divide = ((decimal)50 / (decimal)data.FirstOrDefault().Weight) * 1;

                            decimal redondeado = Math.Ceiling(divide);

                            for (int f = 0; f < redondeado; f++)
                            {
                                if (f == 0)
                                {
                                    topItemWeight = data.FirstOrDefault().Weight;
                                    data.Remove(data.FirstOrDefault());
                                }
                                else
                                {
                                    data.Remove(data.LastOrDefault());
                                }

                            }
                            if (data?.FirstOrDefault()?.Weight * data.Count < 50)
                            {
                                for (int p = 0; p < data.Count; p++)
                                {
                                    data.Remove(data.LastOrDefault());
                                    redondeado++;
                                }

                            }
                            processInformationResponse.LastOrDefault().Detail.Add(new ProcessDetailInformationResponse { TopItemWeight = topItemWeight, Quantity = (int)redondeado });
                        }


                    }
                
                response.Result = processInformationResponse;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message.Add(new MessageResult
                {
                    Message = string.Format("Ocurrió un error procesando el informe, error Capturado: {0}", Convert.ToString(ex))
                });
            }
            return response;
        }
    }

    public class ProcessedModel
    {
        public int Case { get; set; }
    
        public List<ProcessedModelDetail> details { get; set; }
    }
    public class ProcessedModelDetail
    {
        
        public int TopItemWeight { get; set; }
        public int Quantity { get; set; }
    }
}
