using OpenQA.Selenium;
using System;

namespace WebDriver_test.Pages
{
    public class Vacancies
    {
        private IWebDriver driver;
        string select_arg, checkbox_arg;
        public Vacancies(IWebDriver driver, string select_arg, string checkbox_arg)
        {
            //Constructor for the Vacancies Page Object
            //we set desired driver that depends on the browser we want to test
            //and provide argumetns
            //one for http select
            //one for http checkbox
            
            this.driver = driver;
            this.select_arg = select_arg;
            this.checkbox_arg = checkbox_arg;
        }
        public int OpenUrlAndCalculateVacancies()
        {
            //method for calculating number of vacancies that are shown
            //by choosing options provided with select and chockbox arguments

            //returns count of visible vacancies after the desired menu options are set

            string select_menu = "Все отделы";
            string checkbox_menu = "Все языки";

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://careers.veeam.ru/vacancies");
            driver.Manage().Window.FullScreen();

            CloseCookie();
            Select(select_menu, select_arg);
            CheckBox(checkbox_menu, checkbox_arg);

            int vacancies_count = CalculateVacancies();

            driver.Close();
            driver.Dispose();

            return vacancies_count;
        }
        private void CloseCookie()
        {
            //private method to close cookie pop-up
            driver.FindElement(By.Id("cookiescript_close")).Click();
        }

        private IWebElement FindMyElement(string category)
        {
            //private method to find desired select or checkbox menu

            var elements_collection = driver.FindElements(By.ClassName("form-group"));
            foreach (IWebElement element in elements_collection)
            {
                if (element.Text == category)
                {
                    return element;
                }
            }
            return null;
        }

        private void Select(string category, string subcategory)
        {
            //private method for chosing desired menu option from select dropdown style menu

            IWebElement element = FindMyElement(category);
            if (element != null)
            {
                element.Click();
                element.FindElement(By.LinkText(subcategory)).Click();

            }
            else
            {
                Console.WriteLine($"Error. There is no such category {category}");
                return;
            }
        }
        private void CheckBox(string category, string subcategory)
        {
            //private method for choosing desired menu option from checkbox style menu

            IWebElement element = FindMyElement(category);
            if (element != null)
            {
                element.Click();
                ChooseOption(element, subcategory);
            }
            else
            {
                Console.WriteLine($"Error. There is no such category {category}");
                return;
            }
        }
        private void ChooseOption(IWebElement element, string subcategory)
        {
            //private helper method to find correct checkbox option

            var elements_collection = element.FindElements(By.CssSelector("label[class=custom-control-label]"));
            foreach(IWebElement subelement in elements_collection)
            {
                if(subelement.Text == subcategory)
                {
                    subelement.Click();
                    element.Click();
                    return;
                }
            }
            Console.WriteLine($"There is no such subcategory {subcategory}");
        }
        private int CalculateVacancies()
            {
                //private method to calculate visible vacancies
                return driver.FindElements(By.CssSelector("a.card.card-no-hover.card-sm")).Count;
            }
    }
}
