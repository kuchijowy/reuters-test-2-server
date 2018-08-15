using Newtonsoft.Json;
using Reuters_Test_Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Reuters_Test_Api.Controllers
{
    public class CalculatorController : ApiController
    {
        private calculatorEntities db = new calculatorEntities();

        // GET: api/Calculator/5
        public CalculatorSave Get(string id)
        {
            var data = db.t_save
                .Where(w => w.id == id).FirstOrDefault();

            if (data != null)
            {
                CalculatorSave calculator = new CalculatorSave()
                {
                    InputA = data.inputA,
                    InputB = data.inputB,
                    SelectedOperator = data.@operator,
                    Result = data.data_result
                };

                return calculator;
            }
            else
            {
                return null;
            }
        }

        // POST: api/Calculator
        public string Post([FromBody]CalculatorSave calculator)
        {
            try
            {
                if (string.IsNullOrEmpty(calculator.User))
                {
                    calculator.User = "PublicUser";
                }
                var data = db.t_save
                .Where(w => w.id == calculator.User).FirstOrDefault();

                if (data != null)
                {
                    data.inputA = calculator.InputA;
                    data.inputB = calculator.InputB;
                    data.@operator = calculator.SelectedOperator;
                    data.data_result = calculator.Result;
                }
                else
                {
                    t_save save = new t_save()
                    {
                        id = calculator.User,
                        inputA = calculator.InputA,
                        inputB = calculator.InputB,
                        @operator = calculator.SelectedOperator,
                        data_result = calculator.Result
                    };

                    db.t_save.Add(save);
                }

                db.SaveChanges();

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }

        public CalculatorSave Read(string fileName)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Save");
            string targetPath = Path.Combine(targetFolder, fileName);
            return JsonConvert.DeserializeObject<CalculatorSave>(File.ReadAllText(targetPath));
        }
        public void Write(string fileName, CalculatorSave model)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Save");
            string targetPath = Path.Combine(targetFolder, fileName);
            File.WriteAllText(targetPath, JsonConvert.SerializeObject(model));
        }

        // PUT: api/Calculator/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Calculator/5
        public void Delete(int id)
        {
        }
    }
}
