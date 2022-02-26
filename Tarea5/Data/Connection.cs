using MySql.Data.MySqlClient;
using System.Data;

namespace Tarea5.Data
{
    public class Connection
    {
        public static Connection instacia = null;
        private MySqlConnection conn;

        public Connection()
        {
            conn = new MySqlConnection("server=localhost;user id=root;database=northwind;");
        }

        ~Connection()
        {
            conn.Close();
        }

        public static MySqlConnection getConnection()
        {
            if (instacia == null)
            {
                instacia = new Connection();

            }

            return instacia.conn;
        }

        public static List<Dictionary<string, object>> Consulta (string sql)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            MySqlConnection conexion = Connection.getConnection();

            MySqlCommand command = new MySqlCommand(sql, conexion);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();

            adapter.Fill(table);

            foreach (DataRow fila in table.Rows)
            {
                Dictionary<string, object> filaDiccionario = new Dictionary<string, object>();
                foreach (DataColumn columna in table.Columns)
                {
                    filaDiccionario.Add(columna.ColumnName, fila[columna]);
                }
                list.Add(filaDiccionario);
            }

            return list;
        }
    }
}
