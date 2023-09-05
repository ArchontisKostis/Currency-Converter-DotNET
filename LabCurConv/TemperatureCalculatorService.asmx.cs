using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LabCurConv
{
    /// <summary>
    /// Summary description for TemperatureCalculatorService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TemperatureCalculatorService : System.Web.Services.WebService
    {

        [WebMethod]
        public decimal CelciusToFahrenheit(double num)
        {
            double result = (num * 1.8) + 32;
            return Convert.ToDecimal(result);
        }

        [WebMethod]
        public decimal FahrenheitToCelcius(double num)
        {
            double result = (num - 0.5556) * (5 / 9);
            return Convert.ToDecimal(result);
        }
    }
}
