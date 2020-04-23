using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Domain.Entities;
using BlabberApp.Domain.Interfaces;

namespace BlabberApp.DataStore.Plugins
{
    public class MySqlBlab : IBlabPlugin
    {
        MySqlConnection dcBlab;
        public MySqlBlab()
        {
            this.dcBlab = new MySqlConnection("server=142.93.114.73;database=donbstringham;user=donbstringham;password=letmein");
            try
            {
                this.dcBlab.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public void Close()
        {
            this.dcBlab.Close();
        }
        public void Create(IEntity obj)
        {
            Blab blab = (Blab)obj;
            try
            {
                DateTime now = DateTime.Now;
                string sql = "INSERT INTO blabs (sys_id, message, dttm_created, user_id) VALUES ('"
                     + blab.Id + "', '"
                     + MySql.Data.MySqlClient.MySqlHelper.EscapeString(blab.Message) + "', '"
                     + now.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                     + blab.User.Email + "')";
                MySqlCommand cmd = new MySqlCommand(sql, this.dcBlab);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEnumerable ReadAll()
        {
            try
            {
                // SELECT * FROM blabs WHERE blabs.dttm_created NOT over a week ago SORTED DESC BY blabs.dttm_created
                string sql = "SELECT * FROM blabs";
                MySqlDataAdapter daBlabs = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlabs = new MySqlCommandBuilder(daBlabs);
                DataSet dsBlabs = new DataSet();

                daBlabs.Fill(dsBlabs);

                ArrayList blabs = new ArrayList();

                foreach(DataRow dtRow in dsBlabs.Tables[0].Rows)
                {
                    blabs.Add(DataRow2Blab(dtRow));
                }
                
                return blabs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEntity ReadById(Guid Id)
        {
            try
            {
                string sql = "SELECT * FROM blabs WHERE blabs.sys_id = '" + Id.ToString() + "'";
                MySqlDataAdapter daBlab = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlab = new MySqlCommandBuilder(daBlab);
                DataSet dsBlab = new DataSet();

                daBlab.Fill(dsBlab, "blabs");

                DataRow row = dsBlab.Tables[0].Rows[0];
                Blab blab = new Blab();

                blab.Id = new Guid(row["sys_id"].ToString());

                return blab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public IEnumerable ReadByUserId(string email)
        {
            try
            {
                string sql = "SELECT * FROM blabs WHERE blabs.user_id = '" + email.ToString() + "'";
                MySqlDataAdapter daBlabs = new MySqlDataAdapter(sql, this.dcBlab); // To avoid SQL injection.
                MySqlCommandBuilder cbBlabs = new MySqlCommandBuilder(daBlabs);
                DataSet dsBlabs = new DataSet();

                daBlabs.Fill(dsBlabs);

                ArrayList blabs = new ArrayList();

                foreach(DataRow dtRow in dsBlabs.Tables[0].Rows)
                {
                    blabs.Add(DataRow2Blab(dtRow));
                }
                
                return blabs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public void Update(IEntity obj)
        {
            Blab blab = (Blab)obj;
        }

        public void Delete(IEntity obj)
        {
            Blab blab = (Blab)obj;
        }
        public void DeleteAll()
        {
                string sql = "TRUNCATE TABLE blabs";
                MySqlCommand cmd = new MySqlCommand(sql, this.dcBlab);
                cmd.ExecuteNonQuery();
        }
        private Blab DataRow2Blab(DataRow row)
        {
            User user = new User();
            
            user.ChangeEmail(row["user_id"].ToString());

            Blab blab = new Blab(user);

            blab.Id = new Guid(row["sys_id"].ToString());
            blab.Message = row["message"].ToString();
            blab.DTTM = (DateTime)row["dttm_created"];

            return blab;
        }
    }
}