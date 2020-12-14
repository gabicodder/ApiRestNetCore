using ApiREST.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using System.IO;
using System;

namespace ApiREST.Data
{
    public class RPValue
    {
        private readonly string _connectionString;

        public RPValue(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
                        
        }

        public async Task<List<Value>> GetAll()
        {
            var response = new List<Value>();

            try
            {

                SqlConnection sql = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("GetAllValues", sql);
                cmd.CommandType = CommandType.StoredProcedure;
                await sql.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                  response.Add(MapToValue(reader));
                }

                await sql.CloseAsync();

            } catch(SqlException e)
            {
                var msj = e.ToString();
                return response;
            }

            
            return response;
        }

        private Value MapToValue(SqlDataReader reader)
        {
            return new Value()
            {
                Id = (int)reader["Id"],
                Value1 = (int)reader["Value1"],
                Value2 = reader["Value2"].ToString()
            };
        }

        public async Task<Value> GetById(int id)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("GetValueById", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", id));
            Value response = null;
            await sql.OpenAsync();

            var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                response = MapToValue(reader);
            }

            await sql.CloseAsync();
            return response;
        }

        public async Task<string> Insert(Value value)
        {
            try
            {
                SqlConnection sql = new SqlConnection(_connectionString);
                SqlCommand cmd = new SqlCommand("InsertValue", sql);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Value1", value.Value1));
                cmd.Parameters.Add(new SqlParameter("@Value2", value.Value2));
                await sql.OpenAsync();
                await cmd.ExecuteReaderAsync();

                //await sql.CloseAsync();
                
                return "OK";

            }catch(SqlException e)
            {
                return e.ToString();
            }
            

        }

        public async Task DeleteById(int id)
        {
            SqlConnection sql = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("DeleteValue", sql);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Id", id)); 
            await sql.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            await sql.CloseAsync();

        }
    }
}
