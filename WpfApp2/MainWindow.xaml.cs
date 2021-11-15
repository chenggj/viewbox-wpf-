using Microsoft.Win32;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private string _adContentImage = "";

        public string AdContentImage
        {
            get
            {
                return this._adContentImage;
            }

            set
            {
                this._adContentImage = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("AdContentImage"));
                }
            }
        }

        private string _adMarkImage = "";

        public string AdMarkImage
        {
            get
            {
                return this._adMarkImage;
            }

            set
            {
                this._adMarkImage = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("AdMarkImage"));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Test();

            SetBrowserFeatureControl();

            string html = "<html><body style='margin:0;padding:0;'><script src='https://ajax.aspnetcdn.com/ajax/jquery/jquery-2.1.1.min.js'></script> <script> function clickTrack() {$.get('http://trackingiaxtest.optimix.cn/adck?s=214&bid=10j1614068130165&pub=81&d=-1&did=-1&cid=65&aid=40&lr='); }; </script><div style='position:relative;width:100%;'> <a href='https://www.i-click.com/ ' target='_blank'><img onclick='clickTrack()' style='width:100%;border:0px;' src='http://staticiaxtest.optimix.cn/ad/2021/01/04/07030440673116.jpg'></a><img src='http://static.iax.optimix.cn/ad/cornermark.png' style='position:absolute;bottom:0;right:0;'/></div></body></html>";

            //this.browser.NavigateToString(html);
            //browser.LoadCompleted += Browser_LoadCompleted;

        }

        private void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //mshtml.HTMLDocument doc = browser.Document as mshtml.HTMLDocument;
            //mshtml.IHTMLDocument3 doc3 = (mshtml.IHTMLDocument3)doc;
            //foreach (mshtml.IHTMLElement hTML in doc3.getElementsByTagName("IMG"))
            //{

            //    //mshtml.HTMLImgClass image = hTML as mshtml.HTMLImgClass;
            //    if (!((mshtml.HTMLImgClass)hTML).nameProp.Contains("cornermark.png"))//== "cornermark.png"
            //    {
            //        this.AdContentImage = hTML.getAttribute("src").ToString();
            //    }
            //    else
            //    {
            //        this.AdMarkImage = hTML.getAttribute("src").ToString();
            //    }

            //    foreach (mshtml.IHTMLElement aref in doc3.getElementsByTagName("A"))
            //    {

            //       // this.AdContentImage = aref.getAttribute("href").ToString();
            //    }
            //    //hTML.click();
            //}
        }

        private List<mshtml.IHTMLElement> GetImageList(mshtml.IHTMLElement root)
        {
            List<mshtml.IHTMLElement> result = new List<IHTMLElement>();
            foreach (mshtml.IHTMLElement htmlElement in (mshtml.IHTMLElementCollection)root.children)
            {
                if (htmlElement.tagName.ToLower() == "img")
                {
                    result.Add(htmlElement);
                }
                else if (htmlElement.children != null)
                {
                    result.AddRange(GetImageList(htmlElement));
                }
            }

            return result;

        }


        private void SetBrowserFeatureControl()
        {
            // http://msdn.microsoft.com/en-us/library/ee330720(v=vs.85).aspx

            // FeatureControl settings are per-process
            var fileName = System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // make the control is not running inside Visual Studio Designer
            if (String.Compare(fileName, "devenv.exe", true) == 0 || String.Compare(fileName, "XDesProc.exe", true) == 0)
                return;

            SetBrowserFeatureControlKey("FEATURE_BROWSER_EMULATION", fileName, GetBrowserEmulationMode());
            // Webpages containing standards-based !DOCTYPE directives are displayed in IE10 Standards mode.
            //SetBrowserFeatureControlKey("FEATURE_AJAX_CONNECTIONEVENTS", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", fileName, 1);
            // SetBrowserFeatureControlKey("FEATURE_MANAGE_SCRIPT_CIRCULAR_REFS", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_DOMSTORAGE ", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_GPU_RENDERING ", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_IVIEWOBJECTDRAW_DMLT9_WITH_GDI  ", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_DISABLE_LEGACY_COMPRESSION", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_LOCALMACHINE_LOCKDOWN", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_OBJECT", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_BLOCK_LMZ_SCRIPT", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_DISABLE_NAVIGATION_SOUNDS", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_SCRIPTURL_MITIGATION", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_SPELLCHECKING", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_STATUS_BAR_THROTTLING", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_TABBED_BROWSING", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_VALIDATE_NAVIGATE_URL", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_WEBOC_DOCUMENT_ZOOM", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_WEBOC_POPUPMANAGEMENT", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_WEBOC_MOVESIZECHILD", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_ADDON_MANAGEMENT", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_WEBSOCKET", fileName, 1);
            //SetBrowserFeatureControlKey("FEATURE_WINDOW_RESTRICTIONS ", fileName, 0);
            //SetBrowserFeatureControlKey("FEATURE_XMLHTTP", fileName, 1);
        }
        private void SetBrowserFeatureControlKey(string feature, string appName, uint value)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(
                String.Concat(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\", feature),
                RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                key.SetValue(appName, (UInt32)value, RegistryValueKind.DWord);
            }
        }

        private UInt32 GetBrowserEmulationMode()
        {
            int browserVersion = 7;
            using (var ieKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer",
                RegistryKeyPermissionCheck.ReadSubTree,
                System.Security.AccessControl.RegistryRights.QueryValues))
            {
                var version = ieKey.GetValue("svcVersion");
                if (null == version)
                {
                    version = ieKey.GetValue("Version");
                    if (null == version)
                        throw new ApplicationException("Microsoft Internet Explorer is required!");
                }
                int.TryParse(version.ToString().Split('.')[0], out browserVersion);
            }

            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode. Default value for Internet Explorer 11.
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode. Default value for applications hosting the WebBrowser Control.
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode. Default value for Internet Explorer 8
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode. Default value for Internet Explorer 9.
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10. Webpages containing standards-based !DOCTYPE directives are displayed in IE10 mode. Default value for Internet Explorer 10.
                    break;
                default:
                    // use IE11 mode by default
                    break;
            }

            return mode;
        }

        private void Test()
        {
            string str1 = "Qi Longshan told Lijun that he was the daughter chosen.\nWhat was chosen, this is also what we include plastic sisters Sophie Sophie and Li Yuxuan.\nDing Shan told Lijun that he was the girl chosen. What the";
            string str2 = "Ding shan told Lijun, he is the girl selected, what was selected?";
            int startindex = 147;
            int oldlength = str1.Length;
            int newlength = str2.Length;

            int i, j;
            for (i = startindex, j = 0; i < oldlength && j < newlength; i++, j++)
            {
                if (str1[i] != str2[j])
                {
                    str1 = str1.Remove(i, 1);
                    str1 = str1.Insert(i, str2[j].ToString());
                }
            }

            if (i == oldlength)
            {
                string appendStr = str2.Substring(j);
                int firstWhitespaceIndex = appendStr.IndexOf(' ');
                if (firstWhitespaceIndex < 0)
                {
                    ////indicate that the not completed word is the last one.
                    str1 += appendStr;
                    j += appendStr.Length;
                }
                else
                {
                    str1 += str1.Substring(j, firstWhitespaceIndex);
                    j += firstWhitespaceIndex;
                }


                if (j < newlength
                    && !string.IsNullOrWhiteSpace(str2[j].ToString()))
                {

                }

                if (j < newlength)
                {
                    string str3 = str2.Substring(j).Trim();
                    // appendWords = this.StringToWordsList();
                }

            }
            else
            {

                str1 = str1.Remove(i, oldlength - i);


            }
        }

        private void adContent_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //mshtml.HTMLDocument doc = browser.Document as mshtml.HTMLDocument;
            //mshtml.IHTMLDocument3 doc3 = (mshtml.IHTMLDocument3)doc;
            //foreach (mshtml.IHTMLElement hTML in doc3.getElementsByTagName("IMG"))
            //{
            //    //mshtml.HTMLImgClass image = hTML as mshtml.HTMLImgClass;
            //    if (!((mshtml.HTMLImgClass)hTML).nameProp.Contains("cornermark.png"))
            //    {
            //        hTML.click();
            //    }

            //    //foreach (mshtml.IHTMLElement aref in doc3.getElementsByTagName("A"))
            //    //{

            //    //    string url = aref.getAttribute("href").ToString();

            //    //    Process.Start(url);

            //    //    break;
            //    //}
            //}

        }

        private void adContent_Click(object sender, RoutedEventArgs e)
        {
            //mshtml.HTMLDocument doc = browser.Document as mshtml.HTMLDocument;
            //mshtml.IHTMLDocument3 doc3 = (mshtml.IHTMLDocument3)doc;
            //foreach (mshtml.IHTMLElement hTML in doc3.getElementsByTagName("IMG"))
            //{
            //    //mshtml.HTMLImgClass image = hTML as mshtml.HTMLImgClass;
            //    if (!((mshtml.HTMLImgClass)hTML).nameProp.Contains("cornermark.png"))
            //    {
            //        hTML.click();
            //    }

            //    foreach (mshtml.IHTMLElement aref in doc3.getElementsByTagName("A"))
            //    {

            //        string url = aref.getAttribute("href").ToString();

            //        Process.Start(url);

            //        break;
            //    }
            //}
        }
    }
}
