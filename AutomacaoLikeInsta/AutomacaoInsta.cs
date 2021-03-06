using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AutomacaoLikeInsta
{
    public class AutomacaoInsta
    {
        public static void AutomacaoInstagram()
        {
            IWebDriver driver;
            driver = new ChromeDriver("C:\\Users\\OneDrive\\Documentos\\chrome driver\\chromedriver_win32"); //Colocar o caminho do .EXE do chrome driver de acordo com a versão do seu chrome instalada na maquina
            driver.Url = "http://www.instagram.com";

            Logar(driver);
            SuperLike(driver);
        }

        public static void Logar(IWebDriver driver)
        {
            System.Threading.Thread.Sleep(2000);

            var usuario = driver.FindElement(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div/form/div/div[1]/div/label/input"));
            usuario.SendKeys("COLOCAR O USUÁRIO AQUI"); //Colocar o Usuário do Instagram

            var senha = driver.FindElement(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div/form/div/div[2]/div/label/input"));
            senha.SendKeys("COLOCAR A SENHA AQUI"); //Colocar a senha

            System.Threading.Thread.Sleep(20);

            var botaoSubmit = driver.FindElement(By.XPath("/html/body/div[1]/section/main/article/div[2]/div[1]/div/form/div/div[3]/button"));
            botaoSubmit.Submit();

            System.Threading.Thread.Sleep(2000);

        }

        public static void SuperLike(IWebDriver driver)
        {
            driver.Url = "https://www.instagram.com";

            System.Threading.Thread.Sleep(2000);

            driver.FindElement(By.XPath("/html/body/div[4]/div/div/div/div[3]/button[2]")).Click();

            System.Threading.Thread.Sleep(2000);

            Curti(driver);
        }

        public static void Curti(IWebDriver driver, int vezesRolaPagina = 0)
        {
            var verSePossuiLike = driver.FindElements(By.XPath("//*[@aria-label='Curtir']"));

            if (verSePossuiLike.Count != 0)
            {
                int publicacao = 0;
                Curti(verSePossuiLike, publicacao);
            }
            else
            {
                vezesRolaPagina++;
            }

            if (vezesRolaPagina < 3)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                string title = (string)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                Curti(driver, vezesRolaPagina);
            }
            else
            {
                return;
            }
        }

        public static void Curti(ReadOnlyCollection<IWebElement> verSePossuiLike, int publicacao)
        {
            try
            {
                verSePossuiLike[publicacao].Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Erro na curtida"));
            }

            if (publicacao < verSePossuiLike.Count)
            {
                publicacao++;
                Curti(verSePossuiLike, publicacao);
            }
            else
            {
                return;
            }

        }
    }
}
