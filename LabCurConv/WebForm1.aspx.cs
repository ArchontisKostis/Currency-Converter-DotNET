using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace LabCurConv
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Service Decleration
        lt.lb.www.ExchangeRates currencyConverterService = new lt.lb.www.ExchangeRates();
        TemperatureCalculatorService temeratureCalculationService = new TemperatureCalculatorService();

        int EURO_DEFAULT = 26;
        int POUND_DEFAULT = 27;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                populateCurrenciesDropdown();
                populateTemperatureConvertionDropdown();
            }
        }

        // Helper Method to check if it is the first time we load
        protected bool firstTimeLoading()
        {
            return IsPostBack ? false : true;
        }

        protected bool currenciesAreTheSame(string currencyToCheck, string targetCurrency)
        {
            return currencyToCheck.Equals(targetCurrency);
        }

        protected void CurrencyConvertBtn_Click(object sender, EventArgs e)
        {
            decimal finalResult, rateFrom = 0, rateTo = 0;

            string selectedCurrencyFrom = Convert.ToString(CurrenciesDropdown1.SelectedItem).Substring(0, 3);
            string selectedCurrencyTo = Convert.ToString(CurrenciesDropdown2.SelectedItem).Substring(0, 3);

            // Get the <ExchangeRates> XML Node
            XmlNode exchangeRatesByDate = currencyConverterService.getExchangeRatesByDate("2014-12-31");

            // Get the <item>
            XmlNodeList exchangeRatesItems = exchangeRatesByDate.SelectNodes("//item");

            Debug.WriteLine("Exchange Rates List Count: " + exchangeRatesItems.Count);

            // Parse the items to get the rate
            foreach (XmlNode exchangeRatesItem in exchangeRatesItems)
            {
                if (exchangeRatesItem["currency"].InnerText.Equals(selectedCurrencyFrom))
                {
                    rateFrom = calculateRate(exchangeRatesItem);
                }
                if (exchangeRatesItem["currency"].InnerText == selectedCurrencyTo)
                {
                    rateTo = calculateRate(exchangeRatesItem);
                }
            }

            // Perform the conversion
            decimal input = Convert.ToDecimal(CurrOneInput.Text);
            decimal result = convert(input, rateFrom, rateTo);
            finalResult = Math.Round(result, 2);

            // Display The Result
            CurrTwoInout.Text = Convert.ToString(finalResult);
        }

        protected decimal convert(decimal amountToConvert, decimal rateFrom, decimal rateTo)
        {
            return amountToConvert * rateFrom / rateTo;
        }

        protected decimal calculateRate(XmlNode exchangeRatesItem)
        {
            decimal rate = Convert.ToDecimal(exchangeRatesItem["rate"].InnerText);
            decimal quantity = Convert.ToDecimal(exchangeRatesItem["quantity"].InnerText);

            return rate / quantity;
        }

        protected void populateCurrenciesDropdown()
        {
            // Get the available currencies from the service
            // The form is an XMLNode
            XmlNode currenciesNode = currencyConverterService.getListOfCurrencies();

            // The nodes we need are in the form of: <item>
            XmlNodeList currencyItemsNode = currenciesNode.SelectNodes("//item");

            // We parse the nodelist and add the currencies to the dropdown
            foreach (XmlNode currencyItemNode in currencyItemsNode)
            {
                string currName = currencyItemNode["currency"].InnerText;
                string currDesc = currencyItemNode["description"].InnerText;

                Console.WriteLine(currName + " - " + currDesc);

                CurrenciesDropdown1.Items.Add(currName + " " + currDesc);
                CurrenciesDropdown2.Items.Add(currName + " " + currDesc);
            }

            // Set the default selected currencies on the dropdown
            CurrenciesDropdown1.SelectedIndex = EURO_DEFAULT;
            CurrenciesDropdown2.SelectedIndex = POUND_DEFAULT;
        }

        protected void populateTemperatureConvertionDropdown()
        {
            TemperatureFormatDropdown.Items.Add("Celcius to Fahrenheit");
            TemperatureFormatDropdown.Items.Add("Fahrenheit to Celcius");
        }

        protected void TempConvertBtn_Click(object sender, EventArgs e)
        {
            decimal result;
            string selectedConvertion = Convert.ToString(TemperatureFormatDropdown.SelectedItem.Text);
            string numberInputStr = TempInput.Text;

            decimal number = Convert.ToDecimal(numberInputStr);

            if (selectedConvertion == "Celcius to Fahrenheit")
            {
                Debug.WriteLine("C to F selected");
                result = temeratureCalculationService.CelciusToFahrenheit( (double) number );

                TempConversionResultLbl.Text = Convert.ToString(result + "°F");
            }
            else if (selectedConvertion == "Fahrenheit to Celcius") 
            {
                Debug.WriteLine("F to C selected");
                result = temeratureCalculationService.FahrenheitToCelcius((double)number);

                TempConversionResultLbl.Text = Convert.ToString(result + " °C");
            }
        }

    }
}