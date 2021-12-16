using PortalJustNotificationManager.Model;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PortalJustNotificationManager.API
{
   class XmlToCaseMapper
   {
      internal Dosar MapToCaseFile(XmlDocument caseFileXml)
      {
         string testData = @"<Dosar>
					<parti>
						<DosarParte>
							<nume>HURMUZ IOAN</nume>
							<calitateParte>Recurent Reclamant</calitateParte>
						</DosarParte>
						<DosarParte>
							<nume>DIRECŢIA GENERALĂ IMPOZITE ŞI TAXE LOCALE SECTOR 3 PRIN PRIMAR</nume>
							<calitateParte>Intimat Pârât</calitateParte>
						</DosarParte>
					</parti>
					<sedinte>
						<DosarSedinta>
							<complet>8-Camera de consiliu 6 recurs</complet>
							<data>2019-12-10T00:00:00</data>
							<ora>09:00</ora>
							<solutie>Încheiere</solutie>
							<solutieSumar>Respinge cererea de reexaminare a modului de stabilire a taxei judiciare de timbru ca neîntemeiată.
Definitivă.
Pronunţată azi, 10.12.2019, prin punerea soluţiei la dispoziţia părţilor prin mijlocirea grefei instanţei.


</solutieSumar>
							<dataPronuntare>2019-12-10T00:00:00</dataPronuntare>
							<documentSedinta>Incheierefinalacameraconsiliu</documentSedinta>
							<numarDocument />
							<dataDocument>2019-12-10T00:00:00</dataDocument>
						</DosarSedinta>
						<DosarSedinta>
							<complet>8-Camera de consiliu 6 recurs</complet>
							<data>2019-12-05T00:00:00</data>
							<ora>09:00</ora>
							<solutie>Amână pronunţarea</solutie>
							<solutieSumar>Amână pronunţarea la 10.12.2019.
Pronunţarea se va face prin punerea soluţiei la dispoziţia părţilor prin mijlocirea grefei instanţei. 
</solutieSumar>
							<dataPronuntare>2019-12-05T00:00:00</dataPronuntare>
							<documentSedinta>incheiereAmanareinitialaapronuntarii</documentSedinta>
							<numarDocument />
							<dataDocument>2019-12-05T00:00:00</dataDocument>
						</DosarSedinta>
					</sedinte>
					<numar>1904/3/2018/a1</numar>
					<numarVechi />
					<data>2019-12-02T00:00:00</data>
					<institutie>CurteadeApelBUCURESTI</institutie>
					<departament>Secţia a VIII-a contencios administrativ şi fiscal</departament>
					<categorieCaz>Contenciosadministrativsifiscal</categorieCaz>
					<stadiuProcesual>Recurs</stadiuProcesual>
					<obiect>reexaminare taxe de timbru</obiect>
					<dataModificare>2019-12-17T23:07:20.007</dataModificare>
					<categorieCazNume>Contencios administrativ şi fiscal</categorieCazNume>
					<stadiuProcesualNume>Recurs</stadiuProcesualNume>
				</Dosar>";

         XmlSerializer serializer = new XmlSerializer(typeof(Dosar));
         Dosar result;
         using (TextReader reader = new StringReader(testData))
         {
            result = (Dosar)serializer.Deserialize(reader);
         }

         return result;
      }
   }
}
