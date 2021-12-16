using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PortalJustNotificationManager.API
{
   class PortalJustHttpClient
   {
      private HttpClient httpClient;

      public PortalJustHttpClient()
      {
         httpClient = new HttpClient();
         httpClient.DefaultRequestHeaders.Add("Host", "portalquery.just.ro");
      }

      internal async Task<XmlDocument> FindCaseFile(string caseFileNumber)
      {
         using (var response = await httpClient.PostAsync("http://portalquery.just.ro/query.asmx", this.ConstructSoapRequest(caseFileNumber)))
         {
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               return await ParseHttpResponse(response);
            }
            else
            {
               throw new Exception("Server response error");
            }
         }
      }

      private StringContent ConstructSoapRequest(string caseFileNumber)
      {
         string soapEnvelope = String.Format(
            @"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
              <soap12:Body>
                <CautareDosare xmlns=""portalquery.just.ro"">
                  <numarDosar>{0}</numarDosar>
                </CautareDosare>
              </soap12:Body>
            </soap12:Envelope>", caseFileNumber);

         return new StringContent(soapEnvelope, Encoding.UTF8, "application/soap+xml");
      }

      private async Task<XmlDocument> ParseHttpResponse(HttpResponseMessage response)
      {
         var responseContent = await response.Content.ReadAsStringAsync();
         int caseDefinitionStart = responseContent.IndexOf("<Dosar>");
         int caseDefinitionLength = responseContent.IndexOf("</Dosar>") - caseDefinitionStart + 8;
         responseContent = responseContent.Substring(caseDefinitionStart, caseDefinitionLength);

         XmlDocument responseXml = new XmlDocument();
         responseXml.LoadXml(responseContent);

         return responseXml;
      }
   }
}
