
using cube_connect;

string connectionString = @"DataSource=";
string db_name = "";
string tbl_name = "";

Connection conn = new Connection(connectionString);

var table = conn.get_cube_table(db_name, tbl_name);
var db = conn.get_cube_db(db_name);
Console.WriteLine($"table name {table.Name}");

TableProcessing tableProcessing = new TableProcessing(table);

tableProcessing.get_partitions();
tableProcessing.add_partition();

Console.ForegroundColor = ConsoleColor.White;
