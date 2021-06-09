using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProductManagements.DAO
{
    
    class Category
    {
       
        string catId;
        string catName;
        string desc;

        public Category(string catId, string catName, string desc)
        {
            this.CatId = catId;
            this.CatName = catName;
            this.Desc = desc;
        }

        public Category()
        {
        }

        public string CatId { get => catId; set => catId = value; }
        public string CatName { get => catName; set => catName = value; }
        public string Desc { get => desc; set => desc = value; }


        // tuong ung cac phuong thuc duoc goi cho cac chuc nang

        
        public DataTable getCategoryById(string categoryId)
        {
            return Database.getDataBySQL("select * from Categories where CategoryId like '%" + categoryId + "%'");

        }
        public DataTable getCategoryByName(string categoryName)
        {
            return Database.getDataBySQL("select * from Categories where CategoryName like '%" + categoryName + "%'");

        }
        
        internal static List<Category> getCategories()
        {
            List<Category> categories = new List<Category>();
            DataTable dataTable = Database.getDataBySQL("select * from Categories ");
            foreach(DataRow dataRow in dataTable.Rows)
            {
                string catid = dataRow["CategoryId"].ToString();
                string catname = dataRow["CategoryName"].ToString();
                string catdesc = dataRow["Description"].ToString();
                Category category = new Category(catid, catname, catdesc);
                categories.Add(category);
            }

            return categories;
            //return getDataBySQL("select * from Categories ");
        
        }

        internal static int addCategory(ArrayList arrayList)
        {
            string sql = "insert into Categories values(@catId, @catName, @desc)";
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@catId",SqlDbType.Char),
                new SqlParameter("@catName",SqlDbType.NVarChar),
                new SqlParameter("@desc",SqlDbType.Text)
            };

            // gan gia tri cho cac tham so cua cau truy van
            for (int i = 0; i < arrayList.Count; i++)
            {
                para[i].Value = arrayList[i];
            }
            return Database.Execute(sql,para);
        }

        internal static int updateCategory(ArrayList arrayList)
        {
            string sql = "update Categories set CategoryName = @catName, Description =  @desc where CategoryId = @catId";
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@catId",SqlDbType.Char),
                new SqlParameter("@catName",SqlDbType.NVarChar),
                new SqlParameter("@desc",SqlDbType.Text)
            };

            // gan gia tri cho cac tham so cua cau truy van
            for (int i = 0; i < arrayList.Count; i++)
            {
                para[i].Value = arrayList[i];
            }
            return Database.Execute(sql, para);
        }

        internal static int deleteCategory(ArrayList arrayList)
        {
            string sql = "delete from Categories where CategoryId = @catId";
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@catId",SqlDbType.Char)
            };

            // gan gia tri cho cac tham so cua cau truy van
            for (int i = 0; i < arrayList.Count; i++)
            {
                para[i].Value = arrayList[i];
            }
            return Database.Execute(sql, para);
        }
    }
}
