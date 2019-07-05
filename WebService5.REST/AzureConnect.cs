using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebService5.REST
{
    class JTAzureTableRESTCommands
    {
        /// <summary>
        /// Requests JSON Data from Azure Table  using Shared Access Signature
        /// </summary>
        /// <param name="storageAccount"> Azure Storage Account</param>
        /// <param name="sharedaccesssig">Shared Access Signature from the Azure management portal. Good for 8 hours at a time by default</param>
        /// <param name="resourcePath">name of an existing Azure Table in this storage account</param>
        /// <param name="jsonData">[out] JSON object representing the new entity </param>
        /// <returns>Web Response code</returns>
        /// 
        public static int ChangeEntity(string verb, string storageAccount, string sharedaccesssig, string resourcePath, string jsonData, string partitionKey, string rowKey)
        {
            string oDataQuery = "(PartitionKey='" + partitionKey + "',RowKey='" + rowKey + "')";
            if (partitionKey == string.Empty && rowKey == string.Empty) { oDataQuery = string.Empty; }
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + oDataQuery + sharedaccesssig;
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = verb;
            request.ContentType = "application/json";
            if (verb != "POST") { request.Headers.Add("If-Match", "*"); }
            request.ContentLength = jsonData.Length;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            if (verb == "MERGE" || verb == "PUT" || verb == "POST")
            {
                // Write Entity's JSON data into the request body
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }

        public static int GetAllEntities(string storageAccount, string sharedaccesssig, string resourcePath, out string jsonData)
        {
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + sharedaccesssig;
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }

        public static int GetAllTables(string storageAccount, string sharedaccesssig, string resourcePath, out string jsonData)
        {
            string uri = @"https://" + storageAccount + ".table.core.windows.net/Tables" + resourcePath + sharedaccesssig;
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }
        public static int CreateTable(string verb, string storageAccount, string sharedaccesssig, string resourcePath, string jsonData, string partitionKey, string rowKey)
        {
            string oDataQuery = "";
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + oDataQuery + sharedaccesssig;
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = verb;
            request.ContentType = "application/json";
            if (verb != "POST") { request.Headers.Add("If-Match", "*"); }
            request.ContentLength = jsonData.Length;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            if (verb == "MERGE" || verb == "PUT" || verb == "POST")
            {
                // Write Entity's JSON data into the request body
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }
        public static int ChangeTable(string verb, string storageAccount, string sharedaccesssig, string resourcePath, string jsonData, string tablename)
        {
            string oDataQuery = "('" + tablename + "')";
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + oDataQuery + sharedaccesssig;
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = verb;
            request.ContentType = "application/json";
            //if (verb != "POST") { request.Headers.Add("If-Match", "*"); }
            request.ContentLength = jsonData.Length;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            if (verb == "MERGE" || verb == "PUT" || verb == "POST")
            {
                // Write Entity's JSON data into the request body
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(jsonData);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }

        public static int GetEntityProperty(string verb, string storageAccount, string sharedaccesssig, string resourcePath, out string jsonData, string partitionKey, string rowKey)
        {
            string oDataQuery = "(PartitionKey='" + partitionKey + "',RowKey='" + rowKey + "')";
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + oDataQuery + sharedaccesssig;
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = verb;
            request.ContentType = "application/json";
            //if (verb != "POST") { request.Headers.Add("If-Match", "*"); }
            request.ContentLength = 0;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }

        public static int GetLBProperty(string verb, string storageAccount, string sharedaccesssig, string resourcePath, out string jsonData, string partitionKey)
        {
            string oDataQuery = "?$filter=PartitionKey%20eq%20'" + partitionKey + "'";
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + oDataQuery + sharedaccesssig.Replace("?","&");
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = verb;
            request.ContentType = "application/json";
            //if (verb != "POST") { request.Headers.Add("If-Match", "*"); }
            request.ContentLength = 0;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }

        public static int GetRelicsFromAzure(string verb, string storageAccount, string sharedaccesssig, string resourcePath, out string jsonData, string partitionKey)
        {

            string oDataQuery = "?$filter=PartitionKey%20eq%20'" + partitionKey + "'";
            string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + oDataQuery + sharedaccesssig.Replace("?", "&");
            DateTime now = DateTime.UtcNow.AddSeconds(-5);

            // Web request 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = verb;
            request.ContentType = "application/json";
            //if (verb != "POST") { request.Headers.Add("If-Match", "*"); }
            request.ContentLength = 0;
            request.Accept = "application/json;odata=nometadata";
            request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
            request.Headers.Add("x-ms-version", "2015-04-05");
            request.Headers.Add("Accept-Charset", "UTF-8");
            request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
            request.Headers.Add("DataServiceVersion", "1.0;NetFx");

            // Execute the request
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        jsonData = r.ReadToEnd();
                        return (int)response.StatusCode;
                    }
                }
            }
            catch (WebException ex)
            {
                // get the message from the exception response
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                {
                    jsonData = sr.ReadToEnd();
                    // Log res if required
                }

                return (int)ex.Status;
            }
        }

        //public static int InsertEntity(string storageAccount, string sharedaccesssig, string resourcePath, string jsonData)
        //{
        //    string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + sharedaccesssig;
        //    DateTime now = DateTime.UtcNow.AddSeconds(-5);

        //    // Web request 
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
        //    request.Method = "POST";
        //    request.ContentType = "application/json";
        //    request.ContentLength = jsonData.Length;
        //    request.Accept = "application/json;odata=nometadata";
        //    request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
        //    request.Headers.Add("x-ms-version", "2015-04-05");
        //    request.Headers.Add("Accept-Charset", "UTF-8");
        //    request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
        //    request.Headers.Add("DataServiceVersion", "1.0;NetFx");

        //    // Write Entity's JSON data into the request body
        //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //    {
        //        streamWriter.Write(jsonData);
        //        streamWriter.Flush();
        //        streamWriter.Close();
        //    }

        //    // Execute the request
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
        //            {
        //                jsonData = r.ReadToEnd();
        //                return (int)response.StatusCode;
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // get the message from the exception response
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
        //        {
        //            jsonData = sr.ReadToEnd();
        //            // Log res if required
        //        }

        //        return (int)ex.Status;
        //    }
        //}

        //public static int UpdateEntity(string storageAccount, string sharedaccesssig, string resourcePath, string jsonData, string partitionKey, string rowKey)
        //{
        //    string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + "(PartitionKey='" + partitionKey + "',RowKey='" + rowKey + "')" + sharedaccesssig;
        //    DateTime now = DateTime.UtcNow.AddSeconds(-5);

        //    // Web request 
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
        //    request.Method = "PUT";
        //    request.ContentType = "application/json";
        //    request.ContentLength = jsonData.Length;
        //    request.Headers.Add("If-Match", "*");
        //    request.Accept = "application/json;odata=nometadata";
        //    request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
        //    request.Headers.Add("x-ms-version", "2015-04-05");
        //    request.Headers.Add("Accept-Charset", "UTF-8");
        //    request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
        //    request.Headers.Add("DataServiceVersion", "1.0;NetFx");

        //    // Write Entity's JSON data into the request body
        //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //    {
        //        streamWriter.Write(jsonData);
        //        streamWriter.Flush();
        //        streamWriter.Close();
        //    }

        //    // Execute the request
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
        //            {
        //                jsonData = r.ReadToEnd();
        //                return (int)response.StatusCode;
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // get the message from the exception response
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
        //        {
        //            jsonData = sr.ReadToEnd();
        //            // Log res if required
        //        }

        //        return (int)ex.Status;
        //    }
        //}

        //public static int DeleteEntity(string storageAccount, string sharedaccesssig, string resourcePath, string jsonData, string partitionKey, string rowKey)
        //{
        //    string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + "(PartitionKey='" + partitionKey + "',RowKey='" + rowKey + "')" + sharedaccesssig;
        //    DateTime now = DateTime.UtcNow.AddSeconds(-5);

        //    // Web request 
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
        //    request.Method = "DELETE";
        //    request.ContentType = "application/json";
        //    request.ContentLength = 0;
        //    request.Headers.Add("If-Match", "*");
        //    request.Accept = "application/json;odata=nometadata";
        //    request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
        //    request.Headers.Add("x-ms-version", "2015-04-05");
        //    request.Headers.Add("Accept-Charset", "UTF-8");
        //    request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
        //    request.Headers.Add("DataServiceVersion", "1.0;NetFx");

        //    // Execute the request
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
        //            {
        //                jsonData = r.ReadToEnd();
        //                return (int)response.StatusCode;
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // get the message from the exception response
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
        //        {
        //            jsonData = sr.ReadToEnd();
        //            // Log res if required
        //        }

        //        return (int)ex.Status;
        //    }
        //}

        //public static int MergeEntity(string storageAccount, string sharedaccesssig, string resourcePath, string jsonData, string partitionKey, string rowKey)
        //{
        //    string uri = @"https://" + storageAccount + ".table.core.windows.net/" + resourcePath + "(PartitionKey='" + partitionKey + "',RowKey='" + rowKey + "')" + sharedaccesssig;
        //    DateTime now = DateTime.UtcNow.AddSeconds(-5);

        //    // Web request 
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
        //    request.Method = "MERGE";
        //    request.ContentType = "application/json";
        //    request.ContentLength = jsonData.Length;
        //    request.Headers.Add("If-Match", "*");
        //    request.Accept = "application/json;odata=nometadata";
        //    request.Headers.Add("x-ms-date", now.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
        //    request.Headers.Add("x-ms-version", "2015-04-05");
        //    request.Headers.Add("Accept-Charset", "UTF-8");
        //    request.Headers.Add("MaxDataServiceVersion", "3.0;NetFx");
        //    request.Headers.Add("DataServiceVersion", "1.0;NetFx");

        //    // Write Entity's JSON data into the request body
        //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        //    {
        //        streamWriter.Write(jsonData);
        //        streamWriter.Flush();
        //        streamWriter.Close();
        //    }

        //    // Execute the request
        //    try
        //    {
        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (System.IO.StreamReader r = new System.IO.StreamReader(response.GetResponseStream()))
        //            {
        //                jsonData = r.ReadToEnd();
        //                return (int)response.StatusCode;
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // get the message from the exception response
        //        using (System.IO.StreamReader sr = new System.IO.StreamReader(ex.Response.GetResponseStream()))
        //        {
        //            jsonData = sr.ReadToEnd();
        //            // Log res if required
        //        }

        //        return (int)ex.Status;
        //    }
        //}
    }
}
