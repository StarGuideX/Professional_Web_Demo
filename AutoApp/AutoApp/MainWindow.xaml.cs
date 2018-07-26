using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace AutoApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string xmlPath = Directory.GetCurrentDirectory() + @"/Configue.xml";
        private IWebDriver driver;
        // 警综平台登陆页面
        private readonly string loginUrl = ConfigurationManager.AppSettings.Get("login");
        // 进入天地e搜等待时间 默认5000
        private int fowardToSearchTime = 5000;
        // 填写完搜索条件，后出现的结果页面等待时间 默认5000 
        private int fowardToSearchListTime = 5000;
        // 点击—户政_常口基本信息后的等待时间 默认5000 
        private int fowardToHouseholdListTime = 5000;
        // 点击—身份证号后的等待时间 默认100000
        private int fowardToPeopleInfoTime = 100000;
        // 关闭页面等待时间 默认2000
        private int closeWindowTime = 2000;
        // 运行次数 默认300
        private int timeCounts = 300;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                // 进入天地e搜等待时间 默认5000
                string searchTimeStr = ConfigurationManager.AppSettings.Get("fowardToSearchTime");
                int.TryParse(searchTimeStr, out fowardToSearchTime);
                // 填写完搜索条件，后出现的结果页面等待时间 默认5000 
                string searchListTimeStr = ConfigurationManager.AppSettings.Get("fowardToSearchListTime");
                int.TryParse(searchListTimeStr, out fowardToSearchListTime);
                // 点击—户政_常口基本信息后的等待时间 默认5000
                string householdListTimeStr = ConfigurationManager.AppSettings.Get("fowardToHouseholdListTime");
                int.TryParse(householdListTimeStr, out fowardToHouseholdListTime);
                // 点击—身份证号后的等待时间 默认100000
                string peopleInfoTimeStr = ConfigurationManager.AppSettings.Get("fowardToPeopleInfoTime");
                int.TryParse(peopleInfoTimeStr, out fowardToPeopleInfoTime);
                // 关闭页面等待时间 默认2000
                string closeWindowTimeStr = ConfigurationManager.AppSettings.Get("closeWindowTime");
                int.TryParse(closeWindowTimeStr, out closeWindowTime);
                // 运行次数 默认300
                string timeCountsStr = ConfigurationManager.AppSettings.Get("timeCountsStr");
                int.TryParse(timeCountsStr, out timeCounts);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }

            }

            //CDriver driver = new CDriver();
            //driver.AutoStart();
        }

        public void a()
        {
            if (File.Exists(xmlPath))
            {
                //读取
            }
            else
            {
                XmlDocument document = new XmlDocument();
                //一级
                document.AppendChild(document.CreateXmlDeclaration("1.0", "UTF-8", null));
                XmlElement root = document.CreateElement("Root");
                document.AppendChild(root);
                //二级 Foward
                XmlElement fowards = document.CreateElement("Fowards");
                //三级
                XmlElement searchFoward = document.CreateElement("SearchFoward");
                searchFoward.SetAttribute("SleepTime", "5000");
                fowards.AppendChild(searchFoward);

                root.AppendChild(fowards);
                document.Save(xmlPath);
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                driver = new ChromeDriver(Directory.GetCurrentDirectory());
                //进入警综平台
                driver.Navigate().GoToUrl(loginUrl);

                LoginBtn.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }

            }

        }

        public void AutoStart()
        {
            try
            {
                for (int i = 0; i < timeCounts; i++)
                {
                    ReadOnlyCollection<string> handles;
                    if (i == 0)
                    {
                        //点击天地e搜(新)—新标签
                        //driver.FindElement(By.LinkText("天地e搜（新）")).Click();
                        driver.FindElement(By.Id(ConfigurationManager.AppSettings.Get("tdesx"))).Click();
                        Thread.Sleep(fowardToSearchTime);

                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());

                        //设置搜索条件点击搜索——本页
                        handles = driver.WindowHandles;
                        driver.FindElement(By.Id(ConfigurationManager.AppSettings.Get("keyword"))).SendKeys(ConfigurationManager.AppSettings.Get("120225"));
                        //driver.FindElement(By.LinkText("智搜一下")).Click();
                        driver.FindElement(By.Id(ConfigurationManager.AppSettings.Get("searchBtn2"))).Click();
                        Thread.Sleep(fowardToSearchListTime);


                        //户政_常口基本信息——本页
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        Thread.Sleep(closeWindowTime);

                        driver.FindElements(By.Name(ConfigurationManager.AppSettings.Get("house"))).First().Click();
                        Thread.Sleep(fowardToHouseholdListTime);

                        //点击身份证号——新标签
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        Thread.Sleep(closeWindowTime);

                        IWebElement p = driver.SwitchTo().Frame("tableContainerIframe").FindElement(By.Id("divTableParent"));
                        ReadOnlyCollection<IWebElement> ps = p.FindElements(By.TagName(ConfigurationManager.AppSettings.Get("tr0")));
                        IWebElement ps1 = ps[RandomTrIndex()];
                        ps1.FindElement(By.ClassName(ConfigurationManager.AppSettings.Get("hyperlink_idCard"))).Click();
                        Thread.Sleep(fowardToPeopleInfoTime);

                        //关闭身份证号页面
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        driver.Close();
                        Thread.Sleep(closeWindowTime);
                        //关闭搜索页面
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        driver.Close();
                        Thread.Sleep(closeWindowTime);
                    }
                    else
                    {
                        //点击天地e搜(新)—新标签
                        //driver.FindElement(By.LinkText("天地e搜（新）")).Click();
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        driver.FindElement(By.Id(ConfigurationManager.AppSettings.Get("tdesx"))).Click();
                        Thread.Sleep(fowardToSearchTime);

                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());

                        //设置搜索条件点击搜索——本页
                        handles = driver.WindowHandles;
                        driver.FindElement(By.Id(ConfigurationManager.AppSettings.Get("keyword"))).SendKeys(ConfigurationManager.AppSettings.Get("120225"));
                        //driver.FindElement(By.LinkText("智搜一下")).Click();
                        driver.FindElement(By.Id(ConfigurationManager.AppSettings.Get("searchBtn2"))).Click();
                        Thread.Sleep(fowardToSearchListTime);


                        //户政_常口基本信息——本页
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        Thread.Sleep(closeWindowTime);

                        driver.FindElements(By.Name(ConfigurationManager.AppSettings.Get("house"))).First().Click();
                        Thread.Sleep(fowardToHouseholdListTime);

                        //点击身份证号——新标签
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        Thread.Sleep(closeWindowTime);

                        IWebElement p = driver.SwitchTo().Frame("tableContainerIframe").FindElement(By.Id("divTableParent"));
                        ReadOnlyCollection<IWebElement> ps = p.FindElements(By.TagName(ConfigurationManager.AppSettings.Get("tr0")));
                        IWebElement ps1 = ps[RandomTrIndex()];
                        ps1.FindElement(By.ClassName(ConfigurationManager.AppSettings.Get("hyperlink_idCard"))).Click();
                        Thread.Sleep(fowardToPeopleInfoTime);

                        //关闭身份证号页面
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        driver.Close();
                        Thread.Sleep(closeWindowTime);
                        //关闭搜索页面
                        handles = driver.WindowHandles;
                        driver.SwitchTo().Window(handles.Last());
                        driver.Close();
                        Thread.Sleep(closeWindowTime);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }

            }

        }

        private int RandomTrIndex()
        {
            Random rd = new Random();
            return rd.Next(1, 11);
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("在此期间，请不要对本电脑进行任何操作。点击'是'，开始运行本软件。", "提示消息", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                if (driver == null)
                    throw new ArgumentNullException("driver为空");
                Thread t = new Thread(AutoStart);
                t.Start();
            };
            StartBtn.IsEnabled = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (driver != null)
            {
                driver.Quit();
            }
            App.Current.Shutdown();

        }
    }
}
