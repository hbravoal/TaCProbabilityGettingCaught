using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    /// <summary>
    /// Implementación para leer El archivo.
    /// </summary>
    public class ReadFileService: IReadFileService
    {

        public Response<WorkingDays> Read(string FilePath)
        {


            Response<WorkingDays> response = new Response<WorkingDays>
            {
                Result = new WorkingDays{Elements = new List<Elements>{}},
                IsSuccess = true
            };
            
            
            try
            {
                List<int> dataProcessed = new List<int>();
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
                while ((line = file.ReadLine()) != null)
                {
                    dataProcessed.Add(Int32.Parse(line));
                }

                bool isElement = false;
                int elementsProceseed = 0;
                for (int i = 0; i < dataProcessed.Count; i++)
                {
                    //El primer elemento siempre será los días a trabajar.
                    if (i==0)
                    {
                        response.Result.DayToWork= dataProcessed[i];
                        isElement = true;
                        
                    }
                    else
                    {
                        if (isElement)
                        {
                            response.Result.Elements.Add(new Elements { Quantity = dataProcessed[i] });
                            isElement = false;
                        }
                        else
                        {
                           
                           
                                if (response.Result.Elements.LastOrDefault().WeightLastElements == null)
                                {
                                    response.Result.Elements.LastOrDefault().WeightLastElements = new List<WeightLastElement>();
                                }

                                response.Result.Elements.LastOrDefault().WeightLastElements.Add(new WeightLastElement { Weight = dataProcessed[i] });
                                elementsProceseed++;
                                int elementsTo = response.Result.Elements.LastOrDefault().Quantity;
                                if (response.Result.Elements.LastOrDefault().Quantity == elementsProceseed)
                                {
                                    isElement = true;
                                    elementsProceseed = 0;
                                }
                            
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult
                {
                    Message = "ocurrió un error en el proceso."
                });
                response.IsSuccess = false;
            }

            return response;
        }

    }
}
