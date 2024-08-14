using Dapper;
using PhamQuocHuy_BE_DependencyInjection.Model;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;

namespace PhamQuocHuy_BE_DependencyInjection.Services
{
    public class UsersService : IUsersService
    {
        private readonly IConfiguration _configuration;

        public UsersService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IEnumerable<Users> GetUsers()
        {
            //Kết nối database
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                //Thực hiện truy vấn dữ liệu
                var items = connection.Query<Users>("select * from Users");
                return items;
            }
        }
        public Users AddUser(Users user)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                //Thực hiện sử dụng DML để thêm người dùng
                string sql = @"
            INSERT INTO Users (hoVaTen, soDienThoai, ngaySinh, gioiTinh, tinhThanh, username, password) 
            OUTPUT INSERTED.Id
            VALUES (@hoVaTen, @soDienThoai, @BirthDate, @gioiTinh, @tinhThanh, @Username, @Password);";
                //Sử dụng một phương thức của Dapper "QuerySingle" để thực hiện truy vấn SQL
                var id = connection.QuerySingle<int>(sql, new
                {
                    user.hoVaTen,
                    user.soDienThoai,
                    BirthDate = user.ngaySinh.ToString("yyyy-MM-dd"),
                    user.gioiTinh,
                    user.tinhThanh,
                    user.Username,
                    user.Password
                });

                user.Id = id; 
                return user;
            }
        }


        public Users UpdateUser(int id, Users user)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();

                string sql = @"
                    UPDATE Users
                    SET hoVaTen = @hoVaTen,
                        soDienThoai = @soDienThoai,
                        ngaySinh = @BirthDate,
                        gioiTinh = @gioiTinh,
                        tinhThanh = @tinhThanh,
                        username = @Username,
                        password = @Password
                    WHERE Id = @Id;";

                var rowsAffected = connection.Execute(sql, new
                {
                    user.hoVaTen,
                    user.soDienThoai,
                    BirthDate = user.ngaySinh.ToString("yyyy-MM-dd"),
                    user.gioiTinh,
                    user.tinhThanh,
                    user.Username,
                    user.Password,
                    Id = id
                });

                if (rowsAffected == 0)
                {
                    return null; 
                }

                user.Id = id;
                return user;
            }
        }
        public Users DeleteUser(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                string selectSql = "SELECT * FROM Users WHERE Id = @Id";
                var user = connection.QuerySingleOrDefault<Users>(selectSql, new { Id = id });

                if (user == null)
                {
                    return null;
                }
                string deleteSql = "DELETE FROM Users WHERE Id = @Id";
                var taikhoanxoa = connection.Execute(deleteSql, new { Id = id });
                return taikhoanxoa > 0 ? user : null;
            }
        }

    }
}
