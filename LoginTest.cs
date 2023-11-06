using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Playwright_AssessmentTest
{
    [TestFixture]
    public class LoginTest
    {
        private IBrowser browser;
        private IPage page;        

        [SetUp]
        public async Task SetUp()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });

            var context = await browser.NewContextAsync();
            page = await context.NewPageAsync();
        }        

        [Test]
        public async Task SuccessfullLoginTest()
        {
            //Navigating to loginpage
            await page.GotoAsync("https://www.example.com/login");

            //Enter login credentials
            await page.FillAsync("#username", "tester1");
            await page.FillAsync("#password", "password987");

            //Click on the login page
            await page.ClickAsync("#LoginButton");

            //Navigate to the Dashboard
            await page.WaitForNavigationAsync();

            //Assert logout button is present after logging in
            var logoutButton = await page.WaitForSelectorAsync("LogoutButton");
            Assert.That(logoutButton, Is.Not.Null, "The logout button should be available after a successful login");

            //Close browser
            await page.CloseAsync();
            await browser.CloseAsync();
        }
    }    
}