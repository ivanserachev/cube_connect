using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Tabular;

namespace cube_connect
{
    internal class Connection
    {
        private Server server;
        private Database cube_db;
        private Model cube_model;
        private Table cube_table;
        private string connectionString;

        public Connection(string connectionString)
        {

            this.server = server;
            this.connectionString = connectionString;

        }
        private void ssas_connect()
        {
            server = new Server();
            try
            {
                server.Connect(connectionString);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connection successful");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nServer name:\t{0}", server.Name);
                Console.WriteLine("Server product name:\t{0}", server.ProductName);
                Console.WriteLine("Server product level:\t{0}", server.ProductLevel);
                Console.WriteLine("Server version:\t{0}", server.Version);


            }

            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);

            }

        }

        public Database get_cube_db(string db_name)
        {
            if (server == null)
            {
                ssas_connect();
            }

            return cube_db_connect(db_name); ;
        }

        public Model get_cube_model(string db_name)
        {
            if (server == null)
            {
                ssas_connect();
            }
            return cube_model_connect(db_name);
        }

        public void get_cube_model_connections(string db_name)
        {

            DataSourceCollection dataSources;

            Model model = get_cube_model(db_name);

            dataSources = model.DataSources;

            int i = 1;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\n---Cube connections---");
            foreach (var source in dataSources)
            {
                Console.WriteLine("{0}. Connection:\t{1}", i, source.Name);
                i++;
            }
            Console.WriteLine("---Cube connections---");

        }
        public Table get_cube_table(string db_name, string tbl_name)
        {
            if (server == null)
            {
                ssas_connect();
            }
            return cube_tbl_connect(db_name, tbl_name);
        }
        private Database cube_db_connect(string db_name)
        {

            try
            {
                DatabaseCollection db_coll;
                db_coll = server.Databases;

                Console.ForegroundColor = ConsoleColor.Cyan;
                int i = 1;

                Console.WriteLine("\n---Available cube databases---");
                foreach (var db in db_coll)
                {
                    Console.WriteLine("{0}. Database:\t{1}", i, db);
                    i++;
                }
                Console.WriteLine("---Available cube databases---");

                cube_db = server.Databases.GetByName(db_name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nConnect to cube db:\t{0};\tType of object:\t{1}", cube_db.Name, cube_db.GetType());

                return cube_db;

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);

                return null;
            }
        }
        private Model cube_model_connect(string db_name)
        {
            cube_db_connect(db_name);

            try
            {

                cube_model = cube_db.Model;

                Console.WriteLine("\nConnect to model name:\t{0};\tType of object:\t{1}", cube_model.Name, cube_model.GetType());

                return cube_model;

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);

                return null;
            }
        }

        private Table cube_tbl_connect(string db_name, string tbl_name)
        {
            cube_model_connect(db_name);

            TableCollection tbl_coll;

            try
            {

                tbl_coll = cube_model.Tables;

                Console.ForegroundColor = ConsoleColor.Cyan;
                int i = 1;

                Console.WriteLine("\n---Available tables in cube model---");
                foreach (var tbl in tbl_coll)
                {
                    Console.WriteLine("{0}. Table:\t{1}", i, tbl.Name);
                    i++;
                }
                Console.WriteLine("---Available tables in cube model---");

                cube_table = cube_model.Tables.Find(tbl_name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nConnect to table name:\t{0};\tType of object:\t{1}", cube_table.Name, cube_table.GetType());

                return cube_table;


            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);

                return null;
            }

        }
    }
}
