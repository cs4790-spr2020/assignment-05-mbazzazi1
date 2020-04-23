using System;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;
using BlabberApp.DataStore.Interfaces;
using BlabberApp.Domain.Entities;
using BlabberApp.Domain.Interfaces;

namespace BlabberApp.DataStore.Plugins
{
    public class MySqlUser : IUserPlugin
    {
        MySqlConnection _dcUser;
        public MySqlUser()
        {
            _dcUser = new MySqlConnection("server=142.93.114.73;database=donbstringham;user=donbstringham;password=letmein");
            try
            {
                _dcUser.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public void Close()
        {
            _dcUser.Close();
        }
        public void Create(IEntity obj)
        {
            User user = (User)obj;
            try
            {
                DateTime now = DateTime.Now;
                string sql = "INSERT INTO users (sys_id, email, dttm_registration, dttm_last_login) VALUES ('"
                     + user.Id + "', '"
                     + user.Email + "', '"
                     + now.ToString("yyyy-MM-dd HH:mm:ss")
                     + "', '" + now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                MySqlCommand cmd = new MySqlCommand(sql, _dcUser);
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
                string sql = "SELECT * FROM users";
                MySqlDataAdapter daUser = new MySqlDataAdapter(sql, _dcUser); // To avoid SQL injection.
                MySqlCommandBuilder cbUser = new MySqlCommandBuilder(daUser);
                DataSet dsUsers = new DataSet();

                daUser.Fill(dsUsers, "users");

                ArrayList users = new ArrayList();

                foreach (DataRow row in dsUsers.Tables[0].Rows)
                {
                    users.Add(DataRow2User(row));
                }

                return users;
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
                string sql = "SELECT * FROM users WHERE users.sys_id = '" + Id.ToString() + "'";
                MySqlDataAdapter daUser = new MySqlDataAdapter(sql, _dcUser); // To avoid SQL injection.
                MySqlCommandBuilder cbUser = new MySqlCommandBuilder(daUser);
                DataSet dsUser = new DataSet();

                daUser.Fill(dsUser, "users");

                DataRow row = dsUser.Tables[0].Rows[0];

                return DataRow2User(row);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + ": Not found");
            }
        }
        public IEntity ReadByUserEmail(string Id)
        {
            try
            {
                string sql = "SELECT * FROM users WHERE users.email = '" + Id.ToString() + "'";
                MySqlDataAdapter daUser = new MySqlDataAdapter(sql, _dcUser); // To avoid SQL injection.
                MySqlCommandBuilder cbUser = new MySqlCommandBuilder(daUser);
                DataSet dsUser = new DataSet();

                daUser.Fill(dsUser, "users");

                DataRow row = dsUser.Tables[0].Rows[0];
                
                return DataRow2User(row);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString() + ": Not found");
            }
        }

        public void Update(IEntity obj)
        {
            User user = (User)obj;
        }

        public void Delete(IEntity obj)
        {
            User user = (User)obj;
            try{
                string sql = "DELETE FROM users WHERE users.email='"+user.Email+"'";
                MySqlCommand cmd = new MySqlCommand(sql, _dcUser);
                cmd.ExecuteNonQuery();
            } catch(Exception ex) {
                throw new Exception(ex.ToString());
            }
        }

        public void DeleteAll()
        {
                string sql = "TRUNCATE TABLE users";
                MySqlCommand cmd = new MySqlCommand(sql, _dcUser);
                cmd.ExecuteNonQuery();
        }

        private User DataRow2User(DataRow row)
        {
            User user = new User();

            user.Id = new Guid(row["sys_id"].ToString());
            user.ChangeEmail(row["email"].ToString());
            user.RegisterDTTM = (DateTime)row["dttm_registration"];
            user.LastLoginDTTM = (DateTime)row["dttm_last_login"];

            return user;
        }
    }
}