using SmartFishLogin.encryp.Dtos;
using SmartFishLogin.encryp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartFishLogin.encryp.Repositories
{
    internal class EncrypRepo : IEncryp
    {
        private static readonly Rijndael _rijndael = Rijndael.Create();

        public static string EncriptarContrasena(string contrasena, string clave)
        {
            var biteClave = Encoding.UTF8.GetBytes(clave);
            _rijndael.Key = biteClave;
            _rijndael.Padding = PaddingMode.PKCS7;

            byte[] bytesContrasena = Encoding.UTF8.GetBytes(contrasena);
            ICryptoTransform encryptor = _rijndael.CreateEncryptor();
            byte[] bytesContrasenaEncriptada = encryptor.TransformFinalBlock(bytesContrasena, 0, bytesContrasena.Length);

            return Convert.ToBase64String(bytesContrasenaEncriptada);
        }

        public static string DesencriptarContrasena(string contrasenaEncriptada, string clave)
        {
            _rijndael.Key = Encoding.UTF8.GetBytes(clave);
            _rijndael.Padding = PaddingMode.PKCS7;

            byte[] bytesContrasenaEncriptada = Convert.FromBase64String(contrasenaEncriptada);
            ICryptoTransform decryptor = _rijndael.CreateDecryptor();
            byte[] bytesContrasena = decryptor.TransformFinalBlock(bytesContrasenaEncriptada, 0, bytesContrasenaEncriptada.Length);

            return Encoding.UTF8.GetString(bytesContrasena);
        }
        public async Task<EncrypDto> DesEncryption(DataEncryp param)
        {
            try
            {

                var returData = new EncrypDto();
                returData.DataEncry = DesencriptarContrasena("gET1Q2DsKov1TrjceTzZ90ja7dPrzCE/5MVv88nCJ/s=", "AAECAwQFBgcICQoLDA0ODw==");
                return returData;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(DesEncryption)})";
                throw new Exception(mensajeModificado);
            }
        }

        public async Task<EncrypDto> Encryption(DataEncryp param)
        {
            try
            {
                var returData = new EncrypDto();
                returData.DataEncry = EncriptarContrasena("tu_contrasena.==", "AAECAwQFBgcICQoLDA0ODw==");
                return returData;
            }
            catch (Exception ex)
            {
                string mensajeModificado = $"{ex.Message} <- (Clase: {GetType().Name}, Método : {nameof(Encryption)})";
                throw new Exception(mensajeModificado);
            }
        }
    }
}
