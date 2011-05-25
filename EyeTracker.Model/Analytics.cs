using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EyeTracker.DAL;
using System.Drawing.Imaging;

namespace EyeTracker.Model
{
    public class Analytics
    {
            public interface ITestDataRepository
            {
                List<ViewPartData> GetViewPartsData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to);
                List<ClickData> GetClicksData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to);
            }
            public class TestDataRepository : ITestDataRepository
            {
                private Random rnd = new Random();
                private int GetRandomInt(int min, int max)
                {
                    return rnd.Next(min, max);
                }

                public List<ViewPartData> GetViewPartsData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
                {
                    var data = new List<ViewPartData>();
                    for (int i = 0; i < 50; i++)
                    {
                        data.Add(new ViewPartData()
                        {
                            ScrollLeft = 0,
                            ScrollTop = GetRandomInt(0, clientHeight),
                            TimeSpan = GetRandomInt(5, 20)
                        });
                    }
                    return data;
                }

                public List<ClickData> GetClicksData(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
                {
                    List<int[]> points = new List<int[]> { 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) }, 
                    new int[] { GetRandomInt(20, clientWidth - 20), GetRandomInt(20, clientHeight - 20) } 
                };

                    var data = new List<ClickData>();
                    for (int i = 0; i < 50000; i++)
                    {
                        var curPnt = points[GetRandomInt(0, points.Count)];
                        data.Add(new ClickData()
                        {
                            ClientX = GetRandomInt(curPnt[0] - 20, curPnt[0] + 20),
                            ClientY = GetRandomInt(curPnt[1] - 20, curPnt[1] + 20)
                        });
                        data[i].Count = GetRandomInt(0, (20 - Math.Abs(curPnt[0] - data[i].ClientX)) * 5) + GetRandomInt(0, (20 - Math.Abs(curPnt[1] - data[i].ClientY)) * 5);
                    }
                    return data;
                }
            }

            private ITestDataRepository dataRepository;

            public Test()
                : this(new TestDataRepository())
            {

            }

            public Test(ITestDataRepository rep)
            {
                dataRepository = rep;
            }


    }
}
