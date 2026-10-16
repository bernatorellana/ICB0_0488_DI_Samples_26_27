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
    public class DAOProvincia : IDAOProvincia
    {

        private readonly MyDBContext _context;


        public DAOProvincia(MyDBContext c)
        {
            _context = c;
        }



        public Provincia GetProvincia(int id)
        {
            ObservableCollection<Provincia> Provincies =
                GetProvincies("" + id, "");
            if (Provincies.Count == 0) return null;
            else return Provincies[0];
        }

        public int GetNumeroProvincies(string nom)
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
                consulta.CommandText = @"select count(1) from Provincia
                                where
                                (@NOM_TEXT = '%%' OR nom like @NOM_TEXT)";

                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!


                Utils.CrearParametre(consulta, "%" + nom + "%", "NOM_TEXT", System.Data.DbType.String);

                return (int)(long)consulta.ExecuteScalar();
            }

        }

        public ObservableCollection<Provincia> GetProvincies(string id_filtre, string nom, 
                        int offset = 0, int rows_per_page = -1)
        {
            ObservableCollection<Provincia> resultat = new ObservableCollection<Provincia>();

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
                    @"select * from Provincia 
                                where 
                                ( @ID_TEXT=-1 OR id = @ID_TEXT ) AND  					   
                                ( @NOM_TEXT='%%' OR nom like @NOM_TEXT)
                ";

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
                Utils.CrearParametre(consulta, "%" + nom + "%", "NOM_TEXT", System.Data.DbType.String);


                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!


                using (var reader = consulta.ExecuteReader())
                {
                    while (reader.Read()) // per cada Read() avancem una fila en els resultats de la consulta.
                    {

                        // Descarreguem les dades de cada columna de la taula
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string nomR = reader.GetString(reader.GetOrdinal("nom"));
                    

                        Provincia c = new Provincia(id,nomR);


                        resultat.Add(c);
                    }
                }

            }

            return resultat;
        }



        public bool InsertProvincia(Provincia c)
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
                        @"insert into Provincia ( nom )
                                        values(  @nom)
                              ";

                    consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!

                    Utils.CrearParametre(consulta, c.Nom, "nom", System.Data.DbType.String);

                    int numeroFilesAfectades = consulta.ExecuteNonQuery(); // UPDATE; DELETE; INSERT

                    consulta.CommandText = @"select last_insert_id()";

                    ulong lastId = (ulong)consulta.ExecuteScalar();

                    c.Id = (int)lastId; // desem l'ID al Provincia

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

        public bool UpdateProvincia(Provincia c)
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
                    @"update Provincia 
                                set
                                    nom                = @nom          
                                where id = @ID
                              ";

                consulta.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction(); // IMPORTANT !!! NO us ho deixeu <--------------------!!!!!!!


                Utils.CrearParametre(consulta, c.Id, "ID", System.Data.DbType.Int32);
                Utils.CrearParametre(consulta, c.Nom, "nom", System.Data.DbType.String);
                
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




        public bool DeleteProvincia(int id)
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
                    @"delete from Provincia where id = @ID ";

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
