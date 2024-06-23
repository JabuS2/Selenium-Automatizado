using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace MeuProjetoTeste
{
    [TestClass]
    public class CadastroLivroTeste
    {
        private IWebDriver driver;
        private const string URL = "http://localhost:5189"; // Substitua pela URL da sua aplicação

        [TestInitialize]
        public void Inicializar()
        {
            driver = new ChromeDriver();
        }

        [TestMethod]
        public void TesteCadastroNovoLivro()
        {
            driver.Navigate().GoToUrl(URL);

            // Espera até 10 segundos para que o elemento "Titulo" esteja presente na página
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var txtTitulo = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Titulo")));

            // Preenche o título do livro
            txtTitulo.SendKeys("Livro Teste");

            // Preenche o ano de publicação
            driver.FindElement(By.Id("AnoPublicacao")).SendKeys("2023");

            // Clic no botão de salvar
            driver.FindElement(By.Id("btnSalvar")).Click();

            // Espera até que a lista de livros seja atualizada e contenha o novo livro
            var livrosList = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("livrosList")));
            var livroCadastrado = livrosList.FindElement(By.XPath("//li[contains(text(), 'Livro Teste (Ano: 2023)')]")).Text;

            // Verifica se o livro foi cadastrado corretamente
            Assert.AreEqual("Livro Teste (Ano: 2023)", livroCadastrado);
        }

        [TestCleanup]
        public void Finalizar()
        {
            driver.Quit();
        }
    }
}
