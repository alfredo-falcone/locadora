using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Falcone.Locadora.Sistema.Data;
using System.Security.Cryptography;
using Falcone.Locadora.Sistema.Src;

namespace Falcone.Locadora.Sistema.Src
{
  public static class Util
  {
    public static void VerificarSequencial(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
    {
      DbEntities contextoSequencial = new DbEntities();
      string nomeTipo = entityEntry.Entity.GetType().Name;
      Sequencial ultimo = contextoSequencial.Sequenciais.Where(s => s.Entidade == nomeTipo).SingleOrDefault();
      if (ultimo == null)
      {
        ultimo = new Sequencial() { Entidade = entityEntry.Entity.GetType().Name, ProximoSequencial = 1 };
        contextoSequencial.Sequenciais.Add(ultimo);
      }

      ultimo.ProximoSequencial += 1;
      entityEntry.CurrentValues["Id"] = ultimo.ProximoSequencial;
      entityEntry.Entity.GetType().GetProperty("Id").SetValue(entityEntry.Entity, ultimo.ProximoSequencial, null);
      contextoSequencial.SaveChanges();
    }


    public static void PreencherDateTimeMinValue(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry)
    {
      DbEntities contextoSequencial = new DbEntities();
      Type tipo = entityEntry.Entity.GetType();
      foreach (var property in tipo.GetProperties())
      {
        if (property.PropertyType.Equals(typeof(DateTime)) && (DateTime)property.GetValue(entityEntry.Entity, null) == default(DateTime))
        {
          property.SetValue(entityEntry.Entity, Constantes.DATA_MINIMA, null);
          entityEntry.CurrentValues[property.Name] = Constantes.DATA_MINIMA;
        }

      }
    }


    public static string GerarMD5(string input)
    {
      using (MD5 md5Hash = MD5.Create())
      {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
          sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
      }
    }

    // Verify a hash against a string.
    public static bool VerificarMD5(string input, string hash)
    {

      // Hash the input.
      string hashOfInput = GerarMD5(input);

      // Create a StringComparer an compare the hashes.
      StringComparer comparer = StringComparer.OrdinalIgnoreCase;

      if (0 == comparer.Compare(hashOfInput, hash))
      {
        return true;
      }
      else
      {
        return false;
      }
    }


    public static void ValidarLogin(string login, string senha)
    {
      DbEntities banco = new DbEntities();

      var usuario = banco.Usuarios.Where(u => u.Login == login).SingleOrDefault();
      if (usuario == null || !VerificarMD5(usuario.Sal + senha, usuario.Senha))
      {
        throw new UsuarioOuSenhaInvalidosException();
      }


    }

  }
}
