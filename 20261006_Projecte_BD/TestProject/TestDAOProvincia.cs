using DAO;
using IDAO;

namespace TestProject
{
    [TestClass]
    public sealed class TestDAOProvincia
    {
        [TestMethod]
        public void TestGetNumeroProvincies()
        {

            MySQLFactory.getUOW(uow =>
            {
                IDAOProvincia dao_prov = uow.DAOProvincies;
                int num = dao_prov.GetNumeroProvincies("");
                Assert.AreEqual(4, num);
            });

        }
    }
}
