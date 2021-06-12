using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExerciseProductDB.DAO
{
    class ProductDAO
    {
        string pid;
        string pname;
        string price;
        string catename;

        public ProductDAO(string pid, string pname, string price, string catename)
        {
            this.pid = pid;
            this.pname = pname;
            this.price = price;
            this.catename = catename;
        }

        public string Pid { get => pid; set => pid = value; }
        public string Pname { get => pname; set => pname = value; }
        public string Price { get => price; set => price = value; }
        public string Catename { get => catename; set => catename = value; }

        internal static List<ProductDAO> getListProduct()
        {
            DataTable data = Database.getDataSql("select ProductId,ProductName,Price,Categories.CategoryName from Products,Categories where Products.CategoryId = Categories.CategoryId");
            List<ProductDAO> proList = new List<ProductDAO>();
            foreach (DataRow dataRow in data.Rows)
            {
                string pid = dataRow["ProductId"].ToString();
                string pname = dataRow["ProductName"].ToString();
                string price = dataRow["Price"].ToString();
                string catname = dataRow["CategoryName"].ToString();
                ProductDAO pro = new ProductDAO(pid, pname, price, catname);
                proList.Add(pro);
            }
            return proList;
        }
        internal static List<ProductDAO> getListProductbyName(string name)
        {
            DataTable data = Database.getDataSql("select ProductId,ProductName,Price,Categories.CategoryName from Products,Categories where Products.CategoryId = Categories.CategoryId and ProductName like '%"+name+"%'");
            List<ProductDAO> proList = new List<ProductDAO>();
            foreach (DataRow dataRow in data.Rows)
            {
                string pid = dataRow["ProductId"].ToString();
                string pname = dataRow["ProductName"].ToString();
                string price = dataRow["Price"].ToString();
                string catname = dataRow["CategoryName"].ToString();
                ProductDAO pro = new ProductDAO(pid, pname, price, catname);
                proList.Add(pro);
            }
            return proList;
        }
        internal static void Delete(string id)
        {
            Database.Execute("delete from Products where ProductId ='"+id+"'");
        }
        internal static void Add(string id, string pname, string cateid, string unit, string price, string quantity, bool discontinued, DateTime date )
        {
            Database.Execute("insert into Products values('" + id + "','" + pname + "','" + cateid + "','" + unit + "','" + price + "','" + quantity + "','" + discontinued + "','" + date + "')");
        }
        internal static DataTable getCate()
        {
            return Database.getDataSql("select * from Categories");
        }

        internal static ArrayList LoadDataById(ArrayList list, string id)
        {
            DataTable data = Database.getDataSql("select *from Products where ProductId = '" + id + "'");
            list[0] = data.Rows[0]["ProductId"].ToString();
            list[1] = data.Rows[0]["ProductName"].ToString();
            list[2] = data.Rows[0]["CategoryId"].ToString();
            list[3] = data.Rows[0]["Unit"].ToString();
            list[4] = data.Rows[0]["Price"].ToString();
            list[5] = data.Rows[0]["Quantity"].ToString();
            list[6] = data.Rows[0]["Discontinued"].ToString();
            list[7] = data.Rows[0]["CreateDate"].ToString();
            return list;
        }
        internal static void Update( string pname, string cateid, string unit, string price, string quantity, bool discontinued, DateTime date, string id)
        {
            Database.Execute("update Products set ProductName = '"+pname+"', CategoryId = '"+cateid+"', Unit = '"+unit+"', Price = '"+price+"', Quantity = '"+quantity+"', Discontinued = '"+discontinued+"', CreateDate= '"+date+"' where ProductId = '"+id+"' ");
        }
    }
}
