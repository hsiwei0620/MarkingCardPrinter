using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace MarkingCardPrinter
{
    /// <summary>
    /// printer 的摘要描述
    /// </summary>
    public class printer : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string message = string.Empty;
            string gtjson = string.Empty;
            bool result = false;

            using (var reader = new StreamReader(context.Request.InputStream))
            {
                gtjson = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(gtjson))
            {
                MesObject Introduction = JsonConvert.DeserializeObject<MesObject>(gtjson);
                result = true;
                message += Introduction.Name + ":" + Introduction.Subject;

                //var objList = new JavaScriptSerializer().Deserialize<dynamic>(gtjson);
                //foreach (MesObject p in objList)
                //{
                //    result = true;
                //    message += p.Name + ":" + p.Subject;
                //}


            }



            this.SendResponse(context, result, message);


            //test1
            //string lot_id = context.Request.QueryString["lot_id"];
            //string ope_id = context.Request.QueryString["ope_id"];
            //print test
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Lot ID: " + lot_id);

        }
        public class MesObject
        {
            public String Name { set; get; }
            public String Subject { set; get; }
        }

        private void SendResponse(HttpContext context, bool result, string message)
        {
            context.Response.Write(new JavaScriptSerializer().Serialize(
                new
                {
                    Result = result,
                    Message = message
                }));

            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}