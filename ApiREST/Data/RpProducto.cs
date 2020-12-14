using ApiREST.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiREST.Data
{
    public class RpProducto
    {
        private readonly string _conexion;

        public RpProducto(IConfiguration configuration)
        {
            this._conexion = configuration.GetConnectionString("VentasConnection");
        }

        public async Task<List<Producto>> GetAll()
        {
            var response = new List<Producto>();

            try
            {
                SqlConnection conn = new SqlConnection(_conexion);
                SqlCommand cmd = new SqlCommand("sp_GetVenta", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                await conn.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    response.Add(MapComlumn(reader));
                }

               await conn.CloseAsync();

            }catch(Exception e)
            {
                
            }

            return response;
        }

        private Producto MapComlumn(SqlDataReader reader)
        {
            return new Producto()
            {
                Id = (int)reader["Id"],
                producto = reader["Producto"].ToString(),
                valor = (decimal)reader["Valor"],
                unidad = (int)reader["Unidad"]
            };
        }

        public async Task<Producto> GetById(string producto)
        {
            Producto response = null;

            try
            {
                SqlConnection conn = new SqlConnection(_conexion);
                SqlCommand cmd = new SqlCommand("spVentaProductoCategoria", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@producto",producto));
                await conn.OpenAsync();

                var reader = await cmd.ExecuteReaderAsync();
                while(await reader.ReadAsync())
                {
                    response = MapComlumn(reader);
                }

                await conn.CloseAsync();
                return response;

            }catch(Exception e)
            {
                return response;
            }
        }

        public async Task<int> InsertProduct(Producto producto)
        {
            try
            {
                SqlConnection conn = new SqlConnection(_conexion);
                SqlCommand cmd = new SqlCommand("sp_InsertProduct",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@producto",producto.producto));
                cmd.Parameters.Add(new SqlParameter("@valor", producto.valor));
                cmd.Parameters.Add(new SqlParameter("@unidad", producto.unidad));

                await conn.OpenAsync();
                await cmd.ExecuteReaderAsync();

                await conn.CloseAsync();

                return 1;
            }catch(Exception e)
            {
                return 0;
            }


        }
    }
}
