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
            ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

            var mc = GetShortPackage();

            txtBlkServiceStatus.Text = SendPackage(mc).ToString();
        }

        /// <summary>
        /// Send large chung of data to ET service 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_large_chunk_Click(object sender, RoutedEventArgs e)
        {
            ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

            var mc = GetLongPackage(10);

            txtBlkServiceStatus.Text = SendPackage(mc).ToString();  
        }

        /// <summary>
        /// check status 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Check_status(object sender, RoutedEventArgs e)
        {
            ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

            txtBlkServiceStatus.Text =  CheckStatus();
        }


        private void btn_Package_From_File(object sender, RoutedEventArgs e)
        {
            ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

            using (StreamReader fileReader = new StreamReader("TestData\\SavedDataFromAndroid.txt"))
            {
                String str = fileReader.ReadToEnd();
                FPData obj = new FPData() { val = str };
                MemoryStream streamQ2 = new MemoryStream();
                DataContractJsonSerializer serializer2 = new DataContractJsonSerializer(typeof(FPData));
                serializer2.WriteObject(streamQ2, obj);

                SendToServer(streamQ2.ToArray());
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFillTestApp_Click(object sender, RoutedEventArgs e)
        {
            ServiceUrl = ConfigurationSettings.AppSettings[cb.SelectedIndex.ToString()];

            var jsonPackage1 = GetCustomPackage();

            SendPackage(jsonPackage1);

            var jsonPackage2 = GetCustomPackage();

            SendPackage(jsonPackage2);
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

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("Status failed. Received HTTP {0}", response.StatusCode);
                    MessageBox.Show(message, "ETService", MessageBoxButton.OK, MessageBoxImage.Error);
                    return "Error";
                }
                else
                {
                    // grab the response  
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                    }
                    return responseValue;
                }
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
            objJP.SessionsInfo[0].TouchDetails = GetTouchDetails(dtStart, dtEnd, 3, SCREEN_HEIGHT, SCREEN_WIDTH);
            objJP.SessionsInfo[0].ScrollDetails = GetScrollDetails(objJP.SessionsInfo[0].TouchDetails[0], objJP.SessionsInfo[0].TouchDetails[2], 1);
            objJP.SessionsInfo[0].ViewAreaDetails = GetViewAreaDetails(dtStart, dtEnd);

            return objJP;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientKey"></param>
        /// <param name="screenHeight"></param>
        /// <param name="screenWidth"></param>
        /// <param name="clientHeight"></param>
        /// <param name="clientWidth"></param>
        /// <param name="pageUri"></param>
        /// <returns></returns>
        private JsonPackage GetCustomPackage(string clientKey = "RunNow-RunNow-1" , int screenHeight = 800, int screenWidth = 480, 
            int clientHeight = 300, int clientWidth = 100, string pageUri = "ActivityPickerActivity", int touches = 50, int scrolls = 20, int views = 10)
        {
            JsonPackage objJP = new JsonPackage();
            objJP.ClientKey = clientKey;
            objJP.ScreenHeight = screenHeight;
            objJP.ScreenWidth = screenWidth;
            objJP.SessionsInfo = new JsonSessionInfo[1];
            objJP.SessionsInfo[0] = new JsonSessionInfo();
            objJP.SessionsInfo[0].ClientHeight = clientHeight;
            objJP.SessionsInfo[0].ClientWidth = clientWidth;
            objJP.SessionsInfo[0].PageUri = pageUri;

            DateTime dtStart, dtEnd;
            dtStart = DateTime.Now.AddSeconds(-30);
            dtEnd = DateTime.Now;

            objJP.SessionsInfo[0].SessionStartDate = dtStart.ToString("R");
            objJP.SessionsInfo[0].SessionCloseDate = dtEnd.ToString("R");
            objJP.SessionsInfo[0].TouchDetails = GetTouchDetails(dtStart, dtEnd, touches, clientHeight, clientWidth);
            objJP.SessionsInfo[0].ScrollDetails = GetScrollDetails(objJP.SessionsInfo[0].TouchDetails[0], objJP.SessionsInfo[0].TouchDetails[2], scrolls);
            objJP.SessionsInfo[0].ViewAreaDetails = GetViewAreaDetails(dtStart, dtEnd, views, clientHeight, clientWidth);

            objJP.SystemInfo = GetSystemInfo();
            return objJP;
        }



        /// <summary>
        /// Create basic system info for Samsung Galaxy 2
        /// </summary>
        /// <returns></returns>
        private JsonSystemInfo GetSystemInfo()
        {
            JsonSystemInfo jsi = new JsonSystemInfo();
            jsi.BrandName = "a3d";
            jsi.DeviceName = "2sd";
            jsi.DisplayName = "asd";
            jsi.FingerprintName = "a4d";
            jsi.HardwareName = "lsd";
            jsi.ManufactureName = "a4d";
            jsi.ModelName = "asf";
            jsi.OperatorName = "bss";
            jsi.ProductName = "GT-P1000";
            jsi.DevCodeName = "REL";
            jsi.InternalName = "XWJP9";
            jsi.RealVersionName = "2.3.3";
            jsi.SdkIdentName = "10";

            return jsi;

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

                colSessions[i].TouchDetails = GetTouchDetails(dtStart, dtEnd, 3, SCREEN_HEIGHT, SCREEN_WIDTH);
                //todo!
                colSessions[i].ScrollDetails = GetScrollDetails(colSessions[i].TouchDetails[0], colSessions[i].TouchDetails[2], 1);
                colSessions[i].ViewAreaDetails = GetViewAreaDetails(dtStart, dtEnd);

                colSessions[i].SessionCloseDate = dtEnd.ToString("R");
            }

            return colSessions;
        }

        private JsonViewAreaDetails[] GetViewAreaDetails(DateTime dtFrom, DateTime dtTo, int count = 3, int clientHeight = SCREEN_HEIGHT, int clientWidth = SCREEN_WIDTH)
        {
            var colViewAreaDetails = new JsonViewAreaDetails[count];
            //{
            //    GetViewAreaDetail(dtFrom, dtTo),
            //    GetViewAreaDetail(dtFrom, dtTo),
            //    GetViewAreaDetail(dtFrom, dtTo)
            //};
            for (int i = 0; i < count; i++)
            {
                colViewAreaDetails[i] = GetViewAreaDetail(dtFrom, dtTo, clientHeight, clientWidth);
            }
            return colViewAreaDetails;

        }

        private JsonViewAreaDetails GetViewAreaDetail(DateTime dtFrom, DateTime dtTo, int clientHeight = SCREEN_HEIGHT, int clientWidth = SCREEN_WIDTH)
        {
            Random r = new Random();
            var objViewAreaDetail = new JsonViewAreaDetails();
            objViewAreaDetail.CoordX = r.Next(0, clientWidth);
            objViewAreaDetail.CoordY = r.Next(0, clientHeight);

            objViewAreaDetail.StartDate = dtFrom.AddMilliseconds(10).ToString("R");
            objViewAreaDetail.FinishDate = dtTo.AddMilliseconds(-10).ToString("R");

            objViewAreaDetail.Orientation = 7; //??

            return objViewAreaDetail;
        }

        private JsonTouchDetails[] GetTouchDetails(DateTime dtFrom, DateTime dtTo, int count, int clientHeight, int clientWidth)
        {
            var colTouchDeatils = new JsonTouchDetails[count];
            for(int i = 0; i<count; i++)
            {
                colTouchDeatils[i] = GetTouchDetail(dtFrom, dtTo, clientHeight, clientWidth);
                //GetTouchDetail(dtFrom, dtTo),
                //GetTouchDetail(dtFrom, dtTo),
                //GetTouchDetail(dtFrom, dtTo)
            }
            return colTouchDeatils;
        }

        private JsonTouchDetails GetTouchDetail(DateTime dtFrom, DateTime dtTo, int clientHeight, int clientWidth)
        {
            Random r = new Random();
            JsonTouchDetails objTD = new JsonTouchDetails();
            objTD.ClientX = r.Next(0, clientWidth);
            objTD.ClientY = r.Next(0, clientHeight);
            objTD.Date = dtFrom.AddMilliseconds((dtTo - dtFrom).TotalMilliseconds / 2).ToString("R");   // DateTime.Now.AddSeconds(10).ToString();   
            objTD.Press = r.Next(1, 100);
            objTD.Orientation = 0; //portrait 
            return objTD;
        }

        private JsonTouchDetails GetTouchDetail(DateTime dtFrom, DateTime dtTo)
        {
            return GetTouchDetail(dtFrom, dtTo, SCREEN_HEIGHT, SCREEN_WIDTH);
            //Random r = new Random();
            //JsonTouchDetails objTD = new JsonTouchDetails();
            //objTD.ClientX = r.Next(0, SCREEN_WIDTH);
            //objTD.ClientY = r.Next(0, SCREEN_HEIGHT);
            //objTD.Date = dtFrom.AddMilliseconds((dtTo - dtFrom).TotalMilliseconds / 2).ToString("R");   // DateTime.Now.AddSeconds(10).ToString();   
            //objTD.Press = r.Next(1, 100);
            //objTD.Orientation = 7;
            //return objTD;
        }

        private JsonScrollDetails[] GetScrollDetails(JsonTouchDetails p_objStartTouch, JsonTouchDetails p_objEndTouch, int count)
        {
            //count: todo!
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

        
        ///// <summary>
        ///// save single 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btn_Save_Single_Pack(object sender, RoutedEventArgs e)
        //{
        //    var mc = GetShortPackage();
        //    string package = Serialize<JsonPackage>(mc);

        //    using (StreamWriter outfile =
        //    new StreamWriter("SinglePackageTestData.txt"))
        //    {
        //        outfile.Write(package);
        //    }
        //}

       

    }

    
}
