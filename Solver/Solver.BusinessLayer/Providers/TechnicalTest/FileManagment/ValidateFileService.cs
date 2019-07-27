using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using System;
using System.Collections.Generic;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ValidateFileService : IValidateFileService
    {
        /// <summary>
        /// Método que valida la información recibida en el Archivo.
        /// </summary>
        /// <param name="data">Lista de enteros a leer.</param>
        /// <returns>Tipo Response-> Dónde se captura a su vez, el mensaje a mostrar.</returns>
        public Response<bool> Validate(string FilePath)
        {
            Response<bool> response = new Response<bool> { Result = false, IsSuccess = false };
            List<int> data = new List<int>();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                try
                {
                    data.Add(Int32.Parse(line));
                }
                catch (FormatException ex)
                {
                    response.IsSuccess = false;
                    response.Message.Add(new MessageResult { Message = "Solo se aceptan números enteros." });

                }
            }
            int elements = 0;            
            int dayToWork = 0;
            int countProducts = 0;

            for (int i = 0; i < data.Count; i++)
            {
                //El primer elemento siempre será los días a trabajar.
                if (i==0)
                {
                    dayToWork = data[i];                    
                }
                else
                {   //Se trata de navegar entre los elementos para confirmar que contengan los productos indicados.
                    elements = data[i];
                    i = i + data[i];
                    try
                    {

                        int test = 0;
                        countProducts++;
                       
                             test = data[i];
                        
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        response.Message.Add(new MessageResult
                        {
                            Message =
                            string.Format("No se encuentran los Elementos a mover que previamente se configuraror, Posición: {0}, # Elementos: {1}.",
                            i,
                            elements)
                        });
                        response.IsSuccess = false;
                    }
                }
            }

            if (countProducts == dayToWork)
            {
                response.Message.Add(new MessageResult
                {
                    Message = "El formato es correcto y contiene toda la información."
                });
                response.IsSuccess = true;
            }
            else
            {
                response.Message.Add(new MessageResult
                {
                    Message = "Faltaron datos."
                });
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
