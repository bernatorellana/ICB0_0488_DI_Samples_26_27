using _20251001_Controls_Senders.model;
using IDAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DAO
{
    public class DAOClient : IDAOClient
    {

        private readonly MyDBContext _context;


        public DAOClient(MyDBContext c)
        {
            _context = c;
        }



        public Client GetClient(int id)
        {
            ObservableCollection<Client> clients =
                GetClients("" + id, "");
            if (clients.Count == 0) return null;
            else return clients[0];
        }

        public int GetNumeroClients(string id_filtre, string rao_social_filtre)
        {

            var connexio = _context.Database.GetDbConnection();
            // Obrir la connexió a la BD
            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            // Crear una consulta SQL
            using (var consulta = connexio.CreateCommand())
            {

                // query SQL
                consulta.CommandText = @"select count(1) from client
                                where
                                (@ID_TEXT = -1 OR id = @ID_TEXT) AND
                                (@RAO_SOCIAL_TEXT = '%%' OR raoSocial like @RAO_SOCIAL_TEXT)";

                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!


                int id_filtre_int = -1;
                if (!Int32.TryParse(id_filtre, out id_filtre_int))
                {
                    id_filtre_int = -1;
                }

                Utils.CrearParametre(consulta, id_filtre_int, "ID_TEXT", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, "%" + rao_social_filtre + "%", "RAO_SOCIAL_TEXT", System.Data.DbType.String);

                return (int)(long)consulta.ExecuteScalar();
            }

        }

        public ObservableCollection<Client> GetClients(string id_filtre, string rao_social_filtre, int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<Client> resultat = new ObservableCollection<Client>();

            var connexio = _context.Database.GetDbConnection();

            // Obrir la connexió a la BD (només si fa falta)
            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }
            // Crear una consulta SQL
            using (var consulta = connexio.CreateCommand())
            {

                // query SQL
                consulta.CommandText =
                    @"select * from client 
                                where 
                                ( @ID_TEXT=-1 OR id = @ID_TEXT ) AND  					   
                                ( @RAO_SOCIAL_TEXT='%%' OR raoSocial like @RAO_SOCIAL_TEXT)";

                if (rows_per_page > 0)
                {
                    consulta.CommandText += $" limit {rows_per_page} offset {offset}";
                }

                int id_filtre_int = -1;
                if (!Int32.TryParse(id_filtre, out id_filtre_int))
                {
                    id_filtre_int = -1;
                }

                Utils.CrearParametre(consulta, id_filtre_int, "ID_TEXT", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, "%" + rao_social_filtre + "%", "RAO_SOCIAL_TEXT", System.Data.DbType.String);



                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!


                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                    {

                        // Descarreguem les dades de cada columna de la taula
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string CIF = reader.GetString(reader.GetOrdinal("CIF"));
                        string raoSocial = reader.GetString(reader.GetOrdinal("raoSocial"));
                        bool esActiva = reader.GetInt32(reader.GetOrdinal("esActiva")) == 1;
                        int tipus = reader.GetInt32(reader.GetOrdinal("tipus"));
                        int provincia_id = reader.GetInt32(reader.GetOrdinal("provincia_id"));

                        // Atenció ! Aquest camp pot ser NULL :-X
                        string imatge_url = "";
                        if (!reader.IsDBNull(reader.GetOrdinal("imatge_url")))
                        {
                            imatge_url = reader.GetString(reader.GetOrdinal("imatge_url"));
                        }

                        Client c = new Client(id, CIF, raoSocial, esActiva, (TipusEmpresa)tipus,
                            Provincia.GetProvincies().Where(x => x.Id == provincia_id).First());

                        resultat.Add(c);
                    }
                }

            }

            return resultat;
        }



        public bool InsertClient(Client c)
        {

            var connexio = _context.Database.GetDbConnection();

            // Obrir la connexió a la BD (només si fa falta)
            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }

            //var transaccio = connexio.BeginTransaction(); // inicia una transacció

            // assumirem que la transacció s'inicia pel context del UnitOfWork
            var transaccio = _context.Database.CurrentTransaction;
            if (transaccio == null) throw new Exception("NO TRANSACTION");
            else
                // Crear una consulta SQL
                using (var consulta = connexio.CreateCommand())
                {

                    // query SQL
                    consulta.CommandText =
                        @"insert into client (CIF, raoSocial, esActiva,tipus, provincia_id, imatge_url )
                                        values(  @CIF,  @raoSocial,@esActiva ,@tipus ,@provincia_id,@imatge_url)
                              ";

                    consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!

                    Utils.CrearParametre(consulta, c.CIF1, "CIF", System.Data.DbType.String);
                    Utils.CrearParametre(consulta, c.RaoSocial, "raoSocial", System.Data.DbType.String);
                    Utils.CrearParametre(consulta, c.EsActiva, "esActiva", System.Data.DbType.Int32);
                    Utils.CrearParametre(consulta, c.Tipus, "tipus", System.Data.DbType.Int32);
                    Utils.CrearParametre(consulta, c.Provincia.Id, "provincia_id", System.Data.DbType.Int32);
                    Utils.CrearParametre(consulta, "", "imatge_url", System.Data.DbType.String);

                    int numeroFilesAfectades = consulta.ExecuteNonQuery(); // UPDATE; DELETE; INSERT

                    consulta.CommandText = @"select last_insert_id()";

                    ulong lastId = (ulong)consulta.ExecuteScalar();

                    c.Id = (int)lastId; // desem l'ID al client

                    if (numeroFilesAfectades != 1)
                    {
                        transaccio.Rollback(); // Torna enrera !!!
                        return false;
                    }
                    else
                    {
                        //transaccio.Commit();
                        return true;
                    }

                }

        }

        public bool UpdateClient(Client c)
        {


            var connexio = _context.Database.GetDbConnection();

            // Obrir la connexió a la BD
            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }

            //var transaccio = connexio.BeginTransaction(); // inicia una transacció
            var transaccio = _context.Database.CurrentTransaction;
            if (transaccio == null) throw new Exception("NO TRANSACTION");

            // Crear una consulta SQL
            using (var consulta = connexio.CreateCommand())
            {

                // query SQL
                consulta.CommandText =
                    @"update client 
                                set
                                    CIF                = @CIF          ,
                                    raoSocial          = @raoSocial    ,
                                    esActiva           = @esActiva     ,
                                    tipus              = @tipus        ,
                                    provincia_id       = @provincia_id ,
                                    imatge_url         = @imatge_url  
                                where id = @ID
                              ";

                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!


                Utils.CrearParametre(consulta, c.Id, "ID", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, c.CIF1, "CIF", System.Data.DbType.String);
                Utils.CrearParametre(consulta, c.RaoSocial, "raoSocial", System.Data.DbType.String);
                Utils.CrearParametre(consulta, c.EsActiva, "esActiva", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, c.Tipus, "tipus", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, c.Provincia.Id, "provincia_id", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, "", "imatge_url", System.Data.DbType.String);

                int numeroFilesAfectades = consulta.ExecuteNonQuery(); // UPDATE; DELETE; INSERT


                if (numeroFilesAfectades != 1)
                {
                    transaccio.Rollback(); // Torna enrera !!!
                    return false;
                }
                else
                {
                    // Hem decidit fer el commit a fora...això és opcional, és clar.
                    //transaccio.Commit();
                    return true;
                }

            }
        }




        public bool DeleteClient(int id)
        {

            var connexio = _context.Database.GetDbConnection();

            // Obrir la connexió a la BD
            if (connexio.State == System.Data.ConnectionState.Closed)
            {
                connexio.Open();
            }

            //var transaccio = connexio.BeginTransaction(); // inicia una transacció
            var transaccio = _context.Database.CurrentTransaction;
            if (transaccio == null) throw new Exception("NO TRANSACTION");

            // Crear una consulta SQL
            using (var consulta = connexio.CreateCommand())
            {

                // query SQL
                consulta.CommandText =
                    @"delete from client where id = @ID ";

                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!

                Utils.CrearParametre(consulta, id, "ID", System.Data.DbType.Int32);
                
                int numeroFilesAfectades = consulta.ExecuteNonQuery(); // UPDATE; DELETE; INSERT


                if (numeroFilesAfectades != 1)
                {
                    transaccio.Rollback(); // Torna enrera !!!
                    return false;
                }
                else
                {
                    // Hem decidit fer el commit a fora...això és opcional, és clar.
                    //transaccio.Commit();
                    return true;
                }

            }
        }



    }

}
