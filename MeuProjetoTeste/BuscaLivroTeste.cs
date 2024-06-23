using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace MeuProjetoTeste
{
    [TestClass]
    public class BuscaLivroTeste
    {
        private IWebDriver driver;
        private const string URL = "http://localhost:5189";

        [TestInitialize]
        public void Inicializar()
        {
            driver = new ChromeDriver(); 
        }

        [TestMethod]
        public void TesteBuscaLivroCadastrado()
        {
            driver.Navigate().GoToUrl(URL);

            // Simula a busca por um livro cadastrado
            driver.FindElement(By.Id("IdBusca")).SendKeys("Livro Teste");
            driver.FindElement(By.Id("btnPesquisar")).Click();

            // Espera até que a lista de livros seja atualizada e contenha o livro buscado
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var livrosList = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("livrosList")));

            // Verifica se o livro buscado está na lista de resultados
            var livroBuscado = livrosList.FindElement(By.XPath("//li[contains(text(), 'Livro Teste (Ano: 2023)')]")).Text;
            Assert.AreEqual("Livro Teste (Ano: 2023)", livroBuscado);
        }

        [TestCleanup]
        public void Finalizar()
        {
            driver.Quit();
        }
    }
}
