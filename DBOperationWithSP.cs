using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace AdoDotNetConnectedArch
{
    class UsingSp
    {
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        public int ShowData()
        {
            try
            {
                Console.WriteLine("Data from table after Dml command");
                cn = new SqlConnection("Data Source=.;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from employee", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["empid"]}\t{dr["Empname"]}\t\t{dr["salary"]}\t\t{dr["deptno"]}");
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int ShowData1()
        {
            try
            {
                Console.WriteLine("enter the empid");
                var eid = int.Parse(Console.ReadLine());
                cn = new SqlConnection("Data Source=.;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_ShowEmp", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["Empname"]}\t\t{dr["salary"]}\t\t{dr["dptname"]}");
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int UpdateWithSp()
        {
            try
            {
                Console.WriteLine("enter the empid to Update");
                var eid = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Employee name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("enter department id");

                var did = Convert.ToSingle(Console.ReadLine());

                cn = new SqlConnection("Data Source=.;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_UpdateEmp", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row updated using storedprocedure...");
                ShowData();
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int InsertWithSp()
        {
            try
            {
                Console.WriteLine("Enter Employee name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter employee salary");
                var esal = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter employee deptid");
                var did = Convert.ToSingle(Console.ReadLine());
                cn = new SqlConnection("Data Source=.;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_InsertEmp", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@sal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@dno", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row added to the table by insert with parameters...");
                ShowData();
                return i;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }
        public int DeleteWithSp()
        {
            try
            {
                Console.WriteLine("enter the empid to delete");
                var a = int.Parse(Console.ReadLine());
                cn = new SqlConnection("Data Source=.;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_DeleteExp", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = a;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("one row deleted to the table...");
                ShowData();
                return i;

            }
            catch (Exception e)
            {
                Console.WriteLine();
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
    }
    class DBOperationWithSP
    {
        static void Main()
        {

            UsingSp c2 = new UsingSp();
            int ch;
            for (; ; )
            {
                Console.WriteLine("enter your choice: \n1.insert row\n2.delete row\n3.update row\n4.search\n5.showdata");
                ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        c2.InsertWithSp();
                        break;
                    case 2:
                        c2.DeleteWithSp();
                        break;
                    case 3:
                        c2.UpdateWithSp();
                        break;
                    case 4:
                        c2.ShowData1();
                        break;
                    case 5:
                        c2.ShowData();
                        break;
                    default:
                        Console.WriteLine("Please enter valid choice");
                        break;
                }
            }
        }
    }
}
