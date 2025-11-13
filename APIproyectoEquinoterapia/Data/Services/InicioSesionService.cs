//using APIproyectoEquinoterapia.Data.Helpers;
//using APIproyectoEquinoterapia.Data.Models;
//using APIproyectoEquinoterapia.DTOs;
//using Azure.Core;
//using Microsoft.EntityFrameworkCore;

//using Microsoft.IdentityModel.Tokens;
//using System.Configuration;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace APIproyectoEquinoterapia.Data.Services
//{
//    public class InicioSesionService
//    {
//        private readonly  AppDbContext _context;
//        private readonly IConfiguration _configuration;

//        public InicioSesionService(AppDbContext context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

////////////////////////
//        public async Task <(bool,string,string)> LoginAsync(CredencialesDto inicioSesionDataDto)
//        {
//            if (ValidateData(inicioSesionDataDto))
//                return (false,"Datos de usuario incompletos","");

//            var usuario = await ValidateUserInDB(inicioSesionDataDto.Usuario);
//            if (IsNull(usuario) || IsNull(usuario!.IdUsuario) || usuario.IdUsuario == 0)
//                return (false,"Usuario no encontrado","");

//            //inicioSesionDto.contrasena = EncryptPassword(inicioSesionDto.contrasena);
//            var acceso = await ValidateAccesoInDb(inicioSesionDataDto, usuario.IdUsuario);
//            if (IsNull(acceso) || acceso!.IdUsuario == 0)
//                return (false, "Credenciales incorrectas", "");

//            var token = GetToken(acceso);

//            return (true,"Usuario encontrado",token);
//        }







////////////////////////
//        private bool IsEmpty(string? str)
//        {
//            return string.IsNullOrWhiteSpace(str);
//        }

////////////////////////
//        private bool IsNull(object? obj)
//        {
//            if (obj == null)
//            {
//                return true;
//            }
//            return false;
//        }

////////////////////////
//        private bool ValidateData(CredencialesDto inicioSesionDataDto)
//        {
//            if (IsEmpty(inicioSesionDataDto.Usuario)
//                || IsEmpty(inicioSesionDataDto.contrasena))
//                return true;
//            return false;
//        }

////////////////////////
//        private string EncryptPassword(string contrasena)
//        {
//            return Encrypt.EncryptStringSspcWeb(contrasena);
//        }

////////////////////////
//        private async Task <InicioSesion?> ValidateUserInDB(string usuario)
//        {
//            var resultados = await _context.inicioSesion
//                .FromSqlInterpolated($@"
//                    EXEC dbo.Sp_LoginNovedades 
//                        @User = {usuario}, 
//                        @Password = '', 
//                        @IdSistema = {1}, 
//                        @IdPerfil = {0}, 
//                        @Accion = {1}")
//                .ToListAsync();
//            if(resultados.Count == 0)
//                return null;

//            return resultados.SingleOrDefault();

//        }

////////////////////////
//        private async Task <GetDataUsuarioDto?> ValidateAccesoInDb(CredencialesDto inicioSesionDataDto, int idUsuario)
//        {
//            var resultados = await _context.inicioSesion
//                .FromSqlInterpolated($@"
//                    EXEC dbo.Sp_LoginNovedades 
//                        @User = {inicioSesionDataDto.Usuario}, 
//                        @Password = {inicioSesionDataDto.contrasena}, 
//                        @IdSistema = {1}, 
//                        @IdPerfil = {idUsuario}, 
//                        @Accion = {2}")
//                .ToListAsync();

//            if (resultados.Count == 0)
//                return null;

//            var usuarioFormateado = MapToDto(resultados.SingleOrDefault());

//            return usuarioFormateado;
//        }

////////////////////////
//        private GetDataUsuarioDto MapToDto (InicioSesion usuario)
//        {

//            return new GetDataUsuarioDto
//            {
//                IdUsuario = usuario.IdUsuario,
//                IdKronos = usuario.IdKronos,
//                Usuario = usuario.Usuario,
//                NombreCompleto = usuario.NombreCompleto,
//                Mensaje = usuario.Mensaje,
//                Foto = usuario.Foto,
//                IdPerfil = usuario.IdPerfil,
//                Perfil = usuario.Perfil
//            };
//        }

////////////////////////
//        public string GetToken(GetDataUsuarioDto inicioSesionDto)
//        {
//            var claims = new[]
//            {
//        new Claim(ClaimTypes.Name, inicioSesionDto.Usuario ?? ""),
//        new Claim(ClaimTypes.NameIdentifier, inicioSesionDto.IdUsuario.ToString()),
//    };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: "FrontEquinoterapia",
//                audience: "FrontEquinoterapia",
//                claims: claims,
//                expires: DateTime.Now.AddHours(12),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
