using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using APforGUS.ServiceReference1;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using RestSharp;
using System.Xml.Linq;
using System.Net;
using Eco.Wcf.Server;

namespace APforGUS
{

    public class dane
    {

        public string Regon { get; set; }

        public string Nip { get; set; }

        public string Typ { get; set; }
        public string Nazwa { get; set; }
        public string Miejscowosc { get; set; }

    }

    class Program
    {
        public static string Login { get; set; } = "";
        public static string Podpis { get; set; } = "";
        public static string Token { get; set; } = "CK-846df571-0150-1000-84c8-289e43b5bf78";
        public static XElement pobierzOswiadczenie()
        {
            XElement xml = new XElement("Oświadczenie",
                    new XElement("Tresc", $"Logowanie Lekarza przez system zewnętrzny({Login} {Podpis})"),
                    new XElement("Data", DateTime.Now.ToString("yyyy-MM-dd")),
                    new XElement("Czas", DateTime.Now.ToString("hh:mm:ss")),
                    new XElement("Token", Token)
             );

            return xml;
        }
        //public static string ComunicationToGus(string nips)
        //{
        //    WSHttpBinding myBinding = new WSHttpBinding();
        //    myBinding.Security.Mode = SecurityMode.Transport;
        //    myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
        //    myBinding.MessageEncoding = WSMessageEncoding.Mtom;


        //    EndpointAddress ea = new EndpointAddress("http://193.105.143.152:8001/ws/zus.channel.gabinetoweV2:zla");
        //    ServiceReference1.pobierzOswiadczenieResponse oswiadczenieResponse = pobierzOswiadczenieResponse();


        //    //UslugaBIRzewnPublClient service = new UslugaBIRzewnPublClient(myBinding, ea);
        //    //service.Open();

        //    //string sid = service.Zaloguj("abcde12345abcde12345");

        //    //OperationContextScope scope = new OperationContextScope(service.InnerChannel);

        //    //HttpRequestMessageProperty reqProps = new HttpRequestMessageProperty();
        //    //reqProps.Headers.Add("sid", sid);

        //    //OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = reqProps;
        //    //ParametryWyszukiwania parametryWyszukiwania = new ParametryWyszukiwania();
        //    //parametryWyszukiwania.Nipy = nips;
        //    //return service.DaneSzukajPodmioty(parametryWyszukiwania);
        //}
        static void Main(string[] args)
        {
            //string www=pobierzOswiadczenie().ToString();

            WSHttpBinding myBinding = new WSHttpBinding();
            myBinding.Security.Mode = SecurityMode.Transport;
            myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            myBinding.Security.Message.NegotiateServiceCredential = false;
            myBinding.Security.Message.EstablishSecurityContext = false;
            ServicePointManager.SecurityProtocol= SecurityProtocolType.Tls12;

            myBinding.MessageEncoding = WSMessageEncoding.Mtom;
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;


            EndpointAddress adress = new EndpointAddress("https://193.105.143.152:8001/ws/zus.channel.gabinetoweV2:zla");


            ServiceReference1.zla_PortTypeClient zla_Port = new zla_PortTypeClient(myBinding, adress);
            var t = zla_Port.pobierzOswiadczenieAsync();
            
            Console.WriteLine(t.Result);
            //docTypeRef_Rezultat xxx = null;
            //zla_Port.zalogujPodpisem(www, MetodaUwierzytelnienia.ePuap, "", out xxx);

            //Console.WriteLine(xxx.OpisBledu);


            //string xml = ComunicationToGus("8992689516,5261040828");
            ////Console.WriteLine(xml);
            //XmlSerializer serializer = new XmlSerializer(typeof(List<dane>), new XmlRootAttribute("root"));
            //List<dane> result;

            //using (TextReader reader = new StringReader(xml))
            //{
            //    result = (List<dane>)serializer.Deserialize(reader);
            //}

            ////Console.WriteLine(result.Count);
            //Console.WriteLine("Moje dane: ");
            //foreach (dane item in result)
            //{
            //    Console.Write(item.Nip +" ") ;
            //    Console.Write(item.Regon + " ");
            //    Console.Write(item.Miejscowosc + " ");
            //    Console.Write(item.Nazwa + " ");
            //    Console.WriteLine(item.Typ);

            //}

            Console.ReadKey();

        }
    }
}
