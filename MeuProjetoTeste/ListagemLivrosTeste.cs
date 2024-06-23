using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace MeuProjetoTeste
{
    [TestClass]
    public class ListagemLivrosTeste
    {
        private IWebDriver driver;
        private const string URL = "http://localhost:5189"; 

        [TestInitialize]
        public void Inicializar()
        {
            driver = new ChromeDriver(); 
        }

        [TestMethod]
        public void TesteListagemLivros()
        {
            driver.Navigate().GoToUrl(URL);

            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var livrosList = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("livrosList")));

            // Verifica se a list de livros contém pelo menos um item
            var livros = livrosList.FindElements(By.TagName("li"));
            Assert.IsTrue(livros.Count > 0, "A lista de livros está vazia.");
        }

        [TestCleanup]
        public void Finalizar()
        {
            driver.Quit();
        }
    }
}
