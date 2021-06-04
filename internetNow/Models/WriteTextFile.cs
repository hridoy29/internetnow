using Magnum.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using internetNow.Models;
namespace internetNow.Models
{
    public class WriteTextFile
    {
        public async Task Write(string obj)
        {
            try
            {
                var path = HttpContext.Current.Server.MapPath("~/Content/Output" + ".txt");
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    await sw.WriteLineAsync(obj + ",");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool FileSize(string obj)
        {
            try
            {
                bool iswriteable = true;
                var path = HttpContext.Current.Server.MapPath("~/Content/Output" + ".txt");
                FileInfo fi = new FileInfo(path);
                long size = fi.Length;
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                int order = 0;
                while (size >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    size = size / 1024;
                }
                string result = String.Format("{0:0.##}{1}", size, sizes[order]);
                if (result == obj)
                {
                    iswriteable = false;
                }
                return iswriteable;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<RatioModel>> Report()
        {

            string line;
            var path = HttpContext.Current.Server.MapPath("~/Content/Output" + ".txt");
            StreamReader fi = new StreamReader(path);
            int NumaricCount = 0;
            int AlphanumaricCount = 0;
            int FloatCount = 0;
            string NumaricCountRatio = string.Empty;
            string AlphanumaricCountRatio = string.Empty;
            string FloatCountRatio = string.Empty;
            try
            {

                while ((line = await fi.ReadLineAsync()) != null)
                {
                    bool isNumaric = line.Contains("-N");
                    if (isNumaric == true)
                    {
                        NumaricCount++;
                    }
                    bool isAlphanumaric = line.Contains("-A");
                    if (isAlphanumaric == true)
                    {
                        AlphanumaricCount++;
                    }
                    bool isFloat = line.Contains("-F");
                    if (isFloat == true)
                    {
                        FloatCount++;
                    }
                }
                try
                {
                    NumaricCountRatio = "% " + ((NumaricCount * 100) / (NumaricCount + AlphanumaricCount + FloatCount)).ToString();
                    AlphanumaricCountRatio = "% " + ((AlphanumaricCount * 100) / (NumaricCount + AlphanumaricCount + FloatCount)).ToString();
                    FloatCountRatio = "% " + ((FloatCount * 100) / (NumaricCount + AlphanumaricCount + FloatCount)).ToString();

                   
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                fi.Close();

            }
            try
            {
                List<RatioModel> Ratio = new List<RatioModel>()
                    {
                        new RatioModel(){ NumaricCountRatioProperty = NumaricCountRatio,
                                          AlphanumaricCountRatioProperty=AlphanumaricCountRatio,
                                          FloatCountRatioProperty=FloatCountRatio}
                    };

                return Ratio;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

        }

    }
}