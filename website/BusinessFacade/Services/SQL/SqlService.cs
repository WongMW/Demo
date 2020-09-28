using SitefinityWebApp.BusinessFacade.Interfaces.SQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SitefinityWebApp.BusinessFacade.Services.SQL
{
    public class SqlService : SqlInterface
    {
        public async Task<int> ExecuteSPSqlCommand(string spName, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SoftwareDesign.Helper.GetAptifyEntitiesConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(spName))
                    {
                        cmd.Connection = con;

                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }

                        cmd.Parameters.Add(new SqlParameter("@ID", System.Data.SqlDbType.Int));
                        cmd.Parameters["@ID"].Direction = System.Data.ParameterDirection.Output;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();

                        int s = cmd.ExecuteNonQuery();
                        int ival = Convert.ToInt32(cmd.Parameters["@ID"].Value.ToString());

                        return ival;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ExecuteSPUpdateWithoutResultSqlCommand(string spName, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(SoftwareDesign.Helper.GetAptifyEntitiesConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(spName))
                    {
                        cmd.Connection = con;

                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(param);
                        }
                        
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();

                        int s = cmd.ExecuteNonQuery();

                        return "Ok";
                    }
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

    }
}