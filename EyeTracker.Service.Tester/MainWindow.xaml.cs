using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EyeTracker.API.BL.Contract;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System.Configuration;
using System.Threading;

namespace EyeTracker.Service.Tester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        #region code

        private void GetStatus()
        {
            try
            {
                //Create the REST request.
                var url = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

                var service = ConfigurationSettings.AppSettings["submitService"];

                var mc = GetPackage();

                string package = Serialize<JsonPackage>(mc);
                //string package = "hello world";
                //MessageBox.Show(package);
                textBlock1.Text = package;
                MemoryStream streamQ2 = new MemoryStream();
                DataContractJsonSerializer serializer2 = new DataContractJsonSerializer(typeof(String));
                serializer2.WriteObject(streamQ2, package);
                var arrayBytes = streamQ2.ToArray();
                //var arrayBytes = Serialize<JsonPackage>(mc);
                
                string requestUrl = string.Format("{0}{1}", url, service);
                WebRequest request = WebRequest.Create(requestUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = arrayBytes.Length;

                request.GetRequestStream().Write(arrayBytes, 0, arrayBytes.Length);
                request.GetRequestStream().Close();

                // Get response  
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        DataContractJsonSerializer dcs = new DataContractJsonSerializer(typeof(bool));
                        bool result = (bool)dcs.ReadObject(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore while retrieving photos: " + ex.Message, "ETService", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        private JsonPackage GetPackage()
        {
            JsonPackage objJP = new JsonPackage();
            objJP.ClientKey = "123";
            objJP.ScreenHeight = 1280;
            objJP.ScreenWidth = 600;
            objJP.SessionsInfo = new JsonSessionInfo[1];
            objJP.SessionsInfo[0] = new JsonSessionInfo();
            objJP.SessionsInfo[0].ClientHeight = 1000;
            objJP.SessionsInfo[0].ClientWidth = 500;
            objJP.SessionsInfo[0].PageUri = "myPageUri";
            objJP.SessionsInfo[0].SessionStartDate = DateTime.Now.AddSeconds(-30).ToString();
            objJP.SessionsInfo[0].SessionCloseDate = DateTime.Now.ToString();
            objJP.SessionsInfo[0].TouchDetails = GetTouchDetails();
            objJP.SessionsInfo[0].ScrollDetails = GetScrollDetails(objJP.SessionsInfo[0].TouchDetails[0], objJP.SessionsInfo[0].TouchDetails[2]);
            objJP.SessionsInfo[0].ViewAreaDetails = GetViewAreaDetails();

            return objJP;
        }

        private JsonViewAreaDetails[] GetViewAreaDetails()
        {
            var colViewAreaDetails = new JsonViewAreaDetails[]
            {
                GetViewAreaDetail(),
                GetViewAreaDetail(),
                GetViewAreaDetail()
            };
            return colViewAreaDetails;

        }

        private JsonViewAreaDetails GetViewAreaDetail()
        {
            Random r = new Random();
            var objViewAreaDetail = new JsonViewAreaDetails();
            objViewAreaDetail.CoordX = r.Next(0, 600);
            objViewAreaDetail.CoordY = r.Next(0, 1280);
            objViewAreaDetail.StartDate = DateTime.Now.AddSeconds(-10).ToString();
            objViewAreaDetail.FinishDate = DateTime.Now.AddSeconds(-9).ToString();
            objViewAreaDetail.Orientation = 7;

            return objViewAreaDetail;
        }

        private JsonTouchDetails[] GetTouchDetails()
        {
            var colTouchDeatils = new JsonTouchDetails[]
            {
                GetTouchDetail(),
                GetTouchDetail(),
                GetTouchDetail()
            };
            return colTouchDeatils;
        }

        private JsonTouchDetails GetTouchDetail()
        {
            Random r = new Random();
            JsonTouchDetails objTD = new JsonTouchDetails();
            objTD.ClientX = r.Next(0, 600);
            objTD.ClientY = r.Next(0, 1280);
            objTD.Date = DateTime.Now.AddSeconds(10).ToString();   //(-r.Next(0, 30)).ToString();
            objTD.Press = r.Next(1, 100);
            objTD.Orientation = 7;
            return objTD;
        }

        private JsonScrollDetails[] GetScrollDetails(JsonTouchDetails p_objStartTouch, JsonTouchDetails p_objEndTouch)
        {
            JsonScrollDetails objScrollDetails = new JsonScrollDetails();
            objScrollDetails.StartTouchData = p_objStartTouch;
            objScrollDetails.CloseTouchData = p_objEndTouch;

            var colScrollDetails = new JsonScrollDetails[]
            {
                objScrollDetails
            };
            return colScrollDetails;
        }
        #endregion code

        private void btn_single_point_Click(object sender, RoutedEventArgs e)
        {
            GetStatus();
        }

    }

    
}
