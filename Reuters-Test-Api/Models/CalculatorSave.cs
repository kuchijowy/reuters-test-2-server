using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reuters_Test_Api.Models
{
    public class CalculatorSave
    {
        public string User { get; set; }
        public string InputA { get; set; }
        public string InputB { get; set; }
        public string SelectedOperator { get; set; }
        public string Result { get; set; }
    }
}