namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    using Solver.BusinessLayer.Services;
    using Solver.Common.Models;
    using System;
    public class ReadFileService: IReadService
    {

        public Response<bool> Read(string FilePath)
        {
            Response<bool> response = new Response<bool>();
            response.IsSuccess = false;
            try
            {
                int counter = 0;
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
                while ((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    counter++;
                }
            }
            catch (Exception)
            {

                
            }

            return response;
        }
    }
}
