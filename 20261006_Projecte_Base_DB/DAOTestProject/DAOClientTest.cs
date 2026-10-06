using DAO;
using _20251001_Controls_Senders.model;
using System.Collections.ObjectModel;

namespace DAOTestProject
{
    [TestClass]
    public sealed class DAOClientTest       
    {

        [TestMethod]
        public void TestInsertAndDelete()
        {


            MySQLFactory.getUOW(uow =>
            {
                // Obtenim la província amb ID = 1 (que correspon a Barcelona)
                Provincia p = uow.DAOProvincia.GetProvincia(1); // Barcelona

                // Creem un nou client amb dades fictícies:
                // - ID = 0 (encara no assignat, ja que s'ha d'inserir)
                // - CIF = "A11111111"
                // - Raó social = "nova empresa"
                // - EsActiva = false
                // - Tipus d’empresa = YONKI (valor d’un enum definit al sistema)
                // - Província = p (Barcelona)
                Client c = new Client(0, "A11111111", "nova empresa",
                    false, TipusEmpresa.YONKI, p);

                // Inserim el client a la base de dades i comprovem que la inserció ha estat correcta
                Assert.IsTrue(uow.DAOClients.InsertClient(c));

                // Recuperem el client acabat d’inserir utilitzant el seu ID (assignat automàticament)
                Client inserit = uow.DAOClients.GetClient(c.Id);

                // Comprovem que el client realment existeix a la base de dades
                Assert.IsNotNull(inserit);

                // Verifiquem que totes les dades guardades coincideixen amb les originals
                Assert.AreEqual(inserit.RaoSocial, c.RaoSocial);
                Assert.AreEqual(inserit.CIF1, c.CIF1);
                Assert.AreEqual(inserit.EsActiva, c.EsActiva);
                Assert.AreEqual(inserit.Foto, c.Foto);

                // Eliminem el client de la base de dades
                Assert.IsTrue(uow.DAOClients.DeleteClient(c.Id));

                // Finalment, comprovem que el client s’ha eliminat correctament
                Assert.IsNull(uow.DAOClients.GetClient(c.Id));

            });
        }

            [TestMethod]
        public void TestSelectAndUpdate()
        {
            MySQLFactory.getUOW(uow =>
            {
                // Obtenim el nombre total de clients de la base de dades (sense filtres)
                int num = uow.DAOClients.GetNumeroClients("", "");

                // Comprovem que el nombre de clients sigui 23
                Assert.AreEqual(23, num);

                // Recuperem tots els clients (sense filtres)
                ObservableCollection<Client> clients = uow.DAOClients.GetClients("", "");

                // Comprovem que la quantitat de clients obtinguts coincideix amb el total esperat
                Assert.AreEqual(num, clients.Count);

                // Recuperem els clients filtrant pel codi "4"
                clients = uow.DAOClients.GetClients("4", "");

                // Comprovem que només hi hagi un client amb aquest identificador
                Assert.AreEqual(1, clients.Count);

                // Verifiquem que el CIF del client sigui l’esperat
                Assert.AreEqual("D98765432", clients[0].CIF1);

                // Verifiquem que la raó social del client sigui la correcta
                String rs = "Distribucions REXOSTA Brava SL";
                Assert.AreEqual(rs, clients[0].RaoSocial);

                // Guardem el client en una variable per modificar-lo
                Client c = clients[0];

                // Modifiquem la seva raó social
                c.RaoSocial = "XXX";

                // Actualitzem el client modificat a la base de dades
                uow.DAOClients.UpdateClient(c);

                // Tornem a llegir el client per ID i comprovem que el canvi s’ha desat correctament
                c = uow.DAOClients.GetClient(4);
                Assert.AreEqual("XXX", c.RaoSocial);

                // Restablim la raó social original
                c.RaoSocial = rs;
                uow.DAOClients.UpdateClient(c);

                // Tornem a comprovar que la raó social ha tornat al seu valor inicial
                c = uow.DAOClients.GetClient(4);
                Assert.AreEqual(rs, c.RaoSocial);

                // Confirmem definitivament els canvis a la unitat de treball
                uow.Commit();

            }
            );
        }
    }
}
