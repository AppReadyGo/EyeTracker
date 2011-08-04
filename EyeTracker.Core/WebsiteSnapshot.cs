using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EyeTracker.Core
{
    public class WebsiteSnapshot
    {
        private string url { get; set; }
        private int? browserWidth { get; set; }
        private int? browserHeight { get; set; }
        private Bitmap bitmap { get; set; }
        
        public WebsiteSnapshot(string url, int? browserWidth = null, int? browserHeight = null)
        {
            this.url = url;
            this.browserWidth = browserWidth;
            this.browserHeight = browserHeight;
        } 
        
        public Bitmap GenerateWebSiteImage()
        {
            Thread thread = new Thread(new ThreadStart(_GenerateWebSiteImage));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return bitmap;
        } 

        private void _GenerateWebSiteImage()
        {
            var webBrowser = new WebBrowser();
            webBrowser.ScrollBarsEnabled = false;
            webBrowser.Navigate(url, "_self", null, "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows Phone OS 7.0; Trident/3.1; IEMobile/7.0) Asus;Galaxy6");
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowserDocumentCompleted);
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            webBrowser.Dispose();
        }

        private void WebBrowserDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var webBrowser = (WebBrowser)sender;
            if (!browserWidth.HasValue) browserWidth = webBrowser.Document.Body.ScrollRectangle.Width + webBrowser.Margin.Horizontal;
            if (!browserHeight.HasValue) browserHeight = webBrowser.Document.Body.ScrollRectangle.Height + webBrowser.Margin.Vertical;
            webBrowser.ClientSize = new Size(browserWidth.Value, browserHeight.Value);
            webBrowser.ScrollBarsEnabled = false;
            bitmap = new Bitmap(webBrowser.Bounds.Width, webBrowser.Bounds.Height);
            webBrowser.BringToFront();
            webBrowser.DrawToBitmap(bitmap, webBrowser.Bounds);
        } 

    }    
}
