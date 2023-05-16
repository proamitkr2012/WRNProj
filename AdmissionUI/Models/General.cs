using System.Collections;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace AdmissionUI.Models
{
    public class General
    {

        public static bool GetBitsStatus(long statusId, long toCheckStatusID)
        {
            return ((statusId | (long)Math.Pow(2, toCheckStatusID - 1)) == statusId ? true : false);

        }
        public static bool GetBitsStatusUlong(ulong statusId, long toCheckStatusID)
        {
            return ((statusId | (ulong)Math.Pow(2, toCheckStatusID - 1)) == statusId ? true : false);

        }

        public static long SetStatus(long oldStatus, long newStatusID)
        {
            newStatusID = oldStatus | ((long)Math.Pow(2, newStatusID - 1));
            return newStatusID;
        }

        public static ulong SetStatusULONG(ulong oldStatus, ulong newStatusID)
        {
            newStatusID = oldStatus | ((ulong)Math.Pow(2, newStatusID - 1));
            return newStatusID;
        }

        public static long ResetStatus(long oldStatus, long newStatusID)
        {
            long statusId = oldStatus;
            newStatusID = (statusId ^ (int)Math.Pow(2, newStatusID - 1));
            statusId = statusId & newStatusID;
            return statusId;
        }

        public static ulong ResetStatusUlong(ulong oldStatus, ulong newStatusID)
        {
            ulong statusId = oldStatus;
            newStatusID = (statusId ^ (uint)Math.Pow(2, newStatusID - 1));
            statusId = statusId & newStatusID;
            return statusId;
        }


        public static string QuotedString(string source)
        {
            Regex objreg = new Regex("'");
            return objreg.Replace(source, "''");
        }

        public static byte[] ConvertStringToByteArray(string stringToConvert)
        {
            return (new UnicodeEncoding()).GetBytes(stringToConvert);
        }

        public static string ByteArrayToConvertString(byte[] stringinBytes)
        {
            return (new UnicodeEncoding()).GetString(stringinBytes);
        }


        public static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);

        }

        public static Byte[] StringToUTF8ByteArray(String pXmlString)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }



        public static byte[] DownloadFile(string FileNameWithPath)
        {
            System.IO.FileStream ObjFileStream = null;
            ObjFileStream = System.IO.File.Open(FileNameWithPath, FileMode.Open, FileAccess.Read);
            byte[] Objbyte = new byte[ObjFileStream.Length];
            ObjFileStream.Read(Objbyte, 0, (int)ObjFileStream.Length);
            ObjFileStream.Close();
            return Objbyte;
        }

        public static DataTable CreateDataTable(string Col, string datatype)
        {
            string[] columns = Col.Split(",".ToCharArray());
            string[] types = datatype.Split(",".ToCharArray());
            DataTable objTable = new DataTable();
            int count = 0;

            foreach (string clmn in columns)
            {
                string setType = "";
                switch (types[count].Trim())
                {
                    case "string":
                        setType = "System.String";
                        break;
                    case "int":
                        setType = "System.Int32";
                        break;
                    case "arrbyte":
                        setType = "System.Byte[]";
                        break;

                    case "decimal":
                        setType = "System.Decimal";
                        break;
                }
                objTable.Columns.Add(new DataColumn(clmn, System.Type.GetType(setType)));
                count++;
            }

            return objTable;

        }


        public string DateConvert(string DatetoCon, int datecode)
        {
            //1-dd/mm/yyyy
            //2-mm/dd/yyyy

            string jdate = DatetoCon;
            string convertdate = DatetoCon;
            string dd = DatetoCon.Substring(0, DatetoCon.IndexOf('/'));
            jdate = jdate.Substring(jdate.IndexOf('/') + 1, jdate.Length - (jdate.IndexOf('/') + 1));
            string MM = jdate.Substring(0, jdate.IndexOf('/'));
            string yyyy = jdate.Substring(jdate.IndexOf('/') + 1, jdate.Length - (jdate.IndexOf('/') + 1));
            switch (datecode)
            {
                case 1:
                    convertdate = dd + "/" + MM + "/" + yyyy;
                    break;

                case 2:
                    convertdate = MM + "/" + dd + "/" + yyyy;
                    break;

            }
            return convertdate.Trim();

        }


        public static DateTime convertTodate(string date)
        {
            CultureInfo cultInfo = new CultureInfo("en-GB", true);
            DateTimeFormatInfo formatInfo = cultInfo.DateTimeFormat;
            formatInfo.ShortDatePattern = "dd/MM/yy";
            formatInfo.ShortDatePattern = "dd/MM/yyyy";
            formatInfo.LongDatePattern = "dd MMMM yyyy";
            formatInfo.FullDateTimePattern = "dd MMMM yyyy HH:mm:ss";
            return System.Convert.ToDateTime(date, formatInfo);

        }



        public static int CompareDates(string strStartDate, string strEndDate)
        {
            try
            {
                // Creates and initializes the CultureInfo which uses the international sort.
                //I have used English (United Kingdom) cultural inforamtion to convert data into dd/MM/yyyy format 
                CultureInfo cultInfo = new CultureInfo("en-GB", true);
                DateTimeFormatInfo formatInfo = cultInfo.DateTimeFormat;
                formatInfo.ShortDatePattern = "dd/MM/yy";
                formatInfo.ShortDatePattern = "dd/MM/yyyy";
                formatInfo.LongDatePattern = "dd MMMM yyyy";
                formatInfo.FullDateTimePattern = "dd MMMM yyyy HH:mm:ss";

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                //Convert strStartDate which is passed as string argument into date
                if (!String.IsNullOrEmpty(strStartDate))
                    startDate = System.Convert.ToDateTime(strStartDate, formatInfo);

                //Convert strEndDate which is passed as string argument into date
                if (!String.IsNullOrEmpty(strEndDate))
                    endDate = System.Convert.ToDateTime(strEndDate, formatInfo);

                return DateTime.Compare(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static double NumberofDays(string strStartDate, string strEndDate)
        {
            try
            {
                // Creates and initializes the CultureInfo which uses the international sort.
                //I have used English (United Kingdom) cultural inforamtion to convert data into dd/MM/yyyy format 
                CultureInfo cultInfo = new CultureInfo("en-GB", true);
                DateTimeFormatInfo formatInfo = cultInfo.DateTimeFormat;
                formatInfo.ShortDatePattern = "dd/MM/yy";
                formatInfo.ShortDatePattern = "dd/MM/yyyy";
                formatInfo.LongDatePattern = "dd MMMM yyyy";
                formatInfo.FullDateTimePattern = "dd MMMM yyyy HH:mm:ss";

                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                //Convert strStartDate which is passed as string argument into date
                if (!String.IsNullOrEmpty(strStartDate))
                    startDate = System.Convert.ToDateTime(strStartDate, formatInfo);

                //Convert strEndDate which is passed as string argument into date
                if (!String.IsNullOrEmpty(strEndDate))
                    endDate = System.Convert.ToDateTime(strEndDate, formatInfo);

                TimeSpan t = startDate - endDate;
                double NrOfDays = t.TotalDays;

                return NrOfDays;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }









        public static bool ISVALIDATEDates(string strStartDate)
        {
            bool flag = true;
            try
            {
                // Creates and initializes the CultureInfo which uses the international sort.
                //I have used English (United Kingdom) cultural inforamtion to convert data into dd/MM/yyyy format 
                CultureInfo cultInfo = new CultureInfo("en-GB", true);
                DateTimeFormatInfo formatInfo = cultInfo.DateTimeFormat;
                formatInfo.ShortDatePattern = "dd/MM/yy";
                formatInfo.ShortDatePattern = "dd/MM/yyyy";
                formatInfo.LongDatePattern = "dd MMMM yyyy";
                formatInfo.FullDateTimePattern = "dd MMMM yyyy HH:mm:ss";
                DateTime startDate = new DateTime();
                //Convert strStartDate which is passed as string argument into date
                if (!String.IsNullOrEmpty(strStartDate))
                    startDate = System.Convert.ToDateTime(strStartDate, formatInfo);

            }
            catch (Exception ex)
            {
                // throw ex;
                flag = false;
            }

            return flag;
        }


        public static string Encryptdata(string password)
        {
            password = ReverseString(password);
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }


        public static string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            decryptpwd = ReverseString(decryptpwd);
            return decryptpwd;
        }

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }










        public static int getDaysInMonth(int year, int month)
        {

            int[] days = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (month == 1 && year % 4 == 0 && ((year % 100 == 0) || (year % 400 == 0)))
            {
                return 29;
            }


            return days[month];
        }

        public static double ConvertHourMinTODecimal(string str)
        {

            int idx = str.IndexOf(".");
            double d = Convert.ToDouble(str);
            double hours = Math.Floor(d);
            double min = d - hours;
            int len = str.Substring(idx + 1, str.Length - (idx + 1)).Length;
            min = min * 100; //(len == 2) ? min * 100 : (len == 1) ? min * 10 : min * 0;
            min = Math.Round(min * 0.01666, 2);
            // min = min / 100;// (len == 2) ? min / 100 : (len == 1) ? min / 10 : min * 0;
            double res = hours + min;
            return res;

        }

        public static double ConvertDecimalTOHourMin(double Value)
        {

            double Hours = Math.Floor(Value);
            double Minutes = Value - Math.Floor(Value);
            if (Minutes > 0)
            {
                Minutes = (Minutes / .0166) / 100;
                Minutes = Math.Round(Minutes, 2);

            }
            return (Hours + Minutes);


        }



        public static string CalculateHoursFromMinute(int mint)
        {

            string hours = "";
            if (mint >= 60)
            {
                hours = (mint / 60).ToString();
                if (mint % 60 > 0)
                {
                    hours = hours + ":" + (mint % 60);
                }
                else
                {
                    hours = hours + ":00";
                }


            }
            else
            {
                if (mint < 10 && mint < 60)
                {
                    hours = "00:0" + (mint % 60);
                }
                else if (mint >= 10 && mint < 60)
                {
                    hours = "00:" + (mint % 60);
                }
            }
            return hours;
        }


        //public static string DataSetToJSON(DataSet ds)
        //{

        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    foreach (DataTable dt in ds.Tables)
        //    {
        //        object[] arr = new object[dt.Rows.Count + 1];

        //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            arr[i] = dt.Rows[i].ItemArray;
        //        }

        //        dict.Add(dt.TableName, arr);
        //    }

        //    JavaScriptSerializer json = new JavaScriptSerializer();
        //    return json.Serialize(dict);
        //}


        //public static bool fileDelete(string folder, string file_name)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        string path = HttpContext.Current.Server.MapPath("~/" + folder + "/" + file_name);
        //        FileInfo file = new FileInfo(path);
        //        if (file.Exists)//check file exsit or not
        //        {
        //            file.Delete();
        //            flag = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return flag;
        //}

        //public static List<string> getUserIdAndPwd()
        //{
        //    List<string> lis = new List<string>();
        //    Random r = new Random();
        //    string uid = r.Next().ToString().Substring(0, 4);
        //    string pwd = RandomPassword.Generate(4, 6); // r.Next().ToString().Substring(0, 3);        
        //    lis.Add(uid);
        //    lis.Add(pwd);
        //    return lis;
        //}

        public static string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

        public static string GenerateRandomOTP(int iOTPLength)
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }


        string RandomNumbers(int Length)
        {
            Random Rand = new Random();
            StringBuilder SB = new StringBuilder();
            for (int i = 0; i < Length; i++)
                SB.Append(Rand.Next(0, 9));

            return SB.ToString();
        }


    }



}
   
