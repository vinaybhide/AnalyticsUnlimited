using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

//following gets all data for mf=53 from start to to date, if you dont give mf code then it will download data for all MF
//http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf=53&frmdt=01-Jul-2020&todt=25-Sep-2020
//http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf=53&frmdt=2020-07-01&todt=2020-09-25
//static string urlMFCompCodeHistoryURL = "http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf={0}&frmdt={1}&todt={2}";

namespace AnalyticsUnlimited
{
    public static class MFAPI
    {

        public static ListItem[] listFundHouseMaster = new[]
        {
            //DropDownSubCategories.Items.Clear();
            //DropDownSubCategories.Items.AddRange(Electronics);
            // OR 
            //DropDownSubCategories.Items.Add(new ListItem{ Value = "2", Text = "Home Audio"}); .......

            new ListItem{ Value = "-1" , Text = "--Select Mutual Fund --"},
                //new ListItem{ Value = "all" , Text = "All </ option >

                new ListItem{ Value ="39" , Text = "ABN AMRO Mutual Fund"},

                new ListItem{ Value = "3" , Text = "Aditya Birla Sun Life Mutual Fund"},
                new ListItem{ Value = "50" , Text = "AEGON Mutual Fund"},
                new ListItem{ Value = "1" , Text = "Alliance Capital Mutual Fund"},
                new ListItem{ Value = "53" , Text = "Axis Mutual Fund"},
                new ListItem{ Value = "4" , Text = "Baroda Mutual Fund"},
                new ListItem{ Value = "36" , Text = "Benchmark Mutual Fund"},
                new ListItem{ Value = "59" , Text = "BNP Paribas Mutual Fund"},
                new ListItem{ Value = "46" , Text = "BOI AXA Mutual Fund"},
                new ListItem{ Value = "32" , Text = "Canara Robeco Mutual Fund"},
                new ListItem{ Value = "60" , Text = "Daiwa Mutual Fund"},
                new ListItem{ Value = "31" , Text = "DBS Chola Mutual Fund"},
                new ListItem{ Value = "38" , Text = "Deutsche Mutual Fund"},
                new ListItem{ Value = "6" , Text = "DSP Mutual Fund"},
                new ListItem{ Value = "47" , Text = "Edelweiss Mutual Fund"},
                new ListItem{ Value = "54" , Text = "Essel Mutual Fund"},
                new ListItem{ Value = "40" , Text = "Fidelity Mutual Fund"},
                new ListItem{ Value = "51" , Text = "Fortis Mutual Fund"},
                new ListItem{ Value = "27" , Text = "Franklin Templeton Mutual Fund"},
                new ListItem{ Value = "8" , Text = "GIC Mutual Fund"},
                new ListItem{ Value = "49" , Text = "Goldman Sachs Mutual Fund"},
                new ListItem{ Value = "9" , Text = "HDFC Mutual Fund"},
                new ListItem{ Value = "37" , Text = "HSBC Mutual Fund"},
                new ListItem{ Value = "20" , Text = "ICICI Prudential Mutual Fund"},
                new ListItem{ Value = "57" , Text = "IDBI Mutual Fund"},
                new ListItem{ Value = "48" , Text = "IDFC Mutual Fund"},
                new ListItem{ Value = "68" , Text = "IIFCL Mutual Fund (IDF)"},
                new ListItem{ Value = "62" , Text = "IIFL Mutual Fund"},
                ////new ListItem{ Value = "11" , Text = "IL & amp; F S Mutual Fund"},
                //new ListItem{ Value = "11" , Text = "IL&F S Mutual Fund"},
                ////new ListItem{ Value = "65" , Text = "IL & amp; FS Mutual Fund(IDF)"},
                //new ListItem{ Value = "65" , Text = "IL&FS Mutual Fund(IDF)"},
                new ListItem{ Value = "63" , Text = "Indiabulls Mutual Fund"},
                new ListItem{ Value = "14" , Text = "ING Mutual Fund"},
                new ListItem{ Value = "42" , Text = "Invesco Mutual Fund"},
                new ListItem{ Value = "70" , Text = "ITI Mutual Fund"},
                new ListItem{ Value = "16" , Text = "JM Financial Mutual Fund"},
                    new ListItem{ Value = "43" , Text = "JPMorgan Mutual Fund"},
                new ListItem{ Value = "17" , Text = "Kotak Mahindra Mutual Fund"},
                    //new ListItem{ Value = "56" , Text = "L & amp; T Mutual Fund"},
                    new ListItem{ Value = "56" , Text = "L&T Mutual Fund"},
                new ListItem{ Value = "18" , Text = "LIC Mutual Fund"},
                new ListItem{ Value = "69" , Text = "Mahindra Manulife Mutual Fund"},
                    new ListItem{ Value = "45" , Text = "Mirae Asset Mutual Fund"},
                    new ListItem{ Value = "19" , Text = "Morgan Stanley Mutual Fund"},
                    new ListItem{ Value = "55" , Text = "Motilal Oswal Mutual Fund"},
                    new ListItem{ Value = "21" , Text = "Nippon India Mutual Fund"},
                    new ListItem{ Value = "58" , Text = "PGIM India Mutual Fund"},
                    new ListItem{ Value = "44" , Text = "PineBridge Mutual Fund"},
                new ListItem{ Value = "34" , Text = "PNB Mutual Fund"},
                new ListItem{ Value = "64" , Text = "PPFAS Mutual Fund"},
                new ListItem{ Value = "10" , Text = "Principal Mutual Fund"},
                new ListItem{ Value = "13" , Text = "quant Mutual Fund"},
                new ListItem{ Value = "41" , Text = "Quantum Mutual Fund"},
                new ListItem{ Value = "35" , Text = "Sahara Mutual Fund"},
                new ListItem{ Value = "22" , Text = "SBI Mutual Fund"},
                new ListItem{ Value = "52" , Text = "Shinsei Mutual Fund"},
                new ListItem{ Value = "67" , Text = "Shriram Mutual Fund"},
                new ListItem{ Value = "66" , Text = "SREI Mutual Fund (IDF)"},
                new ListItem{ Value = "2" , Text = "Standard Chartered Mutual Fund"},
                    //new ListItem{ Value = "24" , Text = "SUN F&C Mutual Fund"},
                new ListItem{ Value = "33" , Text = "Sundaram Mutual Fund"},
                new ListItem{ Value = "25" , Text = "Tata Mutual Fund"},
                new ListItem{ Value = "26" , Text = "Taurus Mutual Fund"},
                new ListItem{ Value = "72" , Text = "Trust Mutual Fund"},
                new ListItem{ Value = "61" , Text = "Union Mutual Fund"},
                new ListItem{ Value = "28" , Text = "UTI Mutual Fund"},
                new ListItem{ Value = "71" , Text = "YES Mutual Fund"},
                new ListItem{ Value = "29" , Text = "Zurich India Mutual Fund"},
                };


        static string mfMasterFile = "MF_MASTER_CURRENT_NAV.txt";



        //Following URL will fetch latest NAV for ALL MF
        //webservice_url = "https://www.amfiindia.com/spages/NAVAll.txt?t=27092020012930"; //string.Format(mfCurrentNAVALL_URL);
        //webservice_url = "https://www.amfiindia.com/spages/NAVAll.txt?t=27-09-2020"; //string.Format(mfCurrentNAVALL_URL);
        //static string urlMF_MASTER_CURRENT = "https://www.amfiindia.com/spages/NAVAll.txt?t={0}";
        static string urlMF_MASTER_CURRENT = "https://www.amfiindia.com/spages/NAVAll.txt";

        //Use following URL to get specific date NAV for ALL MF. The format is same as urlMF_MASTER_CURRENT
        //Output is:
        //Scheme Code;Scheme Name;ISIN Div Payout/ISIN Growth;ISIN Div Reinvestment;Net Asset Value;Repurchase Price;Sale Price;Date
        //http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?frmdt=01-Jan-2020
        static string urlMF_NAV_FOR_DATE = "http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?frmdt={0}";

        //Use following URL to get NAV history between from dt & to dt for specific MF code. 
        //Output is :
        //Scheme Code;Scheme Name;ISIN Div Payout/ISIN Growth;ISIN Div Reinvestment;Net Asset Value;Repurchase Price;Sale Price;Date
        //http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf=27&frmdt=27-Sep-2020&todt=05-Oct-2020
        static string urlMF_NAV_HISTORY_FROM_TO = "http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf={0}&frmdt={1}&todt={2}";
        static string urlMF_NAV_HISTORY_FROM = "http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf={0}&frmdt={1}";

        //
        //http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?frmdt=27-Sep-2020&todt=05-Oct-2020&mf=3&scm=119551
        static string urlSCHEME_NAV_HISTORY_FROM_TO = "http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf={0}&frmdt={1}&todt={2}";
        static string urlSCHEME_NAV_HISTORY_FROM = "http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf={0}&frmdt={1}";

        static bool isFileWriteDateEqualsToday(string filename)
        {
            bool breturn = false;
            try
            {
                if (File.Exists(filename))
                {
                    if (filename.Contains("MF_MASTER_CURRENT_NAV.txt"))
                    {
                        DateTime dtFileWriteTime = File.GetLastWriteTime(filename);
                        DateTime dtToday = DateTime.Today;

                        if (dtFileWriteTime.Date == DateTime.Today)
                        {
                            breturn = true;
                        }
                        else
                        {
                            breturn = false;
                        }
                    }
                    else
                    {
                        breturn = true;
                    }
                }
                else
                {
                    breturn = false;
                }
            }
            catch (Exception ex)
            {
                breturn = false;
            }
            return breturn;
        }

        //Function to get ALL MF;s and latest available NAV
        //"https://www.amfiindia.com/spages/NAVAll.txt"
        //It will check if file was fetched today, if yes then it will not fetch it from AMFIINDIA.COM, but return DataTable from the existing file
        //Else it will fetch file, save it locally and then return DataTable
        //Format of the MF Master file & return table is as below
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable loadMFMasterWithCurrentNAV(string folderPath)
        {
            DataTable resultDataTable = null;
            StringBuilder returnString = new StringBuilder("MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE");
            string webservice_url;
            Uri url;
            WebResponse wr;
            Stream receiveStream = null;
            StreamReader reader = null;
            string[] fields;
            string record;
            DataRow r;
            string mfType = "", tmp1 = "";
            string mfCompName = "";
            double nav;
            StringBuilder filename = new StringBuilder(folderPath + mfMasterFile); // "MF_MASTER_CURRENT_NAV.txt");
            try
            {
                if (isFileWriteDateEqualsToday(filename.ToString()) == false)
                {
                    //webservice_url = string.Format(mfCurrentNAVALL_URL);
                    //using (var webClient = new System.Net.WebClient())
                    //{
                    //    record = webClient.DownloadString("https://www.amfiindia.com/spages/NAVAll.txt?t=27092020012930");
                    //    webClient.Dispose();
                    //}

                    //https://www.amfiindia.com/spages/NAVAll.txt;


                    //webservice_url = string.Format(urlMF_MASTER_CURRENT, DateTime.Today.Date.ToShortDateString());
                    webservice_url = urlMF_MASTER_CURRENT;
                    url = new Uri(webservice_url);
                    var webRequest = WebRequest.Create(url);
                    webRequest.Method = WebRequestMethods.File.DownloadFile;
                    //webRequest.ContentType = "application/json";
                    wr = webRequest.GetResponseAsync().Result;
                    receiveStream = wr.GetResponseStream();
                    reader = new StreamReader(receiveStream);
                    if (reader != null)
                    {
                        //get first line where fields are mentioned
                        record = reader.ReadLine();
                        fields = record.Split(';');
                        resultDataTable = new DataTable();
                        resultDataTable.Columns.Add("MF_TYPE", typeof(string));
                        resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                        resultDataTable.Columns.Add("SCHEME_CODE", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Payout_ISIN_Growth", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Reinvestment", typeof(string));
                        resultDataTable.Columns.Add("SCHEME_NAME", typeof(string));
                        resultDataTable.Columns.Add("NET_ASSET_VALUE", typeof(decimal));
                        resultDataTable.Columns.Add("DATE", typeof(DateTime));

                        //Now we have table with following fields
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date

                        //Now read each line and fill the data in table. We have to skip lines which do not have ';' and hence fields will be empty
                        while (!reader.EndOfStream)
                        {
                            record = reader.ReadLine();

                            record = record.Trim();

                            if (record.Length == 0)
                            {
                                continue;
                            }
                            else if (record.Contains(";") == false) //case of either MF type or MF House
                            {
                                tmp1 = record;
                                //lets read next few lines till we find a line with either ; or no ;
                                //if we find a line with ; then it's continuation of same MF Type but
                                while (!reader.EndOfStream)
                                {
                                    record = reader.ReadLine();
                                    record = record.Trim();

                                    if (record.Length == 0)
                                    {
                                        continue;
                                    }
                                    else if (record.Contains(";") == false)
                                    {
                                        //we found a MF company name
                                        mfType = tmp1;
                                        mfCompName = record;
                                        tmp1 = record;
                                    }
                                    else if (record.Contains(";") == true)
                                    {
                                        //we continue with same MF type
                                        mfCompName = tmp1;
                                        break;
                                    }
                                }
                            }

                            fields = record.Split(';');

                            //Check if we have values for - Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                            if (fields.Length == 6)
                            {
                                try
                                {
                                    nav = System.Convert.ToDouble(fields[4]);
                                }
                                catch (Exception)
                                {

                                    nav = 0.00;
                                }

                                //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                                returnString.AppendLine();
                                returnString.Append(mfType);
                                returnString.Append(";");
                                returnString.Append(mfCompName);
                                returnString.Append(";");
                                returnString.Append(fields[0]);
                                returnString.Append(";");
                                returnString.Append(fields[1]);
                                returnString.Append(";");
                                returnString.Append(fields[2]);
                                returnString.Append(";");
                                returnString.Append(fields[3]);
                                returnString.Append(";");

                                returnString.Append(string.Format("{0:0.0000}", nav));
                                //returnString.Append(fields[4]);

                                returnString.Append(";");
                                returnString.Append(System.Convert.ToDateTime(fields[5]).ToString("yyyy-MM-dd"));
                                resultDataTable.Rows.Add(new object[] {
                                                                    mfType,
                                                                    mfCompName,
                                                                    fields[0],
                                                                    fields[1],
                                                                    fields[2],
                                                                    fields[3],
                                                                    System.Convert.ToDouble(string.Format("{0:0.0000}", nav)),
                                                                    //fields[4],
                                                                    System.Convert.ToDateTime(fields[5]).ToString("yyyy-MM-dd")
                                                                });
                            }
                        }
                        File.WriteAllText(filename.ToString(), returnString.ToString());
                    }
                }
                else
                {
                    if (File.Exists(filename.ToString()))
                        reader = new StreamReader(filename.ToString());
                    if (reader != null)
                    {
                        record = reader.ReadLine();

                        fields = record.Split(';');
                        resultDataTable = new DataTable();
                        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                        //foreach (string fieldname in fields)
                        //{
                        //    resultDataTable.Columns.Add(fieldname, typeof(string));
                        //}
                        resultDataTable.Columns.Add("MF_TYPE", typeof(string));
                        resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                        resultDataTable.Columns.Add("SCHEME_CODE", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Payout_ISIN_Growth", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Reinvestment", typeof(string));
                        resultDataTable.Columns.Add("SCHEME_NAME", typeof(string));
                        resultDataTable.Columns.Add("NET_ASSET_VALUE", typeof(decimal));
                        resultDataTable.Columns.Add("DATE", typeof(DateTime));

                        while (!reader.EndOfStream)
                        {
                            record = reader.ReadLine();
                            fields = record.Split(';');

                            //r = resultDataTable.NewRow();
                            //r.ItemArray = fields;
                            //resultDataTable.Rows.Add(r);

                            //Fields in file
                            //0MF_TYPE;1MF_COMP_NAME;2SCHEME_CODE;3ISIN_Div_Payout_ISIN_Growth;4ISIN_Div_Reinvestment;5SCHEME_NAME;6NET_ASSET_VALUE;7DATE
                            resultDataTable.Rows.Add(new object[] {
                                                                    fields[0],
                                                                    fields[1],
                                                                    fields[2],
                                                                    fields[3],
                                                                    fields[4],
                                                                    fields[5],
                                                                    System.Convert.ToDouble(string.Format("{0:0.0000}", fields[6])),
                                                                    //fields[6],
                                                                    System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd")
                                                                });

                        }
                    }
                }
                if (reader != null)
                    reader.Close();
                if (receiveStream != null)
                    receiveStream.Close();
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }


        //This method will fetch MF data for specific date with following URL.
        //http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?frmdt=01-Jan-2020
        //The output is in different format than NAVALL for the current NAV
        //The out put of this URL is as below
        //Scheme Code;Scheme Name;ISIN Div Payout/ISIN Growth;ISIN Div Reinvestment;Net Asset Value;Repurchase Price;Sale Price;Date

        //output of the method is table in following format
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable getMFNAVForDate(string folderPath, string fetchDate)
        {
            DataTable resultDataTable = null;
            StringBuilder returnString = new StringBuilder("MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE");
            string webservice_url;
            Uri url;
            WebResponse wr;
            Stream receiveStream = null;
            StreamReader reader = null;
            string[] fields;
            string record;
            DataRow r;
            string mfType = "", tmp1 = "";
            string mfCompName = "";
            string dateFetch = System.Convert.ToDateTime(fetchDate).ToString("yyyy-MM-dd");

            StringBuilder filename = new StringBuilder(folderPath + dateFetch + ".txt"); // "MF_MASTER_CURRENT_NAV.txt");
            try
            {
                //if (isFileWriteDateEqualsToday(filename.ToString()) == false)
                if (File.Exists(filename.ToString()) == false)
                {
                    //webservice_url = string.Format(urlMF_MASTER_CURRENT, DateTime.Today.Date.ToShortDateString());
                    webservice_url = string.Format(urlMF_NAV_FOR_DATE, dateFetch);
                    url = new Uri(webservice_url);
                    var webRequest = WebRequest.Create(url);
                    webRequest.Method = WebRequestMethods.File.DownloadFile;
                    //webRequest.ContentType = "application/json";
                    wr = webRequest.GetResponseAsync().Result;
                    receiveStream = wr.GetResponseStream();
                    reader = new StreamReader(receiveStream);
                    if (reader != null)
                    {
                        //get first line where fields are mentioned
                        record = reader.ReadLine();
                        fields = record.Split(';');
                        resultDataTable = new DataTable();
                        resultDataTable.Columns.Add("MF_TYPE", typeof(string));
                        resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                        resultDataTable.Columns.Add("SCHEME_CODE", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Payout_ISIN_Growth", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Reinvestment", typeof(string));
                        resultDataTable.Columns.Add("SCHEME_NAME", typeof(string));
                        resultDataTable.Columns.Add("NET_ASSET_VALUE", typeof(decimal));
                        resultDataTable.Columns.Add("DATE", typeof(DateTime));

                        //Now we have table with following fields
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date

                        //Now read each line and fill the data in table. We have to skip lines which do not have ';' and hence fields will be empty
                        while (!reader.EndOfStream)
                        {
                            record = reader.ReadLine();

                            record = record.Trim();

                            if (record.Length == 0)
                            {
                                continue;
                            }
                            else if (record.Contains(";") == false) //case of either MF type or MF House
                            {
                                tmp1 = record;
                                //lets read next few lines till we find a line with either ; or no ;
                                //if we find a line with ; then it's continuation of same MF Type but
                                while (!reader.EndOfStream)
                                {
                                    record = reader.ReadLine();
                                    record = record.Trim();

                                    if (record.Length == 0)
                                    {
                                        continue;
                                    }
                                    else if (record.Contains(";") == false)
                                    {
                                        //we found a MF company name
                                        mfType = tmp1;
                                        mfCompName = record;
                                        tmp1 = record;
                                    }
                                    else if (record.Contains(";") == true)
                                    {
                                        //we continue with same MF type
                                        mfCompName = tmp1;
                                        break;
                                    }
                                }
                            }

                            fields = record.Split(';');
                            //Following fields are in a record for this URL
                            //Scheme Code;Scheme Name;ISIN Div Payout/ISIN Growth;ISIN Div Reinvestment;Net Asset Value;Repurchase Price;Sale Price;Date

                            //Check if we have values for - Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                            if (fields.Length >= 6)
                            {
                                //Our table is in following format
                                //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                                returnString.AppendLine();
                                returnString.Append(mfType);
                                returnString.Append(";");
                                returnString.Append(mfCompName);
                                returnString.Append(";");
                                returnString.Append(fields[0]); //Scheme Code
                                returnString.Append(";");
                                returnString.Append(fields[2]); //ISIN_Div_Payout_ISIN_Growth
                                returnString.Append(";");
                                returnString.Append(fields[3]); //ISIN_Div_Reinvestment
                                returnString.Append(";");
                                returnString.Append(fields[1]); //SCHEME_NAME
                                returnString.Append(";");

                                returnString.Append(string.Format("{0:0.0000}", System.Convert.ToDouble(fields[4])));
                                //returnString.Append(fields[4]); //NET_ASSET_VALUE

                                returnString.Append(";");
                                returnString.Append(System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd"));  //DATE
                                resultDataTable.Rows.Add(new object[] {
                                                                    mfType,
                                                                    mfCompName,
                                                                    fields[0],
                                                                    fields[2],
                                                                    fields[3],
                                                                    fields[1],
                                                                    string.Format("{0:0.0000}", System.Convert.ToDouble(fields[4])),
                                                                    //fields[4],
                                                                    System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd")
                                                                });
                            }
                        }
                        File.WriteAllText(filename.ToString(), returnString.ToString());
                    }
                }
                else
                {
                    //if (File.Exists(filename.ToString()))
                    reader = new StreamReader(filename.ToString());
                    if (reader != null)
                    {
                        record = reader.ReadLine();

                        fields = record.Split(';');
                        resultDataTable = new DataTable();
                        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                        //foreach (string fieldname in fields)
                        //{
                        //    resultDataTable.Columns.Add(fieldname, typeof(string));
                        //}
                        resultDataTable.Columns.Add("MF_TYPE", typeof(string));
                        resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                        resultDataTable.Columns.Add("SCHEME_CODE", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Payout_ISIN_Growth", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Reinvestment", typeof(string));
                        resultDataTable.Columns.Add("SCHEME_NAME", typeof(string));
                        resultDataTable.Columns.Add("NET_ASSET_VALUE", typeof(decimal));
                        resultDataTable.Columns.Add("DATE", typeof(DateTime));

                        while (!reader.EndOfStream)
                        {
                            record = reader.ReadLine();
                            fields = record.Split(';');

                            //r = resultDataTable.NewRow();
                            //r.ItemArray = fields;
                            //resultDataTable.Rows.Add(r);
                            //FIle is in following format
                            //0MF_TYPE;1MF_COMP_NAME;2SCHEME_CODE;3ISIN_Div_Payout_ISIN_Growth;4ISIN_Div_Reinvestment;5SCHEME_NAME;6NET_ASSET_VALUE;7DATE
                            resultDataTable.Rows.Add(new object[] {
                                                                    fields[0],
                                                                    fields[1],
                                                                    fields[2],
                                                                    fields[3],
                                                                    fields[4],
                                                                    fields[5],
                                                                    string.Format("{0:0.0000}", System.Convert.ToDouble(fields[6])),
                                                                    //fields[4],
                                                                    System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd")
                                                                });
                        }
                    }
                }
                if (reader != null)
                    reader.Close();
                if (receiveStream != null)
                    receiveStream.Close();
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }

        //This method will fetch MF NAV history data for specific MF Code between from date = fromDt & To date < to date
        //http://portal.amfiindia.com/DownloadNAVHistoryReport_Po.aspx?mf=27&frmdt=2020-09-01&todt=2020-09-04
        //The output is in different format than NAVALL for the current NAV
        //The out put of this URL is as below
        //Scheme Code;Scheme Name;ISIN Div Payout/ISIN Growth;ISIN Div Reinvestment;Net Asset Value;Repurchase Price;Sale Price;Date
        //output of the method is table in following format
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable getHistoryNAV(string folderPath, string mfCode, string fromdt, string todt = null)
        {
            DataTable resultDataTable = null;
            StringBuilder returnString = new StringBuilder("MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE");
            string webservice_url;
            Uri url;
            WebResponse wr;
            Stream receiveStream = null;
            StreamReader reader = null;
            string[] fields;
            string record;
            DataRow r;
            string mfType = "", tmp1 = "";
            string mfCompName = "";
            string dateFrom = System.Convert.ToDateTime(fromdt).ToString("yyyy-MM-dd");
            string dateTo = null;

            StringBuilder filename;
            if (todt != null)
            {
                dateTo = System.Convert.ToDateTime(todt).ToString("yyyy-MM-dd");
                filename = new StringBuilder(folderPath + mfCode + "_" + dateFrom + "_" + dateTo + ".txt");
                webservice_url = string.Format(urlMF_NAV_HISTORY_FROM_TO, mfCode, dateFrom, dateTo);
            }
            else
            {
                filename = new StringBuilder(folderPath + mfCode + "_" + dateFrom + ".txt");
                webservice_url = string.Format(urlMF_NAV_HISTORY_FROM, mfCode, dateFrom);
            }
            try
            {
                //if (isFileWriteDateEqualsToday(filename.ToString()) == false)
                if (File.Exists(filename.ToString()) == false)
                {
                    //webservice_url = string.Format(urlMF_NAV_HISTORY, mfCode, dateFrom, dateTo);
                    url = new Uri(webservice_url);
                    var webRequest = WebRequest.Create(url);
                    webRequest.Method = WebRequestMethods.File.DownloadFile;
                    //webRequest.ContentType = "application/json";
                    wr = webRequest.GetResponseAsync().Result;
                    receiveStream = wr.GetResponseStream();
                    reader = new StreamReader(receiveStream);
                    if (reader != null)
                    {
                        //get first line where fields are mentioned
                        record = reader.ReadLine();
                        if (record.Length <= 0)
                        {
                            throw new Exception("No records found.");
                        }
                        fields = record.Split(';');
                        resultDataTable = new DataTable();
                        resultDataTable.Columns.Add("MF_TYPE", typeof(string));
                        resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                        resultDataTable.Columns.Add("SCHEME_CODE", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Payout_ISIN_Growth", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Reinvestment", typeof(string));
                        resultDataTable.Columns.Add("SCHEME_NAME", typeof(string));
                        resultDataTable.Columns.Add("NET_ASSET_VALUE", typeof(decimal));
                        //resultDataTable.Columns.Add("DATE", typeof(string));
                        resultDataTable.Columns.Add("DATE", typeof(DateTime));

                        //Now we have table with following fields
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date

                        //Now read each line and fill the data in table. We have to skip lines which do not have ';' and hence fields will be empty
                        while (!reader.EndOfStream)
                        {
                            record = reader.ReadLine();

                            record = record.Trim();

                            if (record.Length == 0)
                            {
                                continue;
                            }
                            else if (record.Contains(";") == false) //case of either MF type or MF House
                            {
                                tmp1 = record;
                                //lets read next few lines till we find a line with either ; or no ;
                                //if we find a line with ; then it's continuation of same MF Type but
                                while (!reader.EndOfStream)
                                {
                                    record = reader.ReadLine();
                                    record = record.Trim();

                                    if (record.Length == 0)
                                    {
                                        continue;
                                    }
                                    else if (record.Contains(";") == false)
                                    {
                                        //we found a MF company name
                                        mfType = tmp1;
                                        mfCompName = record;
                                        tmp1 = record;
                                    }
                                    else if (record.Contains(";") == true)
                                    {
                                        //we continue with same MF type
                                        mfCompName = tmp1;
                                        break;
                                    }
                                }
                            }

                            fields = record.Split(';');
                            //Following fields are in a record for this URL
                            //Scheme Code;Scheme Name;ISIN Div Payout/ISIN Growth;ISIN Div Reinvestment;Net Asset Value;Repurchase Price;Sale Price;Date

                            //Check if we have values for - Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                            if (fields.Length >= 6)
                            {
                                //Our table is in following format
                                //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                                returnString.AppendLine();
                                returnString.Append(mfType);
                                returnString.Append(";");
                                returnString.Append(mfCompName);
                                returnString.Append(";");
                                returnString.Append(fields[0]); //Scheme Code
                                returnString.Append(";");
                                returnString.Append(fields[2]); //ISIN_Div_Payout_ISIN_Growth
                                returnString.Append(";");
                                returnString.Append(fields[3]); //ISIN_Div_Reinvestment
                                returnString.Append(";");
                                returnString.Append(fields[1]); //SCHEME_NAME
                                returnString.Append(";");

                                returnString.Append(string.Format("{0:0.0000}", System.Convert.ToDouble(fields[4])));
                                //returnString.Append(fields[4]); //NET_ASSET_VALUE

                                returnString.Append(";");
                                returnString.Append(System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd"));  //DATE
                                resultDataTable.Rows.Add(new object[] {
                                                                    mfType,
                                                                    mfCompName,
                                                                    fields[0],
                                                                    fields[2],
                                                                    fields[3],
                                                                    fields[1],
                                                                    System.Convert.ToDouble(string.Format("{0:0.0000}", System.Convert.ToDouble(fields[4]))),
                                                                    //fields[4],
                                                                    System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd")
                                                                });
                            }
                        }
                        File.WriteAllText(filename.ToString(), returnString.ToString());
                    }
                }
                else
                {
                    //if (File.Exists(filename.ToString()))
                    reader = new StreamReader(filename.ToString());
                    if (reader != null)
                    {
                        record = reader.ReadLine();

                        fields = record.Split(';');
                        resultDataTable = new DataTable();
                        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                        //foreach (string fieldname in fields)
                        //{
                        //    resultDataTable.Columns.Add(fieldname, typeof(string));
                        //}
                        resultDataTable.Columns.Add("MF_TYPE", typeof(string));
                        resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));
                        //Scheme Code;ISIN Div Payout/ ISIN Growth;ISIN Div Reinvestment;Scheme Name;Net Asset Value;Date
                        resultDataTable.Columns.Add("SCHEME_CODE", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Payout_ISIN_Growth", typeof(string));
                        resultDataTable.Columns.Add("ISIN_Div_Reinvestment", typeof(string));
                        resultDataTable.Columns.Add("SCHEME_NAME", typeof(string));
                        resultDataTable.Columns.Add("NET_ASSET_VALUE", typeof(decimal));
                        //resultDataTable.Columns.Add("DATE", typeof(string));
                        resultDataTable.Columns.Add("DATE", typeof(DateTime));

                        while (!reader.EndOfStream)
                        {
                            record = reader.ReadLine();
                            fields = record.Split(';');

                            //r = resultDataTable.NewRow();
                            //r.ItemArray = fields;
                            //resultDataTable.Rows.Add(r);
                            //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
                            resultDataTable.Rows.Add(new object[] {
                                                                    fields[0],
                                                                    fields[1],
                                                                    fields[2],
                                                                    fields[3],
                                                                    fields[4],
                                                                    fields[5],
                                                                    System.Convert.ToDouble(string.Format("{0:0.0000}", System.Convert.ToDouble(fields[6]))),
                                                                    //fields[4],
                                                                    System.Convert.ToDateTime(fields[7]).ToString("yyyy-MM-dd")
                                                                });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            if (reader != null)
                reader.Close();
            if (receiveStream != null)
                receiveStream.Close();
            return resultDataTable;
        }

        //Function that will search a string in SCHEME_NAME column and return all rows from MF Master table that matches search string
        //Format of the MF Master file & return table is as below
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable searchMFMaster(string folderPath, string searchString = null, bool bExactMatch = false,
                                            DataTable mfMasterTable = null, int retryDays = 0, string searchDate = null)
        {
            DataTable resultDataTable = null;
            DataTable localMFMaster = null;
            int retryCount = 0;
            string localSearchDate;
            try
            {
                if (mfMasterTable == null)
                {
                    //mfMasterTable = loadMFMasterWithCurrentNAV(folderPath);
                    localMFMaster = loadMFMasterWithCurrentNAV(folderPath);
                }
                else
                {
                    localMFMaster = mfMasterTable.Copy();
                }

                if ((searchString == null) || (searchString.Length == 0))
                {
                    resultDataTable = mfMasterTable.Copy();
                }
                else
                {
                    do
                    {
                        if (bExactMatch == false)
                        {
                            localMFMaster.DefaultView.RowFilter = "SCHEME_NAME like '%" + searchString + "%'";
                        }
                        else
                        {
                            localMFMaster.DefaultView.RowFilter = "SCHEME_NAME = '" + searchString + "'";
                        }
                        resultDataTable = localMFMaster.DefaultView.ToTable();
                        if ((resultDataTable != null) && (resultDataTable.Rows.Count > 0))
                        {
                            break;
                        }
                        if (retryDays == 0)
                        {
                            break;
                        }
                        if (searchDate == null)
                        {
                            break;
                        }
                        retryCount++;

                        localSearchDate = System.Convert.ToDateTime(searchDate).AddDays(retryCount).ToShortDateString();

                        localMFMaster = getMFNAVForDate(folderPath, localSearchDate);
                    } while ((retryCount <= retryDays) && (searchDate != null) && (System.Convert.ToDateTime(searchDate) <= DateTime.Today));
                }
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }

        //Function that will search a string in SCHEME_NAME column and return all rows from MF History table that matches search string
        //Format of the MF Master file & return table is as below
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable searchMFHistoryForSchemeName(string folderPath, string mfCode, string fromDate,
                                    string searchString = null, bool bExactMatch = false,
                                    DataTable mfHistoryTable = null, string toDate = null)
        {
            DataTable resultDataTable = null;
            DataTable localMFHistoryTable = null;
            string dateFrom = System.Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd");
            string dateTo = null;

            try
            {
                if (mfHistoryTable == null)
                {
                    //mfMasterTable = loadMFMasterWithCurrentNAV(folderPath);
                    if (toDate != null)
                    {
                        dateTo = System.Convert.ToDateTime(toDate).ToString("yyyy-MM-dd");
                    }
                    localMFHistoryTable = getHistoryNAV(folderPath, mfCode, dateFrom, todt: dateTo);
                }
                else
                {
                    localMFHistoryTable = mfHistoryTable.Copy();
                }

                if ((searchString == null) || (searchString.Length == 0))
                {
                    resultDataTable = mfHistoryTable.Copy();
                }
                else
                {
                    if (bExactMatch == false)
                    {
                        localMFHistoryTable.DefaultView.RowFilter = "SCHEME_NAME like '%" + searchString + "%'";
                    }
                    else
                    {
                        localMFHistoryTable.DefaultView.RowFilter = "SCHEME_NAME = '" + searchString + "'";
                    }
                    if (localMFHistoryTable.DefaultView.Count > 0)
                    {
                        resultDataTable = localMFHistoryTable.DefaultView.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }

        //Function that will search a string in MF_COMP_NAME column and return all rows from MF Master table that matches search string
        //Format of the MF Master file & return table is as below
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable getALLMFforFundHouse(string folderPath, string searchString = null, bool bExactMatch = false, DataTable mfMasterTable = null)
        {
            DataTable resultDataTable = null;
            try
            {
                if (mfMasterTable == null)
                {
                    mfMasterTable = loadMFMasterWithCurrentNAV(folderPath);
                }


                if ((searchString == null) || (searchString.Length == 0))
                {
                    resultDataTable = mfMasterTable.Copy();
                }
                else
                {
                    if (bExactMatch == false)
                    {
                        mfMasterTable.DefaultView.RowFilter = "MF_COMP_NAME like '%" + searchString + "%'";
                    }
                    else
                    {
                        mfMasterTable.DefaultView.RowFilter = "MF_COMP_NAME = '" + searchString + "'";
                    }
                    resultDataTable = mfMasterTable.DefaultView.ToTable();
                }
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }

        //Function that will either return ALL Fund Houses or search a string in MF_COMP_NAME column and return all rows from MF Master table that matches search string
        //Format of the MF Master file & return table is as below
        //MF_TYPE;MF_COMP_NAME;SCHEME_CODE;ISIN_Div_Payout_ISIN_Growth;ISIN_Div_Reinvestment;SCHEME_NAME;NET_ASSET_VALUE;DATE
        static public DataTable getFundHouses(string folderPath, string searchString = null, bool bExactMatch = false, DataTable mfMasterTable = null)
        {
            DataTable resultDataTable = null;
            try
            {
                if (mfMasterTable == null)
                {
                    mfMasterTable = loadMFMasterWithCurrentNAV(folderPath);
                }

                if ((searchString == null) || (searchString.Length == 0))
                {
                    //Get all unique MF_COMP_NAME/Fund House rows
                    resultDataTable = mfMasterTable.DefaultView.ToTable(true, "MF_COMP_NAME");
                }
                else
                {
                    if (bExactMatch == false)
                    {
                        mfMasterTable.DefaultView.RowFilter = "MF_COMP_NAME like '%" + searchString + "%'";
                    }
                    else
                    {
                        mfMasterTable.DefaultView.RowFilter = "MF_COMP_NAME = '" + searchString + "'";
                    }
                    resultDataTable = mfMasterTable.DefaultView.ToTable();
                }
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }

        static public DataTable getFundHouseMaster()
        {
            DataTable resultDataTable = null;

            try
            {
                resultDataTable = new DataTable();
                resultDataTable.Columns.Add("MF_CODE", typeof(string));
                resultDataTable.Columns.Add("MF_COMP_NAME", typeof(string));

                foreach (ListItem item in listFundHouseMaster)
                {
                    resultDataTable.Rows.Add(new object[] {
                                                            item.Value,
                                                            item.Text
                                                            });
                }
            }
            catch (Exception ex)
            {
                if (resultDataTable != null)
                {
                    resultDataTable.Clear();
                    resultDataTable.Dispose();
                }
                resultDataTable = null;
            }
            return resultDataTable;
        }

        static public string getMFCodefromFundHouseMaster(string mfCompName)
        {
            string mfCode = null;

            try
            {
                foreach (ListItem item in listFundHouseMaster)
                {
                    if (item.Text.Equals(mfCompName))
                    {
                        mfCode = item.Value;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                mfCode = null;
            }
            return mfCode;
        }

        static public string getMFCompNamefromFundHouseMaster(string mfCodeName)
        {
            string mfCompName = null;

            try
            {
                foreach (ListItem item in listFundHouseMaster)
                {
                    if (item.Value.Equals(mfCodeName))
                    {
                        mfCompName = item.Text;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                mfCompName = null;
            }
            return mfCompName;
        }
    }
}
