using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public ProcessInformation(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// Implementación para asignar los elementos en las bolsas.
        /// Se ordena los elementos de menor a mayor.
        /// Se obtiene el primero (El más pesado) y se le saca cuántas veces está en el Peso mínimo a cargar
        /// Y dependiendo el # se toma del final de la lista (Los menos Pesados) los elementos.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                            Case = i+1,
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
                            if (data.FirstOrDefault().Weight > Convert.ToDecimal(this.configuration["FileValidations:GuaranteedWeight"]))
                            {
                                topItemWeight = data.FirstOrDefault().Weight;
                                processInformationResponse.LastOrDefault().Detail.Add(new ProcessDetailInformationResponse { TopItemWeight = topItemWeight, Quantity = 1 });
                                data.Remove((data.FirstOrDefault()));
                                continue;
                            }

                            decimal divide = (Convert.ToDecimal(this.configuration["FileValidations:GuaranteedWeight"]) / (decimal)data.FirstOrDefault().Weight) * 1;

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
                            if (data?.FirstOrDefault()?.Weight * data.Count < Convert.ToDecimal(this.configuration["FileValidations:GuaranteedWeight"]))
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
