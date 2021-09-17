using System;
using OpenQA.Selenium.Chrome;

namespace WebDriver_test.Test.Scripts
{
    class test_Vacancies
    {
        static void Main(string[] args)
        {
            string select = "Разработка продуктов";
            string checkbox = "Английский";
            int vacancies = 10;

            WebDriver_test.Pages.Vacancies vacancies_page = new(new ChromeDriver(), select, checkbox);
            int vacancies_count = vacancies_page.OpenUrlAndCalculateVacancies();

            Console.WriteLine($"The result of the calculation of visible vacancies represented by website {vacancies_count}");
            Console.WriteLine($"Expected result is {vacancies}");

            if (vacancies > vacancies_count)
            {
                Console.WriteLine($"Expected result {vacancies} is bigger than calculated {vacancies_count}");
            }
            else if (vacancies < vacancies_count)
            {
                Console.WriteLine($"Expected result {vacancies} is less than calculated {vacancies_count}");
            }
            else
            {
                Console.WriteLine($"Expected result {vacancies} equal to calculated {vacancies_count}");
            }
        }
    }
}
