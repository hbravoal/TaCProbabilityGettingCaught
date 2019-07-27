namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    using Solver.BusinessLayer.Services;
    using Solver.Common.Models;
    using Solver.Entities.Models;
    using System;
    using System.Collections.Generic;

    public class ReadFileService: IReadService
    {

        public Response<WorkingDays> Read(string FilePath)
        {
            int[,,] allItems;    //Arreglo de tres dimensiones

            WorkingDays result = new WorkingDays();
            List<Elements> elements = new List<Elements>();
            Response<WorkingDays> response = new Response<WorkingDays>
            {
                Result = new WorkingDays(),
                IsSuccess = false
            };
            try
            {
                List<int> data = new List<int>();
                bool isDay = true;
                bool isElement = false;
                string line;
                int dayToWork = 0;
                int countProducts = 0;
                int countDays = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
                while ((line = file.ReadLine()) != null)
                {
                    try
                    {
                        data.Add(Int32.Parse(line));
                    }
                    catch (FormatException ex )
                    {
                        //TODO: Mensaje de que debe contener validores enteros.
                        
                    }
                }
                for (int i = 0; i < data.Count; i++)
                {
                    if (isDay)
                    {   
                        result.DayToWork = data[i];
                        isElement = true;
                        isDay = false;
                    }
                    else
                    {
                        if (isElement)
                        {
                            elements.Add(new Elements
                            {
                                Quantity = data[1]
                            });
                            
                            i = i + data[i];
                            try
                            {
                                int test = data[i];
                                countProducts++;
                            }
                            catch (ArgumentOutOfRangeException ex)
                            {
                                //TODO: No se encuentran todo los elementos.
                                throw;
                            }
                            if (countProducts == result.DayToWork)
                            {
                                isElement = false;
                                //TODO OK
                            }
                            else
                            {

                                //no se pudo:
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                
            }

            return response;
        }
    }
}
