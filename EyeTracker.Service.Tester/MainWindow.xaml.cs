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
using EyeTracker.API;

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
 
            SubmitService = ConfigurationSettings.AppSettings["submitService"];
            StatusService = ConfigurationSettings.AppSettings["checkStatus"];
        }

        /// <summary>
        /// Send sigle package to service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_single_point_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

                var mc = GetShortPackage();

                txtBlkServiceStatus.Text = SendPackage(mc).ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error while sending single package: " + ex.Message, "ETService", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        /// <summary>
        /// Send large chung of data to ET service 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_large_chunk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

                var mc = GetLongPackage(100);

                txtBlkServiceStatus.Text = SendPackage(mc).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while sending large package: " + ex.Message, "ETService", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// check status 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Check_status(object sender, RoutedEventArgs e)
        {
            try
            {
                ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

                txtBlkServiceStatus.Text =  CheckStatus();
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show("Error while checking status: " + ex.Message, "ETService", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btn_Save_Single_Pack(object sender, RoutedEventArgs e)
        {
            var mc = GetShortPackage();
            string package = Serialize<JsonPackage>(mc);
            
            using (StreamWriter outfile =
            new StreamWriter("SinglePackageTestData.txt"))
            {
                outfile.Write(package);
            }
        }

        private void btn_Save_Large_Chunk(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Package_From_File(object sender, RoutedEventArgs e)
        {

            ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

            using (StreamReader fileReader = new StreamReader("TestData\\SavedDataFromAndroid.txt"))
            {
                String str = fileReader.ReadToEnd();

                
                FPData obj = new FPData() { val = str};

               

                MemoryStream streamQ2 = new MemoryStream();
                DataContractJsonSerializer serializer2 = new DataContractJsonSerializer(typeof(FPData));
                serializer2.WriteObject(streamQ2, obj);

                //str = string.Format("{0}={1}", "val", str);

                // Encode the parameters as form data:
                //byte[] formData = UTF8Encoding.UTF8.GetBytes(paramz.ToString());

                SendToServer(streamQ2.ToArray());
            }

            
        }


        #region code

        /// <summary>
        /// CheckStatus service on ETService 
        /// 1. create WebClient
        /// 2. Get WebResponse 
        /// </summary>
        /// <returns></returns>
        private string CheckStatus()
        {
            //1.
            var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", ServiceUrl, StatusService)); 
  
            //2.
            using ( var response = (HttpWebResponse) request.GetResponse( ) )  
            {  
                var responseValue = string.Empty;  
  
                if ( response.StatusCode != HttpStatusCode.OK )  
                {  
                    string message = String.Format( "Status failed. Received HTTP {0}", response.StatusCode );  
                    MessageBox.Show(message, "ETService", MessageBoxButton.OK, MessageBoxImage.Error);
                }  
  
                // grab the response  
                using ( var responseStream = response.GetResponseStream() )  
                {  
                    using ( var reader = new StreamReader( responseStream ) )  
                    {  
                        responseValue = reader.ReadToEnd();  
                    }  
                }  
                return responseValue;  
            }  
 
        }


 
        private bool SendPackage(JsonPackage mc)
        {

            bool result = false;

            try
            {
                string package = Serialize<JsonPackage>(mc);
                txtxBlkServiceResponse.Text = package;
                MemoryStream streamQ2 = new MemoryStream();
                FPData obj = new FPData() { val = package };

                DataContractJsonSerializer serializer2 = new DataContractJsonSerializer(typeof(FPData));
                serializer2.WriteObject(streamQ2, obj);

                result = SendToServer(streamQ2.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }


        /// <summary>
        /// Actual sending to server
        /// </summary>
        /// <param name="arrayBytes"></param>
        /// <returns></returns>
        private bool SendToServer(byte[] arrayBytes)
        {
            string requestUrl = string.Format("{0}{1}", ServiceUrl, SubmitService);
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
                    return (bool)dcs.ReadObject(stream);
                }
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

        #endregion code

     


        #region DATA

        private JsonPackage GetLongPackage(int size)
        {
            JsonPackage objJP = new JsonPackage();
            objJP.ClientKey = "123" + new Random().Next(1, 5);
            objJP.ScreenHeight = SCREEN_HEIGHT;
            objJP.ScreenWidth = SCREEN_WIDTH;
            objJP.SessionsInfo = GetSessionInfo(size);

            return objJP;
        }

        private JsonPackage GetShortPackage()
        {
            JsonPackage objJP = new JsonPackage();
            objJP.ClientKey = "123" + new Random().Next(1, 5);
            objJP.ScreenHeight = SCREEN_HEIGHT;
            objJP.ScreenWidth = SCREEN_WIDTH;
            objJP.SessionsInfo = new JsonSessionInfo[1];
            objJP.SessionsInfo[0] = new JsonSessionInfo();
            objJP.SessionsInfo[0].ClientHeight = 800;
            objJP.SessionsInfo[0].ClientWidth = 500;
            objJP.SessionsInfo[0].PageUri = "myPageUri";

            DateTime dtStart, dtEnd;
            dtStart = DateTime.Now.AddSeconds(-30);
            dtEnd = DateTime.Now;

            objJP.SessionsInfo[0].SessionStartDate = dtStart.ToString("R");
            objJP.SessionsInfo[0].SessionCloseDate = dtEnd.ToString("R");
            objJP.SessionsInfo[0].TouchDetails = GetTouchDetails(dtStart, dtEnd);
            objJP.SessionsInfo[0].ScrollDetails = GetScrollDetails(objJP.SessionsInfo[0].TouchDetails[0], objJP.SessionsInfo[0].TouchDetails[2]);
            objJP.SessionsInfo[0].ViewAreaDetails = GetViewAreaDetails(dtStart, dtEnd);

            return objJP;
        }

        private JsonSessionInfo[] GetSessionInfo(int num)
        {
            var colSessions = new JsonSessionInfo[num];
            DateTime dtStart, dtEnd;

            for (int i = 0; i < num; i++)
            {
                colSessions[i] = new JsonSessionInfo();
                colSessions[i].ClientHeight = 800;
                colSessions[i].ClientWidth = 500;
                colSessions[i].PageUri = "myPageUri";

                dtStart = DateTime.Now.AddSeconds(-200 + i);
                dtEnd = DateTime.Now.AddSeconds(-200 + i + 2);

                colSessions[i].SessionStartDate = dtStart.ToString("R");

                colSessions[i].TouchDetails = GetTouchDetails(dtStart, dtEnd);
                colSessions[i].ScrollDetails = GetScrollDetails(colSessions[i].TouchDetails[0], colSessions[i].TouchDetails[2]);
                colSessions[i].ViewAreaDetails = GetViewAreaDetails(dtStart, dtEnd);

                colSessions[i].SessionCloseDate = dtEnd.ToString("R");
            }

            return colSessions;
        }

        private JsonViewAreaDetails[] GetViewAreaDetails(DateTime dtFrom, DateTime dtTo)
        {
            var colViewAreaDetails = new JsonViewAreaDetails[]
            {
                GetViewAreaDetail(dtFrom, dtTo),
                GetViewAreaDetail(dtFrom, dtTo),
                GetViewAreaDetail(dtFrom, dtTo)
            };
            return colViewAreaDetails;

        }

        private JsonViewAreaDetails GetViewAreaDetail(DateTime dtFrom, DateTime dtTo)
        {
            Random r = new Random();
            var objViewAreaDetail = new JsonViewAreaDetails();
            objViewAreaDetail.CoordX = r.Next(0, SCREEN_WIDTH);
            objViewAreaDetail.CoordY = r.Next(0, SCREEN_HEIGHT);

            objViewAreaDetail.StartDate = dtFrom.AddMilliseconds(10).ToString("R");
            objViewAreaDetail.FinishDate = dtTo.AddMilliseconds(-10).ToString("R");

            objViewAreaDetail.Orientation = 7;

            return objViewAreaDetail;
        }

        private JsonTouchDetails[] GetTouchDetails(DateTime dtFrom, DateTime dtTo)
        {
            var colTouchDeatils = new JsonTouchDetails[]
            {
                GetTouchDetail(dtFrom, dtTo),
                GetTouchDetail(dtFrom, dtTo),
                GetTouchDetail(dtFrom, dtTo)
            };
            return colTouchDeatils;
        }

        private JsonTouchDetails GetTouchDetail(DateTime dtFrom, DateTime dtTo)
        {
            Random r = new Random();
            JsonTouchDetails objTD = new JsonTouchDetails();
            objTD.ClientX = r.Next(0, SCREEN_WIDTH);
            objTD.ClientY = r.Next(0, SCREEN_HEIGHT);
            objTD.Date = dtFrom.AddMilliseconds((dtTo - dtFrom).TotalMilliseconds / 2).ToString("R");   // DateTime.Now.AddSeconds(10).ToString();   
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
        #endregion DATA

        #region Private memebers 
        const int SCREEN_HEIGHT = 960;
        const int SCREEN_WIDTH = 640;

        /// <summary>
        /// Hold Service URL
        /// </summary>
        private string ServiceUrl { get; set; }

        /// <summary>
        /// Holds Submit service name
        /// </summary>
        private string SubmitService { get; set; }

        /// <summary>
        /// Check status service 
        /// </summary>
        private string StatusService { get; set; }
        #endregion Private members 

      
       

       

    }

    
}
