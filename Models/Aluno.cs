using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace mvc.Models
{
    public partial class Aluno
    {
        #region "Propriedades"
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        private List<double> notas;
        public List<double> Notas 
        {
             get
             {
                if(notas == null) this.notas = new List<double>();
                return this.notas;
             } 
             set
             {
                this.notas = value;
             } 
        }

        #endregion

        #region "Metodos de instancia"

        public string StrNotas()
        {
            return string.Join(",",this.Notas.ToArray());
        }

        public double CalcularMedia()
        {
            var somaNotas = 0.0;
            foreach(var nota in this.Notas){
                somaNotas += nota;
            }
            return somaNotas/ this.Notas.Count;
        }

        public string Situacao()
        {
            return this.CalcularMedia() >= 7 ? "Aprovado" : "Reprovado";
        }

        public  void ApagarPorId()
        {
            Aluno.ApagarPorId(this.Id);
        }

        public  void Salvar()
        {
            if(this.Id > 0)
            {
                Aluno.Atualizar(this);
            }
            else
            {
                Aluno.Incluir(this);
            }
        }

        #endregion
    }

}

