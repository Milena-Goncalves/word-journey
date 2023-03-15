using System;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace projecto_iefp.Models;

public class livrosContext
{
    public string ConnectionString { get; set; }


    public livrosContext()
    {
        this.ConnectionString = "server=localhost;port=3306;database=bd_leitura;user=root;password=123456";
    }

    private MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public List<livro> GetAllLivros()
    {
        List<livro> livroList = new List<livro>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM livros ORDER BY titulo ASC", conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    livroList.Add(new livro()
                    {
                        titulo = reader.GetString("titulo"),
                        autor = reader.GetString("autor"),
                        editora = reader.GetString("editora"),
                        genero = reader.GetString("genero"),
                        sinopse = reader.GetString("sinopse")
                    });

                }
            }
        }
        return livroList;
    }

    //Operação GetGeneros dos livros

    public List<livro> GetAllGenero(string genero)
    {
        List<livro> livroList = new List<livro>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();

         
            //Criei uma variável com @ no MySql, mas não sabia como usar a variável, então voltei ao storage normal
            MySqlCommand cmd = new MySqlCommand("CALL Genero (@genero);", conn);

            cmd.Parameters.AddWithValue("@genero", genero);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    livroList.Add(new livro()
                    {
                        titulo = reader.GetString("titulo"),
                        autor = reader.GetString("autor"),
                        editora = reader.GetString("editora"),
                        genero = reader.GetString("genero"),
                        sinopse = reader.GetString("sinopse")
                    });

                }
            }
        }
        return livroList;
    }

    public List<string> GetAllTitulos()
    {
        List<string> titulos = new List<string>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT titulo FROM livros", conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    titulos.Add(reader.GetString("titulo"));
                }
            }
        }

        return titulos;
    }

    //Operações CRUD - Create
    public void createComentario(Comentario comentarios)
    {

        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();

            //Query
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Comentario (livro_titulo,nome,comentario,email) VALUES (@titulo, @nome,@comentario,@email);", conn);


            cmd.Parameters.AddWithValue("titulo", comentarios.titulo);
            cmd.Parameters.AddWithValue("nome", comentarios.nome);
            cmd.Parameters.AddWithValue("email", comentarios.email);
            cmd.Parameters.AddWithValue("comentario", comentarios.comentario);

            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }

    public List<Comentario> GetAllComentarios()
    {
        List<Comentario> ComentarioList = new List<Comentario>();

        using (MySqlConnection conn = GetConnection())
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM comentario", conn);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ComentarioList.Add(new Comentario()
                    {
                        titulo = reader.GetString("livro_titulo"),
                        nome = reader.GetString("nome"),
                        comentario = reader.GetString("comentario"),
                        email = reader.GetString("email")
                    });

                }
            }
        }
        return ComentarioList;
    }

    public List<Comentario> searchComentario(string titulo)
    {

        List<Comentario> comentariosList = new List<Comentario>();

        
        using (MySqlConnection conn = GetConnection())
        {
            //Abrir a ligação
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM comentario WHERE livro_titulo LIKE CONCAT('%',@titulo,'%');", conn);
            cmd.Parameters.AddWithValue("@titulo", titulo);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    comentariosList.Add(new Comentario()
                    {
                        titulo = reader.GetString("livro_titulo"),
                        nome = reader.GetString("nome"),
                        email = reader.GetString("email"),
                        comentario = reader.GetString("comentario")
                    });
                    
                }
            }
        }

        return comentariosList;
    }

}