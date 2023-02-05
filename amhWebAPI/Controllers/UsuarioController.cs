﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using amhWebAPI.Helpers;
using amhWebAPI.Models;
using amhWebAPI.DTO;

namespace amhWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly string secretKey;
        private AmhWebDbContext _contexto;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioController(IConfiguration config, AmhWebDbContext context, IConfiguration configuration, IMapper mapper)
        {
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _contexto = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO request)
        {
            var passEncoded = CryptographyHelper.Encrypt(request.Password);
            request.Password = passEncoded;
            //var passDecoded = CryptographyHelper.Decrypt(passEncoded);
            UsuarioDTO userDTO = Authenticate(request);
            if (userDTO != null)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.Login));
                claims.AddClaim(new Claim(ClaimTypes.Role, userDTO.Perfil));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfiguration = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreated = tokenHandler.WriteToken(tokenConfiguration);

                return StatusCode(StatusCodes.Status200OK, new { token = tokenCreated });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "", error = "Usuario o contraseña incorrectos" });
            }

        }

        private UsuarioDTO Authenticate(UsuarioLoginDTO userLogin)
        {
            var _user = (from tbl in _contexto.Usuario where tbl.Login == userLogin.Login && tbl.Password == userLogin.Password select tbl).FirstOrDefault();

            UsuarioDTO _userDTO = _mapper.Map<UsuarioDTO>(_user);

            if (_user != null)
            {
                var _perfil = (from tbl in _contexto.Perfil where tbl.Id == _user.IdPerfil select tbl).FirstOrDefault();

                if (_perfil != null)
                {
                    _userDTO.Perfil = _perfil.Descripcion;
                }
            }

            return _userDTO;             
        }

    }
}
