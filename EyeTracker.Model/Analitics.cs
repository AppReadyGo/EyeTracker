using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using EyeTracker.DAL;

namespace EyeTracker.Model
{
    public class Analitics
    {
        public static Image GetViewHeatMap(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
        {
            int pageWidth = screenWidth, pageHeight = 500;
            int[,] heatMap = new int[pageWidth, pageHeight];
            int maxTimeSpan = 20;//The time span is up color of heat map

            List<ViewPartData> viewParts = AnaliticsData.GetViewPartsData(userAppId, pageUri, screenWidth, screenHeight, clientWidth, clientHeight, from, to);
            foreach (var curPart in viewParts)
            {
                for (int i = curPart.ScrollLeft; i < curPart.ScrollLeft + clientWidth; i++)
                {
                    for (int j = curPart.ScrollTop; j < curPart.ScrollTop + clientHeight; j++)
                    {
                        heatMap[i, j] += curPart.TimeSpan;
                        if (heatMap[i, j] > maxTimeSpan) maxTimeSpan = heatMap[i, j];
                    }
                }
            }

            Bitmap bmpPic = new Bitmap(pageWidth, pageHeight);
            for (int i = 0; i < pageWidth; i++)
            {
                for (int j = 0; j < pageHeight; j++)
                {
                    bmpPic.SetPixel(i, j, getColorFromWaveLength(((int)(heatMap[i, j] * 204 / maxTimeSpan)) + 440));
                }
            }
            bmpPic = SetImgOpacity((Image)bmpPic, 0.5f);
            return bmpPic;
        }

        public static Image GetClickHeatMap(long userAppId, string pageUri, int screenWidth, int screenHeight, int clientWidth, int clientHeight, DateTime from, DateTime to)
        {
            int pageWidth = screenWidth, pageHeight = 500;
            List<ClickData> clicks = AnaliticsData.GetClicksData(userAppId, pageUri, screenWidth, screenHeight, clientWidth, clientHeight, from, to);
            int maxCounter = clicks.Max(curClick => curClick.Count);

            Bitmap bmpPic = new Bitmap(pageWidth, pageHeight);
            foreach (var curClick in clicks)
            {
                bmpPic.SetPixel(curClick.ClientX, curClick.ClientY, getColorFromWaveLength(((int)(curClick.Count * 204 / maxCounter)) + 440));
            }
            bmpPic = SetImgOpacity((Image)bmpPic, 0.5f);
            return bmpPic;

        }
    }
}
