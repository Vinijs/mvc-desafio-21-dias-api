using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace mvc.Models
{
    public partial class Aluno
    {

        #region  "Metodos de classe ou staticos"

        private static string connectionString()
        {
            // create database desafio21diasapi
            /*
                CREATE TABLE Alunos (
                    id int IDENTITY(1,1) PRIMARY KEY,
                    nome varchar(150) NOT NULL,
                    matricula varchar(15) NOT NULL,
                    notas varchar(255));
            */
            return @"Persist Security Info=False;User ID=sa;password=_43690;Initial Catalog=desafio21diasapi;Data Source=DESKTOP-8BST02I\SQLEXPRESS";
        }

        public static void Incluir(Aluno aluno)
        {
            using(SqlConnection sqlConn = new SqlConnection(connectionString()))
            {
                sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"insert into Alunos(nome, matricula, notas)values(@nome, @matricula, @notas)",sqlConn);
            
            sqlCommand.Parameters.Add("@nome", SqlDbType.VarChar);
            sqlCommand.Parameters["@nome"].Value = aluno.Nome;

            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
            // sqlCommand.Parameters.Add("@matricula", SqlDbType.VarChar);
            // sqlCommand.Parameters["@matricula"].Value = aluno.Matricula;

            sqlCommand.Parameters.AddWithValue("@notas", aluno.Notas.ToArray());
            // sqlCommand.Parameters.Add("@nota", SqlDbType.VarChar);
            // sqlCommand.Parameters["@nota"].Value = string.Join(",", aluno.Notas.ToArray());

            sqlCommand.ExecuteNonQuery();
            

            sqlConn.Close();
            }            
        }

        public static void Atualizar(Aluno aluno)
        {
            
            SqlConnection sqlConn = new SqlConnection(connectionString());

            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"update Alunos set nome=@nome, matricula=@matricula, notas=@notas where id=@id",sqlConn);

            sqlCommand.Parameters.AddWithValue("@id", aluno.Id);
            sqlCommand.Parameters.AddWithValue("@nome", aluno.Nome);
            sqlCommand.Parameters.AddWithValue("@matricula", aluno.Matricula);
            sqlCommand.Parameters.AddWithValue("@notas", aluno.Notas.ToArray());
            
            sqlCommand.ExecuteNonQuery();
            sqlConn.Close();
            sqlConn.Dispose();

        }

        public static void ApagarPorId(int id)
        {
           
            SqlConnection sqlConn = new SqlConnection(connectionString());

            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand($"delete from Alunos where id={id}",sqlConn);
            sqlCommand.ExecuteNonQuery();
            

            sqlConn.Close();
            sqlConn.Dispose();

        }
        public static List<Aluno> Todos()
        {
            var alunos = new List<Aluno>();
            
            SqlConnection sqlConn = new SqlConnection(connectionString());

            sqlConn.Open();

            SqlCommand sqlCommand = new SqlCommand("select * from Alunos",sqlConn);
            var reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                var notas = new List<double>();
                string strNotas = reader["notas"].ToString();
                foreach (var nota in strNotas.Split(','))
                {
                    notas.Add(Convert.ToDouble(nota));
                }

                var aluno = new Aluno()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Matricula = reader["matricula"].ToString(),
                    Notas = notas,
                };

                alunos.Add(aluno);
            }

            sqlConn.Close();
            sqlConn.Dispose();

            return alunos;
        }
        #endregion
    }

}

