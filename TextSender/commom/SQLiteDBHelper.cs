/*
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Reflection;
using System.Collections;

namespace Common
{
    public class SQLiteDBHelper
    {
        private string connectionString = string.Empty;

        /// <summary>     
        /// 构造函数     
        /// </summary>     
        /// <param name="dbPath">SQLite数据库文件路径</param>     
        public SQLiteDBHelper(string dbPath)
        {
            this.connectionString = "Data Source=" + dbPath;
        }

        /// <summary>     
        /// 创建SQLite数据库文件     
        /// </summary>     
        /// <param name="dbPath">要创建的SQLite数据库文件路径</param>     
        public static void CreateDB(string dbPath)
        {
            if (!System.IO.File.Exists(dbPath))
            {
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                        command.ExecuteNonQuery();

                        command.CommandText = "DROP TABLE Demo";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>     
        /// 执行一个查询语句，返回一个包含查询结果的DataTable     
        /// </summary>     
        /// <param name="sql">要执行的查询语句</param>     
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>     
        /// <returns></returns>     
        public DataTable ExecuteDataTable(string sql, IList<SQLiteParameter> parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (!(parameters == null || parameters.Count == 0))
                    {
                        foreach (SQLiteParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }

        /// <summary>     
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。     
        /// </summary>     
        /// <param name="sql">要执行的增删改的SQL语句</param>     
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>     
        /// <returns></returns>     
        public int ExecuteNonQuery(string sql, IList<SQLiteParameter> parameters)
        {
            int affectedRows = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (!(parameters == null || parameters.Count == 0))
                        {
                            foreach (SQLiteParameter parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }

        /// <summary>     
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例     
        /// </summary>     
        /// <param name="sql">要执行的查询语句</param>     
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>     
        /// <returns></returns>     
        public SQLiteDataReader ExecuteReader(string sql, IList<SQLiteParameter> parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (!(parameters == null || parameters.Count == 0))
            {
                foreach (SQLiteParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>     
        /// 执行一个查询语句，返回查询结果的第一行第一列     
        /// </summary>     
        /// <param name="sql">要执行的查询语句</param>     
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param>     
        /// <returns></returns>     
        public Object ExecuteScalar(string sql, IList<SQLiteParameter> parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (!(parameters == null || parameters.Count == 0))
                    {
                        foreach (SQLiteParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>     
        /// 查询数据库中的所有数据类型信息     
        /// </summary>     
        /// <returns></returns>     
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                //foreach (DataColumn column in data.Columns)     
                //{     
                //    Console.WriteLine(column.ColumnName);     
                //}     
                return data;
            }
        }

        public static void FastInsert(string[] args)
        {
            string dbPath = Environment.CurrentDirectory + "/test.db";//指定数据库路径

            using (SQLiteConnection conn = new SQLiteConnection("Data Source =" + dbPath))//创建连接
            {
                conn.Open();//打开连接
                using (SQLiteTransaction tran = conn.BeginTransaction())//实例化一个事务
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        SQLiteCommand cmd = new SQLiteCommand(conn);//实例化SQL命令
                        cmd.Transaction = tran;
                        cmd.CommandText = "insert into student values(@id, @name, @sex)";//设置带参SQL语句
                        cmd.Parameters.AddRange(new[] {//添加参数
                            new SQLiteParameter("@id", i),
                            new SQLiteParameter("@name", "中国人"),
                            new SQLiteParameter("@sex", "男")
                        });
                        cmd.ExecuteNonQuery();//执行查询
                    }
                    tran.Commit();//提交
                }
            }
        }

        //====================================================================================
        static void Main__(string[] args)
        {
            //CreateTable(); 
            //InsertData(); 
            ShowData();

        }
        public static void CreateTable()
        {
            string dbPath = "D:\\Demo.db3";
            //如果不存在改数据库文件，则创建该数据库文件 
            if (!System.IO.File.Exists(dbPath))
            {
                SQLiteDBHelper.CreateDB("D:\\Demo.db3");
            }
            SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            string sql = "CREATE TABLE IF NOT EXISTS Test3(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name char(3),TypeName varchar(50),addDate datetime,UpdateTime Date,Time time,Comments blob)";
            db.ExecuteNonQuery(sql, null);
        }

        public static void InsertData()
        {
            string sql = "INSERT INTO Test3(Name,TypeName,addDate,UpdateTime,Time,Comments)values(@Name,@TypeName,@addDate,@UpdateTime,@Time,@Comments)";
            SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            for (char c = 'A'; c <= 'Z'; c++)
            {
                for (int i = 0; i < 1; i++)
                {
                    SQLiteParameter[] parameters = new SQLiteParameter[]{
                                                new SQLiteParameter("@Name",c+i.ToString()),
                                        new SQLiteParameter("@TypeName",c.ToString()),
                                        new SQLiteParameter("@addDate",DateTime.Now),
                                        new SQLiteParameter("@UpdateTime",DateTime.Now.Date),
                                        new SQLiteParameter("@Time",DateTime.Now.ToShortTimeString()),
                                        new SQLiteParameter("@Comments","Just a Test"+i)
                                        };
                    db.ExecuteNonQuery(sql, parameters);
                }
            }
        }

        public static void ShowData()
        {
            //查询从50条起的20条记录 
            string sql = "select * from test3 order by id desc limit 50 offset 20";
            SQLiteDBHelper db = new SQLiteDBHelper("D:\\Demo.db3");
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Console.WriteLine("ID:{0},TypeName{1}", reader.GetInt64(0), reader.GetString(1));
                }
            }
        }



    }
}
*/